using CloudBreak.State;
using TMPro;
using UnityEngine;
using Zenject;
using UniRx;

namespace CloudBreak.View {
	public sealed class CommandView : Panel {
		[SerializeField] TMP_Text _serverText;
		[SerializeField] TMP_Text _commandText;

		[Inject]
		public void Inject(ServerState serverState, CommandState commandState) {
			serverState.CurrentServer
				.Subscribe(OnServerChange);
			commandState.LastCommand
				.Subscribe(OnLastCommandChange);
		}

		void OnServerChange(Server server) {
			_serverText.text = $"{server?.Address}:";
		}

		void OnLastCommandChange(string value) {
			_commandText.text = value;
		}
	}
}