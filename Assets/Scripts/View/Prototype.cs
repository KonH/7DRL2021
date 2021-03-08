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

		[Inject]
		public void Inject(
			MessageService messageService, UIService uiService, CommandService commandService, ServerState serverState) {
			_messageService = messageService;
			_uiService      = uiService;
			_commandService = commandService;
			_serverState    = serverState;
		}

		void Start() {
			var message = _messageService.AddMessage(MessageSetup.TemplateId.Hello);
			_uiService.OpenMessage(message);

			_serverState.CurrentServer = new Server("192.168.1.2");
			_serverState.CurrentServer.Files.Add(new ServerKey("192.168.1.3"));
			_serverState.CurrentServer.Files.Add(new ServerMessage(MessageSetup.TemplateId.Dataset1, "Old dataset"));

			var rootCommand = new PlaceholderCommand(
				"", "",
				new PlaceholderCommand(
					"ssh", "ssh # connect to other server",
					new BackCommand()),
				new PlaceholderCommand(
					"nmap", "nmap # lookup vulnerabilities",
					new BackCommand()),
				new ExploreCommand());
			_commandService.Execute(rootCommand);
		}
	}
}