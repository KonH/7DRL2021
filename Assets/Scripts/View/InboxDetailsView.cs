using CloudBreak.Service;
using CloudBreak.State;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CloudBreak.View {
	public sealed class InboxDetailsView : MonoBehaviour {
		[SerializeField] TMP_Text _header;
		[SerializeField] TMP_Text _body;
		[SerializeField] Button   _closeButton;

		UIState   _state;
		UIService _service;

		[Inject]
		public void Inject(UIState state, UIService service) {
			_state   = state;
			_service = service;
			_closeButton.onClick.AddListener(OnClick);
		}

		void OnEnable() {
			var activeMessage = _state?.ActiveMessage;
			if ( activeMessage == null ) {
				return;
			}
			_header.text = activeMessage.Header;
			_body.text   = activeMessage.Body;
		}

		void OnClick() {
			_service.CloseMessage();
		}
	}
}