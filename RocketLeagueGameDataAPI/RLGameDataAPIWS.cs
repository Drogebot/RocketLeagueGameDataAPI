using RocketLeagueGameDataAPI.Commands;
using RocketLeagueGameDataAPI.Events;
using RocketLeagueGameDataAPI.JsonConverters;
using System.Net;
using System.Net.WebSockets;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI
{
	/// <summary>
	/// Simple wrapper for the Rocket League Game Data API using WebSocket.
	/// </summary>
	public class RLGameDataAPIWS : IDisposable
	{
		public const int gamePort = 49124;

		/// <summary>
		/// Gets a value indicating whether the underlying <see cref="ClientWebSocket"/> is connected to the game.
		/// </summary>
		public bool Connected => _webSocketClient.State == WebSocketState.Open;

		private ClientWebSocket _webSocketClient;
		private JsonSerializerOptions? _jsonOptions;
		private byte[] _buffer;
		private bool _disposed;

		public RLGameDataAPIWS()
		{
			_webSocketClient = new ClientWebSocket();
			_jsonOptions = new JsonSerializerOptions()
			{
				RespectNullableAnnotations = true,
				UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
			};
			_jsonOptions.Converters.Add(new JsonUniqueNetIdConverter());
			_jsonOptions.Converters.Add(new JsonDateTimeConverter());
			_jsonOptions.Converters.Add(new JsonStringEnumConverter<EventType>());
			_jsonOptions.Converters.Add(new JsonStringEnumConverter<StatEvent>());
			_jsonOptions.Converters.Add(new JsonStringEnumConverter<BoostType>());
			_jsonOptions.Converters.Add(new JsonStringEnumConverter<CommandType>());
			_jsonOptions.Converters.Add(new JsonStringEnumConverter<PerspectiveType>());
			_jsonOptions.MakeReadOnly(true);
			_buffer = new byte[1024 * 4];
		}

		public void Dispose()
		{
			if (!_disposed)
			{
				_disposed = true;
				_buffer = [];
				_jsonOptions = null;
				_webSocketClient.Dispose();
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>
		/// Connects to the local game using the specified port as an asynchronous operation.
		/// </summary>
		/// <param name="port">The port number specified in your DefaultStatsAPI.ini.</param>
		/// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
		/// <exception cref="OperationCanceledException"/>
		public async ValueTask ConnectAsync(int port = gamePort, CancellationToken cancellationToken = default)
		{
			await _webSocketClient.ConnectAsync(new Uri($"ws://{IPAddress.Loopback}:{port}"), cancellationToken);
		}

		/// <summary>
		/// Disposes this <see cref="RLGameDataAPIWS"/> instance and requests that the underlying WebSocket connection be closed.
		/// </summary>
		public async ValueTask CloseAsync()
		{
			await _webSocketClient.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
		}

		/// <summary>
		/// Reads all available data from the <see cref="ClientWebSocket"/> and converts it to their corresponding <see cref="EventData"/> as an asynchronous operation.
		/// </summary>
		/// <returns>A <see cref="Task"/> representing the asynchronous read operation. The value of its result contains a <see cref="List{T}"/> of received <see cref="EventData"/>.</returns>
		/// <exception cref="InvalidOperationException"/>
		/// <exception cref="ObjectDisposedException"/>
		/// <exception cref="OperationCanceledException"/>
		/// <exception cref="IOException"/>
		/// <exception cref="UnknownEventException"/>
		public async ValueTask<List<EventData>> ReceiveEventsAsync(CancellationToken cancellationToken = default)
		{
			using var received = new MemoryStream();
			ValueWebSocketReceiveResult result;

			do
			{
				result = await _webSocketClient.ReceiveAsync(_buffer.AsMemory(0, _buffer.Length), cancellationToken);
				await received.WriteAsync(_buffer.AsMemory(0, result.Count), cancellationToken);
			}
			while (!result.EndOfMessage);

			received.Seek(0, SeekOrigin.Begin);
			return await ProccessMessageAsync(received, cancellationToken);
		}

		/// <summary>
		/// Writes a <see cref="CommandData"/> to the <see cref="ClientWebSocket"/> to be processed by the game as an asynchronous operation.
		/// </summary>
		/// <param name="command">The command to be sent.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous write operation.</returns>
		/// <exception cref="UnknownCommandException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		/// <exception cref="ObjectDisposedException"></exception>
		/// <exception cref="OperationCanceledException"/>
		public async ValueTask SendCommandAsync(CommandData command, CancellationToken cancellationToken = default)
		{
			var commandMessage = CommandMessage.CreateCommandMessage(command, _jsonOptions);
			var buffer = await commandMessage.SerializeCommandMessageAsync(_jsonOptions, cancellationToken);
			await _webSocketClient.SendAsync(buffer.AsMemory(), WebSocketMessageType.Text, true, cancellationToken);
		}

		private async ValueTask<List<EventData>> ProccessMessageAsync(MemoryStream dataStream, CancellationToken cancellationToken = default)
		{
			var dataLength = (int)dataStream.Length;
			var data = dataStream.GetBuffer();
			var events = new List<EventData>();
			var totalConsumed = 0;
			while (totalConsumed < dataLength)
			{
				var reader = new Utf8JsonReader(new ReadOnlySpan<byte>(data, totalConsumed, dataLength - totalConsumed));
				var eventMessage = JsonSerializer.Deserialize<EventMessage>(ref reader, _jsonOptions);
				totalConsumed += (int)reader.BytesConsumed;
				if (eventMessage is null) continue;

				var eventData = await eventMessage.DeserializeEventDataAsync(_jsonOptions, cancellationToken);
				if (eventData is null) continue;

				events.Add(eventData);
			}

			return events;
		}
	}
}
