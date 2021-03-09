namespace CloudBreak.State {
	public sealed class DownloadCommand : Command {
		public readonly ServerFile File;

		public DownloadCommand(ServerFile file) : base($"wget \"{file.Name}\"", $"download file \"{file.Name}\"") {
			File = file;
		}
	}
}