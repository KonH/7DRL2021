using CloudBreak.Configuration;
using CloudBreak.Service;
using CloudBreak.State;
using CloudBreak.View;
using UnityEngine;
using Zenject;

namespace CloudBreak.Installers {
	public sealed class CloudBreakGameInstaller : MonoInstaller {
		[SerializeField] MessageSetup     _messageSetup;
		[SerializeField] InboxMessageView _inboxMessagePrefab;
		[SerializeField] CommandButton    _commandButtonPrefab;
		[SerializeField] ServerView       _serverViewPrefab;

		public override void InstallBindings() {
			Container.BindInstance(_messageSetup).AsSingle();

			Container.Bind<UIState>().AsSingle();
			Container.Bind<MessageState>().AsSingle();
			Container.Bind<CommandState>().AsSingle();
			Container.Bind<ServerState>().AsSingle();
			Container.Bind<InventoryState>().AsSingle();

			Container.Bind<UIService>().AsSingle();
			Container.Bind<MessageService>().AsSingle();
			Container.Bind<CommandService>().AsSingle();
			Container.Bind<InventoryService>().AsSingle();

			Container.BindMemoryPool<InboxMessageView, InboxMessageView.Pool>()
				.FromComponentInNewPrefab(_inboxMessagePrefab);
			Container.BindMemoryPool<CommandButton, CommandButton.Pool>()
				.FromComponentInNewPrefab(_commandButtonPrefab);
			Container.BindMemoryPool<ServerView, ServerView.Pool>()
				.FromComponentInNewPrefab(_serverViewPrefab);
		}
	}
}