namespace CloudBreak.State {
	public abstract class Command {
		public readonly string Text;
		public readonly string Description;

		public Command(string text, string description) {
			Text        = text;
			Description = description;
		}

		public override string ToString() {
			return $"{GetType().Name}({Text})";
		}
	}
}