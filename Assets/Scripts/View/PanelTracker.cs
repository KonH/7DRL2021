using CloudBreak.State;
using UnityEngine;
using Zenject;
using UniRx;

namespace CloudBreak.View {
	public sealed class PanelTracker : MonoBehaviour {
		[SerializeField] PanelObjectDictionary _panels;

		[Inject]
		public void Inject(UIState state) {
			state.ActivePanel.Subscribe(OnPanelChanged);
		}

		void OnPanelChanged(UIPanel panel) {
			foreach ( var pair in _panels ) {
				var key    = pair.Key;
				var holder = pair.Value;
				holder.SetActive(panel == key);
			}
		}
	}
}