[![NuGet version (RocketLeagueGameDataAPI)](https://img.shields.io/nuget/v/RocketLeagueGameDataAPI.svg?style=flat-square)](https://www.nuget.org/packages/RocketLeagueGameDataAPI/)
[![Donate](https://img.shields.io/badge/Dontate-Paypal-002f85)](https://www.paypal.com/paypalme/Drogings)
# [RocketLeagueGameDataAPI](https://github.com/Drogebot/RocketLeagueGameDataAPI)
A Rocket League Game Data API Wrapper written in C#. The wrapper allow you to connect to [the Rocket League Game Data API](https://www.rocketleague.com/en/developer/stats-api) on your local machine.
It will then convert the JSON event data transmitted over the WebSocket connection into `EventData` objects.
The API also supports sending Commands to the game over the WebSocket using the `CommandData` objects.

Supports the latest Rocket League versions down to [v2.72](https://www.rocketleague.com/news/rocket-league-patch-notes-v2-72) (2026-08-04). Newer versions should keep working unless Psyonix adds a new `EventType`, `CommandType` or `StatEvent`.

If you do at some point run into a problem, please create an issue so I can look into fixing it.

## Install
You can download [the NuGet package](https://www.nuget.org/packages/RocketLeagueGameDataAPI), or build from source.

## Usage
```cs
var rl = new RLGameDataAPIWS();
await rl.ConnectAsync();

while(rl.Connected) {
  var events = await rl.ReceiveEventsAsync();
  foreach (var e in events) {
    Console.WriteLine($"Received {e.EventType} for match {e.MatchGuid}!");
  }
}
```
There is [a small example](Examples/Program.cs) in Examples.
