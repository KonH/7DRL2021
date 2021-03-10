using CloudBreak.Configuration;
using CloudBreak.State;
using UnityEngine;

namespace CloudBreak.Service {
	public sealed class MessageService {
		readonly MessageSetup _setup;
		readonly MessageState _state;

		public MessageService(MessageSetup setup, MessageState state) {
			_setup = setup;
			_state = state;
		}

		public Message AddMessage(MessageSetup.TemplateId templateId, params object[] args) {
			Debug.Log($"Add message from template: {templateId}");
			var template = _setup.Templates[templateId];
			var sender   = string.Format(template.Sender, args);
			var receiver = string.Format(template.Receiver, args);
			var header   = string.Format(template.Header, args);
			var body     = string.Format(template.Body, args);
			var message  = new Message(templateId, sender, receiver, header, body);
			_state.Messages.Insert(0, message);
			return message;
		}
	}
}