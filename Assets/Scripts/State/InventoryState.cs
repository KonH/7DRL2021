using System.Collections.Generic;

namespace CloudBreak.State {
	public sealed class InventoryState {
		public List<ServerFile> Files { get; } = new List<ServerFile>();
	}
}