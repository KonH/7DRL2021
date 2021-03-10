using CloudBreak.State;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace CloudBreak.View {
	public sealed class LinkView : MonoBehaviour {
		public sealed class Pool : MonoMemoryPool<Server, Server, LinkView> {
			protected override void Reinitialize(Server start, Server end, LinkView view) {
				view.Reinitialize(start, end);
			}
		}

		[SerializeField] float          _speed;
		[SerializeField] RectTransform  _targetTransform;
		[SerializeField] Image          _targetImage;
		[SerializeField] AnimationCurve _alphaChange;
		[SerializeField] float          _minDelay;
		[SerializeField] float          _maxDelay;

		Vector2 _startPosition;
		Vector2 _endPosition;
		float   _timer;

		void Reinitialize(Server start, Server end) {
			gameObject.name = $"{start.Address} -> {end.Address}";
			_startPosition = start.Position;
			_endPosition = end.Position;
			SetupDelay();
		}

		void SetupDelay() {
			_timer = -Random.Range(_minDelay, _maxDelay);
		}

		void Update() {
			if ( _timer < 0 ) {
				_timer += Time.deltaTime;
				if ( _timer > 0 ) {
					_timer = 0;
				}
				return;
			}
			if ( _timer >= 1 ) {
				SetupDelay();
			}
			_targetTransform.localPosition = Vector2.Lerp(_startPosition, _endPosition, _timer);
			var color = _targetImage.color;
			color.a = _alphaChange.Evaluate(_timer);
			_targetImage.color = color;
			_timer += Time.deltaTime * _speed;
		}

		void OnDrawGizmosSelected() {
			var pos = transform.position;
			Gizmos.DrawLine(pos + (Vector3)_startPosition, pos + (Vector3)_endPosition);
		}
	}
}