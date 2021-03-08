using System.Collections.Generic;
using System.Linq;
using CloudBreak.State;
using UnityEngine;

namespace CloudBreak.Service {
	public sealed class CommandService {
		readonly CommandState     _commandState;
		readonly ServerState      _serverState;
		readonly InventoryService _inventoryService;

		public CommandService(CommandState commandState, ServerState serverState, InventoryService inventoryService) {
			_commandState     = commandState;
			_serverState      = serverState;
			_inventoryService = inventoryService;
		}

		public void SetupAvailableCommands(IEnumerable<Command> commands) {
			while ( _commandState.AvailableCommands.Count > 0 ) {
				_commandState.AvailableCommands.RemoveAt(0);
			}
			foreach ( var command in commands ) {
				_commandState.AvailableCommands.Add(command);
			}
		}

		public void Execute(Command command) {
			switch ( command ) {
				case PlaceholderCommand _: {
					_commandState.CurrentChain.Push(command);
					_commandState.LastCommand.Value = command.Text;
					SetupAvailableCommands(command.NextCommands);
					break;
				}
				case ExploreCommand _: {
					_commandState.CurrentChain.Push(command);
					_commandState.LastCommand.Value = command.Text;
					var commands = _serverState.CurrentServer.Files
						.Select(f => (Command)new DownloadCommand(f))
						.ToList();
					commands.Add(new BackCommand());
					SetupAvailableCommands(commands);
					break;
				}
				case DownloadCommand download: {
					var file  = download.File;
					var files = _serverState.CurrentServer.Files;
					files.Remove(file);
					var commands = files
						.Select(f => (Command)new DownloadCommand(f))
						.ToList();
					commands.Add(new BackCommand());
					SetupAvailableCommands(commands);
					_inventoryService.AddFile(file);
					break;
				}
				case BackCommand _: {
					_commandState.CurrentChain.Pop();
					var prevCommand = _commandState.CurrentChain.Peek();
					_commandState.LastCommand.Value = prevCommand.Text;
					SetupAvailableCommands(prevCommand.NextCommands);
					break;
				}
			}
			Debug.Log(
				$"{command} applied\n" +
				$"Current chain: {string.Join(", ", _commandState.CurrentChain)}\n" +
				$"Available commands: {string.Join(", ", _commandState.AvailableCommands)}");
		}
	}
}