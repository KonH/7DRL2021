using UniRx;

namespace CloudBreak.State {
	public sealed class UIState {
		public UIPanel                   PreviousPanel { get; set; }
		public ReactiveProperty<UIPanel> ActivePanel   { get; } = new ReactiveProperty<UIPanel>();

		public Message ActiveMessage;
	}
}