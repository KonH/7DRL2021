namespace CloudBreak.State {
	public sealed class ServerKey : ServerFile {
		public readonly string Address;

		public ServerKey(string address) : base($"{address}.key") {
			Address = address;
		}
	}
}