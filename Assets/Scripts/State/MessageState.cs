using UniRx;

namespace CloudBreak.State {
	public sealed class MessageState {
		public ReactiveCollection<Message> Messages { get; } = new ReactiveCollection<Message>();
	}
}