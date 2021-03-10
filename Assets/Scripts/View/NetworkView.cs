using CloudBreak.State;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CloudBreak.View {
	public sealed class NetworkView : MonoBehaviour, IDragHandler, IScrollHandler {
		[SerializeField] RectTransform  _root;
		[SerializeField] Vector2        _offset;
		[SerializeField] float          _moveSpeed;
		[SerializeField] AnimationCurve _moveCurve;

		ServerView.Pool _pool;

		Vector3 _startPosition;
		Vector3 _endPosition;
		float   _moveTimer;

		[Inject]
		public void Inject(ServerState state, ServerView.Pool pool) {
			_pool = pool;
			state.AvailableServers
				.ObserveAdd()
				.Subscribe(OnAddServer);
			state.CurrentServer
				.Subscribe(OnCurrentServerChange);
		}

		void OnAddServer(CollectionAddEvent<Server> addEvent) {
			var server = addEvent.Value;
			var view   = _pool.Spawn(server);
			var trans  = view.transform;
			trans.SetParent(_root);
			trans.localPosition = server.Position;
			view.AddLinks();
		}

		void OnCurrentServerChange(Server server) {
			_startPosition = _root.localPosition;
			_endPosition   = (new Vector2(-server?.Position.x ?? 0, -server?.Position.y ?? 0) * _root.localScale + _offset);
			_moveTimer     = 1.0f;
		}

		void Update() {
			if ( _moveTimer <= 0 ) {
				return;
			}
			_moveTimer -= Time.deltaTime * _moveSpeed;
			var t = _moveCurve.Evaluate(1 - _moveTimer);
			_root.localPosition = Vector3.Lerp(_startPosition, _endPosition, t);
		}

		public void OnDrag(PointerEventData eventData) {
			_root.localPosition += (Vector3)eventData.delta;
		}

		public void OnScroll(PointerEventData eventData) {
			var diff     = eventData.scrollDelta.y * 0.33f;
			var rawScale = (_root.localScale + Vector3.one * diff);
			_root.localScale = new Vector3(
				Mathf.Clamp(rawScale.x, 0.25f, 1.75f),
				Mathf.Clamp(rawScale.y, 0.25f, 1.75f),
				Mathf.Clamp(rawScale.z, 0.25f, 1.75f)
			);
		}
	}
}