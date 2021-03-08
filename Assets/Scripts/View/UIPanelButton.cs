using CloudBreak.Service;
using CloudBreak.State;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CloudBreak.View {
	[RequireComponent(typeof(Button))]
	public sealed class UIPanelButton : MonoBehaviour {
		[SerializeField] UIPanel _panel;

		UIService _service;

		[Inject]
		public void Inject(UIService service) {
			_service = service;
			GetComponent<Button>().onClick.AddListener(OnClick);
		}

		void OnClick() {
			_service.ChangePanel(_panel);
		}
	}
}