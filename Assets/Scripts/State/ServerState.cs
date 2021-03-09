using UniRx;

namespace CloudBreak.State {
	public sealed class ServerState {
		public Server RootServer { get; set; }

		public ReactiveProperty<Server> CurrentServer { get; } = new ReactiveProperty<Server>();
	}
}