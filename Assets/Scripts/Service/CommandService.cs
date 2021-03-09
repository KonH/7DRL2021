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

		public void Execute(Command command, bool hidden = false) {
			switch ( command ) {
				case ExploreCommand _: {
					_commandState.CurrentChain.Push(command);
					if ( !hidden ) {
						_commandState.LastCommand.Value = command.Text;
					}
					var commands = _serverState.CurrentServer.Value.Files
						.Select(f => (Command)new DownloadCommand(f))
						.ToList();
					commands.Add(new BackCommand());
					SetupAvailableCommands(commands);
					break;
				}
				case DownloadCommand download: {
					if ( !hidden ) {
						_commandState.LastCommand.Value = command.Text;
					}
					var file  = download.File;
					var files = _serverState.CurrentServer.Value.Files;
					files.Remove(file);
					var commands = files
						.Select(f => (Command)new DownloadCommand(f))
						.Append(new BackCommand());
					SetupAvailableCommands(commands);
					_inventoryService.AddFile(file);
					break;
				}
				case ConnectCommand _: {
					_commandState.CurrentChain.Push(command);
					if ( !hidden ) {
						_commandState.LastCommand.Value = command.Text;
					}
					var links = _serverState.CurrentServer.Value.Links;
					var commands = links
						.Where(s => _serverState.AvailableServers.Contains(s))
						.Select(s => (Command) new ChangeServerCommand(s))
						.Append(new BackCommand());
					SetupAvailableCommands(commands);
					break;
				}
				case ChangeServerCommand changeServer: {
					_commandState.CurrentChain.Push(command);
					if ( !hidden ) {
						_commandState.LastCommand.Value = command.Text;
					}
					_serverState.CurrentServer.Value = changeServer.Server;
					var commands = new Command[] {
						new ConnectCommand(),
						new ExploreCommand(),
					};
					SetupAvailableCommands(commands);
					break;
				}
				case BackCommand _: {
					_commandState.CurrentChain.Pop();
					var prevCommand = _commandState.CurrentChain.Pop();
					Execute(prevCommand);
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