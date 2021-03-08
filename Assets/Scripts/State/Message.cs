using CloudBreak.Configuration;
using UniRx;

namespace CloudBreak.State {
	public sealed class Message {
		public readonly MessageSetup.TemplateId Template;

		public readonly string Sender;
		public readonly string Header;
		public readonly string Body;

		public ReactiveProperty<bool> Read { get; } = new ReactiveProperty<bool>();

		public Message(MessageSetup.TemplateId template, string sender, string header, string body) {
			Template = template;
			Sender   = sender;
			Header   = header;
			Body     = body;
		}
	}
}