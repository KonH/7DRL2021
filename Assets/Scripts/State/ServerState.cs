using UniRx;

namespace CloudBreak.State {
	public sealed class ServerState {
		public ReactiveProperty<Server> CurrentServer { get; } = new ReactiveProperty<Server>();
	}
}