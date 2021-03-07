using CloudBreak.Configuration;
using CloudBreak.Service;
using UnityEngine;
using Zenject;

namespace CloudBreak.View {
	public class Prototype : MonoBehaviour {
		MessageService _messageService;
		UIService      _uiService;

		[Inject]
		public void Inject(MessageService messageService, UIService uiService) {
			_messageService = messageService;
			_uiService      = uiService;
		}

		void Start() {
			var message = _messageService.AddMessage(MessageSetup.TemplateId.Hello);
			_uiService.OpenMessage(message);
		}
	}
}