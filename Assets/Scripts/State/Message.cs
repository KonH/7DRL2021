using UniRx;

namespace CloudBreak.State {
	public sealed class Message {
		public readonly string Sender;
		public readonly string Header;
		public readonly string Body;

		public ReactiveProperty<bool> Read { get; } = new ReactiveProperty<bool>();

		public Message(string sender, string header, string body) {
			Sender = sender;
			Header = header;
			Body   = body;
		}
	}
}