using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when the game enters the podium state after the match ends.
	/// </summary>
	[JsonSerializable(typeof(Event_PodiumStart))]
	public class Event_PodiumStart : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.PodiumStart;
	}
}
