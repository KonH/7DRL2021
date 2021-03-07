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

		[Inject]
		public void Inject(MessageService messageService, UIService uiService, CommandService commandService) {
			_messageService = messageService;
			_uiService      = uiService;
			_commandService = commandService;
		}

		void Start() {
			var message = _messageService.AddMessage(MessageSetup.TemplateId.Hello);
			_uiService.OpenMessage(message);

			var rootCommand = new PlaceholderCommand(
				"", "",
				new PlaceholderCommand(
					"ssh", "ssh # connect to other server",
					new BackCommand("", "# back")),
				new PlaceholderCommand(
					"nmap", "nmap # lookup vulnerabilities",
					new BackCommand("", "# back")),
				new PlaceholderCommand(
					"ls", "ls # investigate server storage",
					new BackCommand("", "# back")));
			_commandService.Execute(rootCommand);
		}
	}
}