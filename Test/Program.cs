using RocketLeagueGameDataAPI;
using System.Net.Sockets;

namespace Test
{
	internal class Program
	{
		static void Main(string[] _)
		{
			using var rl = new RLGameDataAPI();

			Console.WriteLine("Trying to connect to game...");
			while (true)
			{
				try
				{
					rl.Connect();
					break;
				}
				catch (SocketException)
				{
					Thread.Sleep(1000);
					Console.WriteLine("Retrying...");
				}

			}
			Console.WriteLine("Connected to the game!");

			Console.WriteLine("Reading...");
			while (rl.Connected)
			{
				try
				{
					var events = rl.ReceiveEvents();
					foreach (var e in events)
					{
						Console.WriteLine($"Received {e.EventType} for match {e.MatchGuid}!");
					}
				}
				catch (IOException e)
				{
					if (e.InnerException is SocketException se)
					{
						Console.WriteLine("Game connection was foribly closed by game!");
						break;
					}
				}
			}

			Console.WriteLine("Closing...");
			Thread.Sleep(1000);
		}
	}
}
