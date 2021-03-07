namespace CloudBreak.State {
	public sealed class PlaceholderCommand : Command {
		public PlaceholderCommand(string text, string description, params Command[] nextCommands) : base(text, description, nextCommands) {}
	}
}