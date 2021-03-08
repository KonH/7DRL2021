using CloudBreak.Configuration;

namespace CloudBreak.State {
	public sealed class ServerMessage : ServerFile {
		public readonly MessageSetup.TemplateId Template;
		public readonly object[]                Args;

		public ServerMessage(MessageSetup.TemplateId template, string name, params object[] args) : base($"{name}.txt") {
			Template = template;
			Args     = args;
		}
	}
}