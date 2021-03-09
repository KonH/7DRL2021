using System.Linq;
using CloudBreak.Configuration;
using CloudBreak.State;

namespace CloudBreak.Service {
	public sealed class InventoryService {
		readonly InventoryState _inventoryState;
		readonly ServerState    _serverState;
		readonly MessageService _messageService;
		readonly UIService      _uiService;

		public InventoryService(
			InventoryState inventoryState, ServerState serverState, MessageService messageService, UIService uiService) {
			_inventoryState = inventoryState;
			_serverState    = serverState;
			_messageService = messageService;
			_uiService      = uiService;
		}

		public void AddFile(ServerFile file) {
			_inventoryState.Files.Add(file);
			switch ( file ) {
				case ServerMessage msg: {
					var message = _messageService.AddMessage(msg.Template, msg.Args);
					_uiService.OpenMessage(message);
					break;
				}

				case ServerKey key: {
					_messageService.AddMessage(MessageSetup.TemplateId.ServerKey, key.Address, "key value");
					var server = _serverState.AllServers.Single(s => s.Address == key.Address);
					_serverState.AvailableServers.Add(server);
					break;
				}
			}
		}
	}
}