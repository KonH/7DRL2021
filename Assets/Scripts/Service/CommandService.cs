using System.Collections.Generic;
using CloudBreak.State;
using UnityEngine;

namespace CloudBreak.Service {
	public sealed class CommandService {
		readonly CommandState _state;

		public CommandService(CommandState state) {
			_state = state;
		}

		public void SetupAvailableCommands(IEnumerable<Command> commands) {
			while ( _state.AvailableCommands.Count > 0 ) {
				_state.AvailableCommands.RemoveAt(0);
			}
			foreach ( var command in commands ) {
				_state.AvailableCommands.Add(command);
			}
		}

		public void Execute(Command command) {
			switch ( command ) {
				case PlaceholderCommand _: {
					_state.CurrentChain.Push(command);
					_state.LastCommand.Value = command.Text;
					SetupAvailableCommands(command.NextCommands);
					break;
				}
				case BackCommand _: {
					_state.CurrentChain.Pop();
					var prevCommand = _state.CurrentChain.Peek();
					_state.LastCommand.Value = prevCommand.Text;
					SetupAvailableCommands(prevCommand.NextCommands);
					break;
				}
			}
			Debug.Log(
				$"{command} applied\n" +
				$"Current chain: {string.Join(", ", _state.CurrentChain)}\n" +
				$"Available commands: {string.Join(", ", _state.AvailableCommands)}");
		}
	}
}