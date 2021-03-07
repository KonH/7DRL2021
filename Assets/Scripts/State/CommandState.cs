using System.Collections.Generic;
using UniRx;

namespace CloudBreak.State {
	public sealed class CommandState {
		public ReactiveCollection<Command> AvailableCommands { get; } = new ReactiveCollection<Command>();

		public ReactiveProperty<string> LastCommand  { get; } = new ReactiveProperty<string>();
		public Stack<Command>           CurrentChain { get; } = new Stack<Command>();
	}
}