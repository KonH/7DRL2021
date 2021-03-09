namespace CloudBreak.State {
	public sealed class ChangeServerCommand : Command {
		public readonly Server Server;

		public ChangeServerCommand(Server server) : base($"ssh {server.Address}", $"Connect to {server.Address}") {
			Server = server;
		}
	}
}