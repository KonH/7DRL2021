using CloudBreak.Configuration;
using CloudBreak.State;

namespace CloudBreak.Service {
	public sealed class InventoryService {
		readonly InventoryState _state;
		readonly MessageService _messageService;

		public InventoryService(InventoryState state, MessageService messageService) {
			_state          = state;
			_messageService = messageService;
		}

		public void AddFile(ServerFile file) {
			_state.Files.Add(file);
			switch ( file ) {
				case ServerMessage message: {
					_messageService.AddMessage(message.Template, message.Args);
					break;
				}

				case ServerKey key: {
					_messageService.AddMessage(MessageSetup.TemplateId.ServerKey, key.Address, "key value");
					break;
				}
			}
		}
	}
}