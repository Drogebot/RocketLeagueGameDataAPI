using RocketLeagueGameDataAPI.Commands;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI
{
	internal abstract class CommandMessage
	{
		public static CommandMessage CreateCommandMessage(CommandData command, JsonSerializerOptions? options = null)
		{
			return command.CommandType switch
			{
				CommandType.ChangePOV => CommandMessage<Command_ChangePOV>.CreateCommandMessage((Command_ChangePOV)command, options),
				CommandType.LoadReplay => CommandMessage<Command_LoadReplay>.CreateCommandMessage((Command_LoadReplay)command, options),
				CommandType.SeekReplay => CommandMessage<Command_SeekReplay>.CreateCommandMessage((Command_SeekReplay)command, options),
				CommandType.SetGameSpeed => CommandMessage<Command_SetGameSpeed>.CreateCommandMessage((Command_SetGameSpeed)command, options),
				CommandType.SetHUDVisibility => CommandMessage<Command_SetHUDVisibility>.CreateCommandMessage((Command_SetHUDVisibility)command, options),
				CommandType.SetMatchPaused => CommandMessage<Command_SetMatchPaused>.CreateCommandMessage((Command_SetMatchPaused)command, options),
				_ => throw new UnknownCommandException($"Unexpected Command {command}"),
			};
		}

		public abstract Memory<byte> SerializeCommandMessage(JsonSerializerOptions? options = null);
		public abstract ValueTask<byte[]> SerializeCommandMessageAsync(JsonSerializerOptions? options = null, CancellationToken cancellationToken = default);
	}

	[JsonSerializable(typeof(CommandMessage<>))]
	internal class CommandMessage<T> : CommandMessage where T : CommandData
	{
		public required CommandType Command { get; set; }
		public required T Data { get; set; }

		internal static CommandMessage<T> CreateCommandMessage(T command, JsonSerializerOptions? options = null)
		{
			return new CommandMessage<T>()
			{
				Command = command.CommandType,
				Data = command,
			};
		}

		public override Memory<byte> SerializeCommandMessage(JsonSerializerOptions? options = null)
		{
			return JsonSerializer.SerializeToUtf8Bytes(this, options);
		}

		public override async ValueTask<byte[]> SerializeCommandMessageAsync(JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
		{
			using var stream = new MemoryStream();
			await JsonSerializer.SerializeAsync(stream, this, options, cancellationToken);
			return stream.ToArray();
		}
	}

	public enum CommandType
	{
		/// <summary>Changes the spectator/replay viewpoint. At least one of Focus or Perspective must be supplied; both may be combined.</summary>
		ChangePOV,
		/// <summary>Loads and begins playback of a replay by file name or by file path.</summary>
		LoadReplay,
		/// <summary>Jumps replay playback to a target frame or time.</summary>
		SeekReplay,
		/// <summary>Sets replay playback speed.</summary>
		SetGameSpeed,
		/// <summary>Shows or hides the in-game HUD.</summary>
		SetHUDVisibility,
		/// <summary>Pauses or unpauses the current match or replay.</summary>
		SetMatchPaused,
	}
}
