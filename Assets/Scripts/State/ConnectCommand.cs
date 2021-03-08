namespace CloudBreak.State {
	public sealed class ConnectCommand : Command {
		public ConnectCommand() : base("", "ssh # connect to other server") {}
	}
}