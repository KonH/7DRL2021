using System.Collections.Generic;
using CloudBreak.State;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;

namespace CloudBreak.View {
	public sealed class ServerView : MonoBehaviour {
		public sealed class Pool : MonoMemoryPool<Server, ServerView> {
			protected override void Reinitialize(Server server, ServerView view) {
				view.Reinitialize(server);
			}
		}

		[SerializeField] Color    _emptyColor;
		[SerializeField] Color    _containsColor;
		[SerializeField] Image    _icon;
		[SerializeField] TMP_Text _addressText;

		ServerState _state;
		Server      _server;

		[Inject]
		public void Inject(ServerState state) {
			_state = state;
			state.CurrentServer
				.Subscribe(OnCurrentServerChange);
		}

		void Reinitialize(Server server) {
			_server = server;
			SetupName(_state.CurrentServer.Value);
			SetupIcon(_server.Files);
			_server
				.Files.ObserveRemove()
				.Subscribe(OnFileRemoved);
		}

		void OnCurrentServerChange(Server server) => SetupName(server);

		void SetupName(Server currentServer) {
			var baseName   = _server?.Address;
			var actualName = (_server == currentServer) ? $"[{baseName}]" : baseName;
			_addressText.text = actualName;
		}

		void OnFileRemoved(CollectionRemoveEvent<ServerFile> removeEvent) => SetupIcon(_server.Files);

		void SetupIcon(ICollection<ServerFile> files) {
			var color = (files.Count > 0) ? _containsColor : _emptyColor;
			_icon.color = color;
		}
	}
}