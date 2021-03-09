using System.Collections.Generic;
using CloudBreak.State;
using UniRx;
using UnityEngine;
using Zenject;

namespace CloudBreak.View {
	public sealed class CommandBlock : MonoBehaviour {
		CommandButton.Pool _pool;

		readonly List<CommandButton> _buttons = new List<CommandButton>();

		[Inject]
		public void Inject(CommandState state, CommandButton.Pool pool) {
			_pool = pool;
			state.AvailableCommands
				.ObserveAdd()
				.Subscribe(OnAddCommand);
			state.AvailableCommands
				.ObserveRemove()
				.Subscribe(OnRemoveCommand);
		}

		void OnAddCommand(CollectionAddEvent<Command> addEvent) {
			var command = addEvent.Value;
			var button  = _pool.Spawn(command);
			var trans   = button.transform;
			trans.SetParent(transform);
			trans.localScale = Vector3.one;
			_buttons.Add(button);
		}

		void OnRemoveCommand(CollectionRemoveEvent<Command> removeEvent) {
			var index  = removeEvent.Index;
			var button = _buttons[index];
			_pool.Despawn(button);
			_buttons.RemoveAt(index);
		}
	}
}