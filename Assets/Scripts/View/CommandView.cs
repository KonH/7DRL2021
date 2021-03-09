using CloudBreak.State;
using TMPro;
using UnityEngine;
using Zenject;
using UniRx;

namespace CloudBreak.View {
	public sealed class CommandView : Panel {
		[SerializeField] TMP_Text _serverText;
		[SerializeField] TMP_Text _commandText;

		string _currentText;
		int    _currentChar;
		float  _printTimer;
		float  _blinkTimer;

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
			_currentText      = value;
			_commandText.text = string.Empty;
			_currentChar      = 0;
			_printTimer       = 0;
		}

		void Update() {
			if ( string.IsNullOrEmpty(_currentText) ) {
				UpdateBlink();
			} else {
				UpdatePrint();
			}
		}

		void UpdatePrint() {
			_commandText.enabled = true;
			_printTimer += Time.deltaTime;
			if ( _currentChar >= _currentText.Length ) {
				if ( _printTimer < 0.1f ) {
					return;
				}
				_printTimer       = 0;
				_currentText      = string.Empty;
				_commandText.text = string.Empty;
			} else {
				if ( _printTimer < 0.025f ) {
					return;
				}
				_printTimer = 0;
				_commandText.text += _currentText[_currentChar];
				_currentChar++;
			}
		}

		void UpdateBlink() {
			_blinkTimer += Time.deltaTime;
			_commandText.text = "_";
			if ( _blinkTimer < 0.5f ) {
				return;
			}
			_commandText.enabled = !_commandText.enabled;
			_blinkTimer          = 0;
		}
	}
}