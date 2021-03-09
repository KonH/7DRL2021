using System.Collections.Generic;
using UniRx;

namespace CloudBreak.State {
	public sealed class Server {
		public readonly string Address;

		public ReactiveCollection<ServerFile> Files { get; } = new ReactiveCollection<ServerFile>();

		public List<Server> Links { get; } = new List<Server>();

		public Server(string address) {
			Address = address;
		}
	}
}