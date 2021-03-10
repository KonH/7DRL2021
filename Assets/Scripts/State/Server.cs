using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace CloudBreak.State {
	public sealed class Server {
		public readonly string  Address;
		public readonly Vector2 Position;

		public ReactiveCollection<ServerFile> Files { get; } = new ReactiveCollection<ServerFile>();

		public List<Server> Links { get; } = new List<Server>();

		public Server(string address, Vector2 position) {
			Address  = address;
			Position = position;
		}

		public override string ToString() {
			return Address;
		}
	}
}