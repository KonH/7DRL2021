using System.Collections.Generic;

namespace CloudBreak.State {
	public sealed class Server {
		public readonly string Address;

		public List<ServerFile> Files { get; } = new List<ServerFile>();

		public Server(string address) {
			Address = address;
		}
	}
}