using RocketLeagueGameDataAPI;
using RocketLeagueGameDataAPI.Commands;
using RocketLeagueGameDataAPI.Events;
using System.Net.Sockets;
using System.Net.WebSockets;

namespace Examples
{
	internal class Program
	{
		private static int _lastHitPlayerId = 0;

		static async Task Main(string[] _)
		{
			using var rl = new RLGameDataAPIWS();

			Console.WriteLine("Trying to connect to game...");
			while (true)
			{
				try
				{
					await rl.ConnectAsync();
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
					var events = await rl.ReceiveEventsAsync();
					foreach (var e in events)
					{
						//Console.WriteLine($"Received {e.EventType} for match {e.MatchGuid}!");

						if(e is Event_BallHit ballHit)
						{
							var lastHitPlayer = ballHit.Players.Last();
							Console.WriteLine($"{lastHitPlayer.Name} hit the ball!");
							if (lastHitPlayer.Shortcut != _lastHitPlayerId)
							{
								Console.WriteLine($"Switching to {lastHitPlayer.Name}'s perspective!");
								await rl.SendCommandAsync(new Command_ChangePOV
								{
									Focus = lastHitPlayer.Shortcut.ToString(),
									Perspective = PerspectiveType.PlayerView,
								});
								_lastHitPlayerId = lastHitPlayer.Shortcut;
							}
						}
					}
				}
				catch (WebSocketException e)
				{
					if (e.InnerException?.InnerException is SocketException se)
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
