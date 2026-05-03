namespace RocketLeagueGameDataAPI
{
	/// <summary>
	/// The exception that is thrown when an unknown EventType is encountered.
	/// </summary>
	public class UnknownEventException : Exception
	{
		public UnknownEventException() : base() { }
		public UnknownEventException(string? message) : base(message) { }
		public UnknownEventException(string? message, Exception? innerException) : base(message, innerException) { }
	}
}
