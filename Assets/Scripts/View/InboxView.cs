using CloudBreak.State;
using UniRx;
using UnityEngine;
using Zenject;

namespace CloudBreak.View {
	public sealed class InboxView : MonoBehaviour {
		[SerializeField] Transform _root;

		MessageState          _state;
		InboxMessageView.Pool _pool;

		[Inject]
		public void Inject(MessageState state, InboxMessageView.Pool pool) {
			_state = state;
			_pool  = pool;
			_state
				.Messages
				.ObserveAdd()
				.Subscribe(OnNewMessage);
		}

		void OnNewMessage(CollectionAddEvent<Message> addEvent) {
			var message  = addEvent.Value;
			var instance = _pool.Spawn(message);
			instance.transform.SetParent(_root);
			instance.transform.SetSiblingIndex(addEvent.Index);
		}
	}
}