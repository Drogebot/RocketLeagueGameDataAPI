using RocketLeagueGameDataAPI.Models;
using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when someone earns a stat.
	/// </summary>
	[JsonSerializable(typeof(Event_StatfeedEvent))]
	public class Event_StatfeedEvent : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.StatfeedEvent;
		/// <summary>
		/// Asset name of the StatEvent (e.g. "Demolish", "Save").
		/// </summary>
		public required StatEvent EventName { get; set; }
		/// <summary>
		/// Localized display label for the stat (e.g. "Demolition").
		/// </summary>
		public required string Type { get; set; }
		/// <summary>
		/// Player who earned the stat.
		/// </summary>
		public required PlayerShortcut MainTarget { get; set; }
		/// <summary>
		/// Player involved in the stat (e.g. the demolished player). Same shape as MainTarget.
		/// </summary>
		/// <remarks>CONDITIONAL (Only for <see cref="StatEvent.Demolish"/>)</remarks>
		public PlayerShortcut? SecondaryTarget { get; set; }
	}

	public enum StatEvent
	{
		/// <summary>Asset name for Win.</summary>
		Win,
		/// <summary>Asset name for MVP.</summary>
		MVP,
		/// <summary>Asset name for Goal.</summary>
		Goal,
		/// <summary>Asset name for Aerial Goal.</summary>
		AerialGoal,
		/// <summary>Asset name for Backwards Goal.</summary>
		BackwardsGoal,
		/// <summary>Asset name for Bicycle Goal.</summary>
		BicycleGoal,
		/// <summary>Asset name for Long Goal.</summary>
		LongGoal,
		/// <summary>Asset name for Turtle Goal.</summary>
		TurtleGoal,
		/// <summary>Asset name for Pool Shot.</summary>
		PoolShot,
		/// <summary>Asset name for Overtime Goal.</summary>
		OvertimeGoal,
		/// <summary>Asset name for Hat Trick.</summary>
		HatTrick,
		/// <summary>Asset name for Assist.</summary>
		Assist,
		/// <summary>Asset name for Playmaker.</summary>
		Playmaker,
		/// <summary>Asset name for Save.</summary>
		Save,
		/// <summary>Asset name for Epic Save.</summary>
		EpicSave,
		/// <summary>Asset name for Savior.</summary>
		Savior,
		/// <summary>Asset name for Shot on Goal.</summary>
		Shot,
		/// <summary>Asset name for Demolition.</summary>
		Demolish,
		/// <summary>Asset name for Extermination.</summary>
		Demolition,
		/// <summary>Asset name for Damage.</summary>
		BreakoutDamage,
		/// <summary>Asset name for Ultra Damage.</summary>
		BreakoutDamageLarge,
		/// <summary>Asset name for Low Five.</summary>
		LowFive,
		/// <summary>Asset name for High Five.</summary>
		HighFive,
		/// <summary>Asset name for Swish Goal.</summary>
		HoopsSwishGoal,
		/// <summary>Asset name for Flip Reset.</summary>
		FlipReset,
	}
}
