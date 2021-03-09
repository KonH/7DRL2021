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

			var firstServer  = new Server("ai.smuggle.xx");
			var secondServer = new Server("db.smuggle.xx");
			var finalServer  = new Server("gateway.smuggle.xx");

			firstServer.Links.Add(secondServer);
			firstServer.Files.Add(new ServerKey(secondServer.Address));
			firstServer.Files.Add(new ServerMessage(MessageSetup.TemplateId.Dataset1, "old_dataset"));

			secondServer.Files.Add(new ServerKey(finalServer.Address));
			secondServer.Links.Add(firstServer);
			secondServer.Links.Add(finalServer);

			finalServer.Files.Add(new ServerMessage(MessageSetup.TemplateId.Final, "final"));

			_serverState.RootServer          = firstServer;
			_serverState.CurrentServer.Value = firstServer;

			_inventoryState.Files.Add(new ServerKey(firstServer.Address));

			var rootCommand = new ChangeServerCommand(firstServer);
			_commandService.Execute(rootCommand, hidden: true);
		}
	}
}