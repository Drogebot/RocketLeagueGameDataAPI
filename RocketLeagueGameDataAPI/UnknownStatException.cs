namespace RocketLeagueGameDataAPI
{
	/// <summary>
	/// The exception that is thrown when an unknown StatEvent is encountered.
	/// </summary>
	public class UnknownStatException : Exception
	{
		public UnknownStatException() : base() { }
		public UnknownStatException(string? message) : base(message) { }
		public UnknownStatException(string? message, Exception? innerException) : base(message, innerException) { }
	}
}
