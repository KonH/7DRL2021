using UniRx;

namespace CloudBreak.State {
	public sealed class Message {
		public readonly string Header;
		public readonly string Body;

		public ReactiveProperty<bool> Read { get; } = new ReactiveProperty<bool>();

		public Message(string header, string body) {
			Header = header;
			Body   = body;
		}
	}
}