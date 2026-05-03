using RocketLeagueGameDataAPI.Events;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI
{
	[JsonSerializable(typeof(EventMessage))]
	internal class EventMessage
	{
		public required EventType Event { get; set; }
		public required string Data { get; set; }

		public EventData? DeserializeEventData(JsonSerializerOptions? options = null)
		{
			using var stream = new MemoryStream(Encoding.UTF8.GetBytes(Data));
			return Event switch
			{
				EventType.UpdateState =>  JsonSerializer.Deserialize<Event_UpdateState>(stream, options),
				EventType.BallHit =>  JsonSerializer.Deserialize<Event_BallHit>(stream, options),
				EventType.ClockUpdatedSeconds =>  JsonSerializer.Deserialize<Event_ClockUpdatedSeconds>(stream, options),
				EventType.CountdownBegin =>  JsonSerializer.Deserialize<Event_CountdownBegin>(stream, options),
				EventType.CrossbarHit =>  JsonSerializer.Deserialize<Event_CrossbarHit>(stream, options),
				//EventType.GoalReplayEnd =>  JsonSerializer.Deserialize<Event_GoalReplayEnd>(stream, options),
				EventType.ReplayPlaybackEnd =>  JsonSerializer.Deserialize<Event_GoalReplayEnd>(stream, options),
				//EventType.GoalReplayStart =>  JsonSerializer.Deserialize<Event_GoalReplayStart>(stream, options),
				EventType.ReplayPlaybackStart =>  JsonSerializer.Deserialize<Event_GoalReplayStart>(stream, options),
				//EventType.GoalReplayWillEnd =>  JsonSerializer.Deserialize<Event_GoalReplayWillEnd>(stream, options),
				EventType.ReplayWillEnd =>  JsonSerializer.Deserialize<Event_GoalReplayWillEnd>(stream, options),
				EventType.GoalScored =>  JsonSerializer.Deserialize<Event_GoalScored>(stream, options),
				EventType.MatchCreated =>  JsonSerializer.Deserialize<Event_MatchCreated>(stream, options),
				EventType.MatchInitialized =>  JsonSerializer.Deserialize<Event_MatchInitialized>(stream, options),
				EventType.MatchDestroyed =>  JsonSerializer.Deserialize<Event_MatchDestroyed>(stream, options),
				EventType.MatchEnded =>  JsonSerializer.Deserialize<Event_MatchEnded>(stream, options),
				EventType.MatchPaused =>  JsonSerializer.Deserialize<Event_MatchPaused>(stream, options),
				EventType.MatchUnpaused =>  JsonSerializer.Deserialize<Event_MatchUnpaused>(stream, options),
				EventType.PodiumStart =>  JsonSerializer.Deserialize<Event_PodiumStart>(stream, options),
				EventType.ReplayCreated =>  JsonSerializer.Deserialize<Event_ReplayCreated>(stream, options),
				EventType.RoundStarted =>  JsonSerializer.Deserialize<Event_RoundStarted>(stream, options),
				EventType.StatfeedEvent =>  JsonSerializer.Deserialize<Event_StatfeedEvent>(stream, options),
				_ => throw new UnknownEventException($"Unexpected Event {Event}"),
			};
		}

		public async ValueTask<EventData?> DeserializeEventDataAsync(JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
		{
			using var stream = new MemoryStream(Encoding.UTF8.GetBytes(Data));
			return Event switch
			{
				EventType.UpdateState => await JsonSerializer.DeserializeAsync<Event_UpdateState>(stream, options, cancellationToken),
				EventType.BallHit => await JsonSerializer.DeserializeAsync<Event_BallHit>(stream, options, cancellationToken),
				EventType.ClockUpdatedSeconds => await JsonSerializer.DeserializeAsync<Event_ClockUpdatedSeconds>(stream, options, cancellationToken),
				EventType.CountdownBegin => await JsonSerializer.DeserializeAsync<Event_CountdownBegin>(stream, options, cancellationToken),
				EventType.CrossbarHit => await JsonSerializer.DeserializeAsync<Event_CrossbarHit>(stream, options, cancellationToken),
				//EventType.GoalReplayEnd => await JsonSerializer.DeserializeAsync<Event_GoalReplayEnd>(stream, options, cancellationToken),
				EventType.ReplayPlaybackEnd => await JsonSerializer.DeserializeAsync<Event_GoalReplayEnd>(stream, options, cancellationToken),
				//EventType.GoalReplayStart => await JsonSerializer.DeserializeAsync<Event_GoalReplayStart>(stream, options, cancellationToken),
				EventType.ReplayPlaybackStart => await JsonSerializer.DeserializeAsync<Event_GoalReplayStart>(stream, options, cancellationToken),
				//EventType.GoalReplayWillEnd => await JsonSerializer.DeserializeAsync<Event_GoalReplayWillEnd>(stream, options, cancellationToken),
				EventType.ReplayWillEnd => await JsonSerializer.DeserializeAsync<Event_GoalReplayWillEnd>(stream, options, cancellationToken),
				EventType.GoalScored => await JsonSerializer.DeserializeAsync<Event_GoalScored>(stream, options, cancellationToken),
				EventType.MatchCreated => await JsonSerializer.DeserializeAsync<Event_MatchCreated>(stream, options, cancellationToken),
				EventType.MatchInitialized => await JsonSerializer.DeserializeAsync<Event_MatchInitialized>(stream, options, cancellationToken),
				EventType.MatchDestroyed => await JsonSerializer.DeserializeAsync<Event_MatchDestroyed>(stream, options, cancellationToken),
				EventType.MatchEnded => await JsonSerializer.DeserializeAsync<Event_MatchEnded>(stream, options, cancellationToken),
				EventType.MatchPaused => await JsonSerializer.DeserializeAsync<Event_MatchPaused>(stream, options, cancellationToken),
				EventType.MatchUnpaused => await JsonSerializer.DeserializeAsync<Event_MatchUnpaused>(stream, options, cancellationToken),
				EventType.PodiumStart => await JsonSerializer.DeserializeAsync<Event_PodiumStart>(stream, options, cancellationToken),
				EventType.ReplayCreated => await JsonSerializer.DeserializeAsync<Event_ReplayCreated>(stream, options, cancellationToken),
				EventType.RoundStarted => await JsonSerializer.DeserializeAsync<Event_RoundStarted>(stream, options, cancellationToken),
				EventType.StatfeedEvent => await JsonSerializer.DeserializeAsync<Event_StatfeedEvent>(stream, options, cancellationToken),
				_ => throw new UnknownEventException($"Unexpected Event {Event}"),
			};
		}
	}

	public enum EventType
	{
		/// <summary>Sent X amount of times per second based on the player's PacketSendRate preference.</summary>
		UpdateState,
		/// <summary>Sent one frame after the ball is hit.</summary>
		BallHit,
		/// <summary>Sent when the in-game clock has changed.</summary>
		ClockUpdatedSeconds,
		/// <summary>Sent at the start of each round when the countdown starts.</summary>
		CountdownBegin,
		/// <summary>Sent when the ball hits a crossbar.</summary>
		CrossbarHit,
		//GoalReplayEnd,	These 3 are wrong in the API docs
		//GoalReplayStart,
		//GoalReplayWillEnd,
		/// <summary>Sent when a goal replay ends.</summary>
		ReplayPlaybackEnd,
		/// <summary>Sent when a goal replay starts.</summary>
		ReplayPlaybackStart,
		/// <summary>Sent when the ball explodes during a goal replay. If the replay is skipped this event will not fire.</summary>
		ReplayWillEnd,
		/// <summary>Sent when a goal is scored.</summary>
		GoalScored,
		/// <summary>Sent when all teams are created and replicated.</summary>
		MatchCreated,
		/// <summary>Sent when the first countdown starts.</summary>
		MatchInitialized,
		/// <summary>Sent when leaving the game.</summary>
		MatchDestroyed,
		/// <summary>Sent when the match ends and a winner is chosen.</summary>
		MatchEnded,
		/// <summary>Sent when the game is paused by a match admin.</summary>
		MatchPaused,
		/// <summary>Sent when the game is unpaused by a match admin.</summary>
		MatchUnpaused,
		/// <summary>Sent when the game enters the podium state after the match ends.</summary>
		PodiumStart,
		/// <summary>Sent when a replay is initialized. Does not pertain to goal replays, only replays you load via the Match History menu.</summary>
		ReplayCreated,
		/// <summary>Sent when the game enters the active state (after the countdown finishes).</summary>
		RoundStarted,
		/// <summary>Sent when someone earns a stat.</summary>
		StatfeedEvent,
	}
}
