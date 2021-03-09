using CloudBreak.State;
using UniRx;
using UnityEngine;
using Zenject;

namespace CloudBreak.View {
	public sealed class InboxView : Panel {
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
			var trans    = instance.transform;
			trans.SetParent(_root);
			trans.SetSiblingIndex(addEvent.Index);
			trans.localScale = Vector3.one;
		}
	}
}