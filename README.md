[![NuGet version (RocketLeagueGameDataAPI)](https://img.shields.io/nuget/v/RocketLeagueGameDataAPI.svg?style=flat-square)](https://www.nuget.org/packages/RocketLeagueGameDataAPI/)
[![Donate](https://img.shields.io/badge/Dontate-Paypal-002f85)](https://www.paypal.com/paypalme/Drogings)
# [RocketLeagueGameDataAPI](https://github.com/Drogebot/RocketLeagueGameDataAPI)
A Rocket League Game Data API Wrapper written in C#. The wrapper allow you to connect to [the Rocket League Game Data API](https://www.rocketleague.com/en/developer/stats-api) on your local machine.
It will then convert the JSON event data transmitted over the TCP connection into `EventData` objects.

Supports all Rocket League versions up to at least [v2.70](https://www.rocketleague.com/news/rocket-league-patch-notes-v270-season-23-live/) (2026-06-10). Newer versions should keep working unless Psyonix adds a new `EventType` or `StatEvent`.

If you do at some point run into a problem, please create an issue so I can look into fixing it.

## Install
You can download [the NuGet package](https://www.nuget.org/packages/RocketLeagueGameDataAPI), or build from source.

## Usage
```cs
var rl = new RLGameDataAPI();
await rl.ConnectAsync();

while(rl.Connected) {
  var events = await rl.ReceiveEventsAsync();
  foreach (var e in events) {
    Console.WriteLine($"Received {e.EventType} for match {e.MatchGuid}!");
  }
}
```
There is [a small example](Examples/Program.cs) in Examples.
