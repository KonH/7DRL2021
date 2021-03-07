using UniRx;

namespace CloudBreak.State {
	public sealed class UIState {
		public ReactiveProperty<UIPanel> ActivePanel { get; } = new ReactiveProperty<UIPanel>();

		public Message ActiveMessage;
	}
}