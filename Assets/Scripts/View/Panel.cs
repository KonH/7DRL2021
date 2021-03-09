using UnityEngine;

namespace CloudBreak.View {
	public abstract class Panel : MonoBehaviour {
		[SerializeField] AnimationClip _appearAnimation;
		[SerializeField] AnimationClip _disappearAnimation;

		Animation _animation;
		bool      _waitHide;

		void Awake() {
			_animation = GetComponent<Animation>();
		}

		void Update() {
			if ( !_waitHide ) {
				return;
			}
			if ( _animation && _animation.isPlaying ) {
				return;
			}
			gameObject.SetActive(false);
			_waitHide = false;
		}

		public void Show() {
			if ( gameObject.activeSelf ) {
				return;
			}
			gameObject.SetActive(true);
			if ( !_animation ) {
				return;
			}
			_animation.clip = _appearAnimation;
			_animation.Play();
		}

		public void Hide() {
			if ( !gameObject.activeSelf ) {
				return;
			}
			_waitHide = true;
			if ( !_animation ) {
				return;
			}
			_animation.clip = _disappearAnimation;
			_animation.Play();
		}

		public void SetActive(bool active) {
			if ( active ) {
				Show();
			} else {
				Hide();
			}
		}
	}
}