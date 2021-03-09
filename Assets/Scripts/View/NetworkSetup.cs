using System.Collections.Generic;
using CloudBreak.Configuration;
using CloudBreak.State;
using UnityEngine;

namespace CloudBreak.View {
	public static class NetworkSetup {
		public static (Server rootServer, List<Server> allServers) Apply() {
			var firstServer  = new Server("john.doe.smuggle.xx", Vector2.zero);
			var secondServer = new Server("ci.smuggle.xx", new Vector2(0, 100));
			var finalServer  = new Server("gateway.smuggle.xx", new Vector2(150, 0));

			firstServer.Links.Add(secondServer);
			firstServer.Files.Add(new ServerKey(secondServer.Address));

			secondServer.Links.Add(firstServer);
			secondServer.Links.Add(finalServer);
			secondServer.Files.Add(new ServerMessage(MessageSetup.TemplateId.MainStory1, "note1"));
			secondServer.Files.Add(new ServerMessage(MessageSetup.TemplateId.BackStory1, "log1"));
			secondServer.Files.Add(new ServerKey(finalServer.Address));

			finalServer.Files.Add(new ServerMessage(MessageSetup.TemplateId.MainStoryFinal, "congrats"));
			finalServer.Files.Add(new ServerMessage(MessageSetup.TemplateId.BackStoryN, "logN"));

			var allServers = new List<Server> {
				firstServer,
				secondServer,
				finalServer,
			};
			return (firstServer, allServers);
		}
	}
}