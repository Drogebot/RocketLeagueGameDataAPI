namespace RocketLeagueGameDataAPI
{
	/// <summary>
	/// The exception that is thrown when an unknown CommandType is encountered.
	/// </summary>
	public class UnknownCommandException : Exception
	{
		public UnknownCommandException() : base() { }
		public UnknownCommandException(string? message) : base(message) { }
		public UnknownCommandException(string? message, Exception? innerException) : base(message, innerException) { }
	}
}
