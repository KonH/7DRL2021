using CloudBreak.Service;
using CloudBreak.State;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CloudBreak.View {
	[RequireComponent(typeof(Button))]
	public sealed class CommandButton : MonoBehaviour {
		public sealed class Pool : MonoMemoryPool<Command, CommandButton> {
			protected override void Reinitialize(Command command, CommandButton view) {
				view.Reinitialize(command);
			}
		}

		[SerializeField] TMP_Text _descriptionText;

		CommandService _service;

		Command _command;

		[Inject]
		public void Inject(CommandService service) {
			_service = service;
			GetComponent<Button>().onClick.AddListener(OnClick);
		}

		void Reinitialize(Command command) {
			_command              = command;
			_descriptionText.text = command.Description;
		}

		void OnClick() {
			_service.Execute(_command);
		}
	}
}