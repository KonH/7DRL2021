namespace CloudBreak.State {
	public sealed class ChangeServerCommand : Command {
		public readonly Server Server;

		public ChangeServerCommand(Server server) : base($"ssh {server.Address}", $"ssh {server.Address} # connect to {server.Address}") {
			Server = server;
		}
	}
}