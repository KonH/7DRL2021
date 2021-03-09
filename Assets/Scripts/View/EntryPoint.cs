using CloudBreak.Configuration;
using CloudBreak.Service;
using CloudBreak.State;
using UnityEngine;
using Zenject;

namespace CloudBreak.View {
	public class EntryPoint : MonoBehaviour {
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
			var message = _messageService.AddMessage(MessageSetup.TemplateId.MainStoryStart);
			_uiService.OpenMessage(message);

			var (rootServer, allServers) = NetworkSetup.Apply();

			_serverState.RootServer          = rootServer;
			_serverState.CurrentServer.Value = rootServer;
			_serverState.AllServers.AddRange(allServers);
			_serverState.AvailableServers.Add(rootServer);

			_inventoryState.Files.Add(new ServerKey(rootServer.Address));

			var rootCommand = new ChangeServerCommand(rootServer);
			_commandService.Execute(rootCommand, hidden: true);
		}
	}
}