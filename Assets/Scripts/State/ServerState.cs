using System.Collections.Generic;
using UniRx;

namespace CloudBreak.State {
	public sealed class ServerState {
		public Server RootServer { get; set; }

		public List<Server> AllServers { get; } = new List<Server>();

		public ReactiveProperty<Server>   CurrentServer    { get; } = new ReactiveProperty<Server>();
		public ReactiveCollection<Server> AvailableServers { get; } = new ReactiveCollection<Server>();

	}
}