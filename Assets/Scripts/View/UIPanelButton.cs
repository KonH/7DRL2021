using CloudBreak.State;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CloudBreak.View {
	[RequireComponent(typeof(Button))]
	public sealed class UIPanelButton : MonoBehaviour {
		[SerializeField] UIPanel _panel;

		UIState _state;

		[Inject]
		public void Inject(UIState state) {
			_state = state;
			GetComponent<Button>().onClick.AddListener(OnClick);
		}

		void OnClick() {
			_state.ActivePanel.Value = _panel;
		}
	}
}