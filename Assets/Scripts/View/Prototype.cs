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

			var firstServer = new Server("192.168.1.2");
			firstServer.Files.Add(new ServerKey("192.168.1.3"));
			firstServer.Files.Add(new ServerMessage(MessageSetup.TemplateId.Dataset1, "Old dataset"));

			var secondServer = new Server("192.168.1.3");

			firstServer.Links.Add(secondServer);
			secondServer.Links.Add(firstServer);

			_serverState.CurrentServer.Value = firstServer;

			var rootCommand = new ChangeServerCommand(firstServer);
			_commandService.Execute(rootCommand, hidden: true);
		}
	}
}