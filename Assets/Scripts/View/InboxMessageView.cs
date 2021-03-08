using CloudBreak.Service;
using CloudBreak.State;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;

namespace CloudBreak.View {
	[RequireComponent(typeof(Button))]
	public sealed class InboxMessageView : MonoBehaviour {
		public sealed class Pool : MonoMemoryPool<Message, InboxMessageView> {
			protected override void Reinitialize(Message message, InboxMessageView view) {
				view.Reinitialize(message);
			}
		}

		[SerializeField] TMP_Text _senderText;
		[SerializeField] TMP_Text _headerText;
		[SerializeField] Graphic  _unreadTarget;

		UIService _service;
		Message   _message;

		[Inject]
		public void Inject(UIService service) {
			_service = service;
			GetComponent<Button>().onClick.AddListener(OnClick);
		}

		void Reinitialize(Message message) {
			_message         = message;
			_senderText.text = message.Sender;
			_headerText.text = message.Header;
			_message.Read
				.Subscribe(OnReadChange);
		}

		void OnClick() {
			_service.OpenMessage(_message);
		}

		void OnReadChange(bool read) {
			_unreadTarget.enabled = !read;
		}
	}
}