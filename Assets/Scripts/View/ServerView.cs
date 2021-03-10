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

		ServerState   _state;
		Server        _server;
		LinkView.Pool _linkPool;

		[Inject]
		public void Inject(ServerState state, LinkView.Pool linkPool) {
			_state = state;
			state.CurrentServer
				.Subscribe(OnCurrentServerChange);
			_linkPool = linkPool;
		}

		void Reinitialize(Server server) {
			gameObject.name = server.Address;
			_server = server;
			SetupName(_state.CurrentServer.Value);
			SetupIcon(_server.Files);
			_server
				.Files.ObserveRemove()
				.Subscribe(OnFileRemoved);
			_state.AvailableServers
				.ObserveAdd()
				.Subscribe(OnNewAvailableServer);
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

		public void AddLinks() {
			foreach ( var server in _server.Links ) {
				if ( _state.AvailableServers.Contains(server) ) {
					AddLink(server);
				}
			}
		}

		void OnNewAvailableServer(CollectionAddEvent<Server> addEvent) {
			var server = addEvent.Value;
			if ( _server.Links.Contains(server) ) {
				AddLink(server);
			}
		}

		void AddLink(Server server) {
			var link  = _linkPool.Spawn(_server, server);
			var trans = link.transform;
			trans.SetParent(transform.parent);
			trans.localScale = Vector3.one;
			trans.localPosition = Vector3.zero;
		}
	}
}