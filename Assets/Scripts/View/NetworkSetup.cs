using System;
using System.Collections.Generic;
using CloudBreak.Configuration;
using CloudBreak.State;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CloudBreak.View {
	public static class NetworkSetup {
		public static (Server rootServer, List<Server> allServers) Apply() {
			var grid       = new HashSet<Vector2>();
			var allServers = new List<Server>();

			var firstServer  = AddServer("john.doe.smuggle.xx", grid, allServers);
			var secondServer = AddServer("ci.smuggle.xx", grid, allServers);
			var finalServer  = AddServer("gateway.smuggle.xx", grid, allServers);

			var allKeys = new HashSet<Server> {
				firstServer
			};
			CreateSimpleRoute(allKeys, firstServer, secondServer);

			secondServer.Files.Add(new ServerMessage(MessageSetup.TemplateId.MainStory1, "note1"));
			secondServer.Files.Add(new ServerMessage(MessageSetup.TemplateId.BackStory1, "log1"));

			finalServer.Files.Add(new ServerMessage(MessageSetup.TemplateId.MainStoryFinal, "congrats"));

			CreateBranchRoutes(
				grid, allServers, allKeys,
				firstServer, finalServer,
				mainStoryServerCount: 3,
				backStoryServerCount: 3,
				maxBranchSize: 2);

			return (firstServer, allServers);
		}

		static Server AddServer(string address, HashSet<Vector2> grid, List<Server> allServers) {
			var server = new Server(address, GetPosition(grid));
			allServers.Add(server);
			return server;
		}

		static void CreateSimpleRoute(HashSet<Server> allKeys, params Server[] servers) {
			for ( var i = 0; i < servers.Length; i++ ) {
				var curServer  = servers[i];
				var prevServer = (i > 0) ? servers[i - 1] : null;
				var nextServer = (i < servers.Length - 1) ? servers[i + 1] : null;
				TryAddRoute(curServer, prevServer, allKeys);
				TryAddRoute(curServer, nextServer, allKeys);
			}
		}

		static void CreateBranchRoutes(
			HashSet<Vector2> grid, List<Server> allServers, HashSet<Server> allKeys,
			Server firstServer, Server finalServer,
			int mainStoryServerCount, int backStoryServerCount, int maxBranchSize) {
			var usedNames             = new HashSet<string>();
			var totalBackStoryServers = 0;
			var mainStoryServers      = new List<Server>();
			var branches              = new List<List<Server>>();
			for ( var i = 0; i < mainStoryServerCount; i++ ) {
				var mainStoryServer = AddServer(GenerateAddress(usedNames), grid, allServers);
				mainStoryServer.Files.Add(new ServerMessage(
					(MessageSetup.TemplateId)Enum.Parse(typeof(MessageSetup.TemplateId), $"MainStory{2 + i}"),
					$"note{2 + i}"));
				mainStoryServers.Add(mainStoryServer);
				var curBranchSize = Math.Min(Random.Range(1, maxBranchSize), backStoryServerCount - totalBackStoryServers);
				var branch        = new List<Server>();
				for ( var j = 0; j < curBranchSize; j++ ) {
					var backStoryServer = AddServer(GenerateAddress(usedNames), grid, allServers);
					backStoryServer.Files.Add(new ServerMessage(
						(MessageSetup.TemplateId)Enum.Parse(typeof(MessageSetup.TemplateId), $"BackStory{2 + j}"),
						$"log{2 + j}"));
					branch.Add(backStoryServer);
				}
				branches.Add(branch);
				totalBackStoryServers += curBranchSize;
			}
			CreateSimpleRoute(allKeys, firstServer, mainStoryServers[0]);
			for ( var i = 0; i < branches.Count; i++ ) {
				var targetServer = (i < mainStoryServers.Count - 1) ? mainStoryServers[i + 1] : finalServer;
				if ( branches[i].Count == 0 ) {
					CreateSimpleRoute(allKeys, mainStoryServers[i], targetServer);
					continue;
				}
				var branch = branches[i];
				foreach ( var b in branch ) {
					CreateSimpleRoute(allKeys, mainStoryServers[i], b);
				}
				var mainStoryLinkIndex = Random.Range(0, branch.Count);
				CreateSimpleRoute(allKeys, branch[mainStoryLinkIndex], targetServer);
			}
		}

		static string GenerateAddress(HashSet<string> usedNames) {
			while ( true ) {
				var name = GenerateAddress();
				if ( usedNames.Add(name) ) {
					return name;
				}
			}
		}

		static string GenerateAddress() {
			var domains = new[] {
				"smuggle",
				"under",
				"quack",
				"duck",
				"lazy",
				"forge",
				"drone",
				"owin"
			};
			var subdomains = new[] {
				"",
				"web",
				"db",
				"mail",
				"qa",
				"dev",
				"st"
			};
			var domain    = domains[Random.Range(0, domains.Length)];
			var subdomain = subdomains[Random.Range(0, subdomains.Length)];
			var name      = (string.IsNullOrEmpty(subdomain)) ? domain : $"{subdomain}.{domain}";
			return $"{name}.xx";
		}

		static void TryAddRoute(Server curServer, Server nextServer, HashSet<Server> allKeys) {
			if ( nextServer == null ) {
				return;
			}
			if ( curServer.Links.Contains(nextServer) ) {
				return;
			}
			curServer.Links.Add(nextServer);
			if ( allKeys.Add(nextServer) ) {
				curServer.Files.Add(new ServerKey(nextServer.Address));
			}
		}

		static Vector2 GetPosition(HashSet<Vector2> grid) {
			var offset = 0;
			var tries  = 0;
			while ( true ) {
				var position = new Vector2(Random.Range(-offset, offset + 1), Random.Range(-offset, offset + 1));
				if ( grid.Add(position) ) {
					var origin = new Vector2(position.x * 200, position.y * 150);
					return origin + new Vector2(Random.Range(-30, 30), Random.Range(-20, 20));
				}
				tries++;
				if ( tries <= offset * 3 ) {
					continue;
				}
				offset++;
				tries = 0;
			}
		}
	}
}