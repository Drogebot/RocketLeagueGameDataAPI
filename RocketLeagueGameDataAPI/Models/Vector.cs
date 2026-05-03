namespace RocketLeagueGameDataAPI.Models
{
	/// <summary>
	/// 3D Vector
	/// </summary>
	public struct Vector
	{
		/// <summary>
		/// Towards left from Blue's perspective (team 0)
		/// </summary>
		public required float X { get; set; }
		/// <summary>
		/// Towards Orange's goal (team 1)
		/// </summary>
		public required float Y { get; set; }
		/// <summary>
		/// Upwards direction
		/// </summary>
		public required float Z { get; set; }
	}
}
