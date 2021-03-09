using CloudBreak.Configuration;
using CloudBreak.Service;
using CloudBreak.State;
using UnityEngine;
using Zenject;

namespace CloudBreak.View {
	public class Prototype : MonoBehaviour {
		MessageService _messageService;
		UIService      _uiService;
		CommandService _commandService;
		ServerState    _serverState;
		InventoryState _inventoryState;

		[Inject]
		public void Inject(
			MessageService messageService, UIService uiService, CommandService commandService, ServerState serverState,
			InventoryState inventoryState) {
			_messageService = messageService;
			_uiService      = uiService;
			_commandService = commandService;
			_serverState    = serverState;
			_inventoryState = inventoryState;
		}

		void Start() {
			var message = _messageService.AddMessage(MessageSetup.TemplateId.Hello);
			_uiService.OpenMessage(message);

			var firstServer  = new Server("ai.smuggle.xx", Vector2.zero);
			var secondServer = new Server("db.smuggle.xx", new Vector2(0, 100));
			var finalServer  = new Server("gateway.smuggle.xx", new Vector2(150, 0));

			firstServer.Links.Add(secondServer);
			firstServer.Files.Add(new ServerKey(secondServer.Address));
			firstServer.Files.Add(new ServerMessage(MessageSetup.TemplateId.Dataset1, "old_dataset"));

			secondServer.Files.Add(new ServerKey(finalServer.Address));
			secondServer.Links.Add(firstServer);
			secondServer.Links.Add(finalServer);

			finalServer.Files.Add(new ServerMessage(MessageSetup.TemplateId.Final, "final"));

			_serverState.RootServer          = firstServer;
			_serverState.CurrentServer.Value = firstServer;
			_serverState.AllServers.Add(firstServer);
			_serverState.AllServers.Add(secondServer);
			_serverState.AllServers.Add(finalServer);
			_serverState.AvailableServers.Add(firstServer);

			_inventoryState.Files.Add(new ServerKey(firstServer.Address));

			var rootCommand = new ChangeServerCommand(firstServer);
			_commandService.Execute(rootCommand, hidden: true);
		}
	}
}