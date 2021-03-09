using CloudBreak.Configuration;
using CloudBreak.Service;
using CloudBreak.State;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CloudBreak.View {
	public sealed class InboxDetailsView : Panel {
		[SerializeField] TMP_Text _senderText;
		[SerializeField] TMP_Text _headerText;
		[SerializeField] TMP_Text _bodyText;
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
			_closeButton.gameObject.SetActive(activeMessage.Template != MessageSetup.TemplateId.MainStoryFinal);
			_senderText.text = $"{activeMessage.Sender} -> {activeMessage.Receiver}";
			_headerText.text = activeMessage.Header;
			_bodyText.text   = activeMessage.Body;
		}

		void OnClick() {
			_service.CloseMessage();
		}
	}
}