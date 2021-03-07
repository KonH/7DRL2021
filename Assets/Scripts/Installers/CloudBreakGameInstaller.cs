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

		public override void InstallBindings() {
			Container.BindInstance(_messageSetup).AsSingle();

			Container.Bind<UIState>().AsSingle();
			Container.Bind<MessageState>().AsSingle();
			Container.Bind<CommandState>().AsSingle();

			Container.Bind<UIService>().AsSingle();
			Container.Bind<MessageService>().AsSingle();
			Container.Bind<CommandService>().AsSingle();

			Container.BindMemoryPool<InboxMessageView, InboxMessageView.Pool>()
				.FromComponentInNewPrefab(_inboxMessagePrefab);

			Container.BindMemoryPool<CommandButton, CommandButton.Pool>()
				.FromComponentInNewPrefab(_commandButtonPrefab);
		}
	}
}