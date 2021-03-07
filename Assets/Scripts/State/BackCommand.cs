namespace CloudBreak.State {
	public sealed class BackCommand : Command {
		public BackCommand(string text, string description, params Command[] nextCommands) : base(text, description, nextCommands) {}
	}
}