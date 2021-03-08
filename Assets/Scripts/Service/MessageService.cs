using CloudBreak.Configuration;
using CloudBreak.State;

namespace CloudBreak.Service {
	public sealed class MessageService {
		readonly MessageSetup _setup;
		readonly MessageState _state;

		public MessageService(MessageSetup setup, MessageState state) {
			_setup = setup;
			_state = state;
		}

		public Message AddMessage(MessageSetup.TemplateId id, params object[] args) {
			var template = _setup.Templates[id];
			var sender   = string.Format(template.Sender, args);
			var header   = string.Format(template.Header, args);
			var body     = string.Format(template.Body, args);
			var message  = new Message(sender, header, body);
			_state.Messages.Insert(0, message);
			return message;
		}
	}
}