using System.Collections.Generic;

namespace CloudBreak.State {
	public abstract class Command {
		public readonly string                 Text;
		public readonly string                 Description;
		public readonly IReadOnlyList<Command> NextCommands;

		public Command(string text, string description, params Command[] nextCommands) {
			Text         = text;
			Description  = description;
			NextCommands = nextCommands;
		}

		public override string ToString() {
			return $"{GetType().Name}({Text})";
		}
	}
}