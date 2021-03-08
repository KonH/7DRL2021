using CloudBreak.Configuration;
using CloudBreak.State;

namespace CloudBreak.Service {
	public sealed class InventoryService {
		readonly InventoryState _state;
		readonly MessageService _messageService;
		readonly UIService      _uiService;

		public InventoryService(InventoryState state, MessageService messageService, UIService uiService) {
			_state          = state;
			_messageService = messageService;
			_uiService      = uiService;
		}

		public void AddFile(ServerFile file) {
			_state.Files.Add(file);
			switch ( file ) {
				case ServerMessage msg: {
					var message = _messageService.AddMessage(msg.Template, msg.Args);
					_uiService.OpenMessage(message);
					break;
				}

				case ServerKey key: {
					var message = _messageService.AddMessage(MessageSetup.TemplateId.ServerKey, key.Address, "key value");
					_uiService.OpenMessage(message);
					break;
				}
			}
		}
	}
}