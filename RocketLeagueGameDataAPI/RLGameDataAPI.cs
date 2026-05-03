using RocketLeagueGameDataAPI.Events;
using RocketLeagueGameDataAPI.JsonConverters;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI
{
	/// <summary>
	/// Simple wrapper for the Rocket League Game Data API.
	/// </summary>
	public class RLGameDataAPI : IDisposable
	{
		public const int gamePort = 49123;

		/// <summary>
		/// Gets a value indicating whether the underlying <see cref="TcpClient"/> is connected to the game.
		/// </summary>
		public bool Connected => _tcpClient.Connected;
		/// <summary>
		/// Gets a value indicating whether any events are able to be read from the underlying <see cref="NetworkStream"/>.
		/// </summary>
		public bool DataAvailable => Connected && _tcpClient.GetStream().DataAvailable;

		private TcpClient _tcpClient;
		private JsonSerializerOptions? _jsonOptions;
		private byte[] _buffer;
		private bool _disposed;

		public RLGameDataAPI()
		{
			_tcpClient = new TcpClient();
			_jsonOptions = new JsonSerializerOptions()
			{
				RespectNullableAnnotations = true,
				UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
			};
			_jsonOptions.Converters.Add(new JsonUniqueNetIdConverter());
			_jsonOptions.Converters.Add(new JsonStringEnumConverter<EventType>());
			_jsonOptions.Converters.Add(new JsonStringEnumConverter<StatEvent>());
			_jsonOptions.MakeReadOnly();
			_buffer = new byte[1024 * 4];
		}

		public void Dispose()
		{
			if (!_disposed)
			{
				_disposed = true;
				_buffer = [];
				_jsonOptions = null;
				_tcpClient.Dispose();
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>
		/// Connects to the local game using the specified port.
		/// </summary>
		/// <param name="port">the port number specified in your DefaultStatsAPI.ini.</param>
		/// <exception cref="SocketException"/>
		/// <exception cref="ObjectDisposedException"/>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public void Connect(int port = gamePort)
		{
			_tcpClient.Connect(new IPEndPoint(IPAddress.Loopback, port));
		}

		/// <summary>
		/// Connects to the local game using the specified port as an asynchronous operation.
		/// </summary>
		/// <param name="port">the port number specified in your DefaultStatsAPI.ini.</param>
		/// <param name="cancellationToken"></param>
		/// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
		/// <exception cref="OperationCanceledException"/>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public async ValueTask ConnectAsync(int port = gamePort, CancellationToken cancellationToken = default)
		{
			await _tcpClient.ConnectAsync(new IPEndPoint(IPAddress.Loopback, port), cancellationToken);
		}

		/// <summary>
		/// Begins an asynchronous request for a local game connection using the specified port number.
		/// </summary>
		/// <param name="port">the port number specified in your DefaultStatsAPI.ini.</param>
		/// <param name="requestCallback">An <see cref="AsyncCallback"/> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the connect operation. This object is passed to the <paramref name="requestCallback"/> delegate when the operation is complete.</param>
		/// <returns>An <see cref="IAsyncResult"/> object that references the asynchronous connection.</returns>
		/// <exception cref="SocketException"/>
		/// <exception cref="ObjectDisposedException"/>
		/// <exception cref="System.Security.SecurityException"/>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public IAsyncResult BeginConnect(int port = gamePort, AsyncCallback? requestCallback, object? state)
		{
			return _tcpClient.BeginConnect(IPAddress.Loopback, port, requestCallback, state);
		}

		/// <summary>
		/// Ends a pending asynchronous connection attempt.
		/// </summary>
		/// <param name="asyncResult">An <see cref="IAsyncResult"/> object returned by a call to BeginConnect.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="InvalidOperationException"/>
		/// <exception cref="SocketException"/>
		/// <exception cref="ObjectDisposedException"/>
		public void EndConnect(IAsyncResult asyncResult)
		{
			_tcpClient.EndConnect(asyncResult);
		}

		/// <summary>
		/// Disposes this <see cref="RLGameDataAPI"/> instance and requests that the underlying TCP connection be closed.
		/// </summary>
		public void Close()
		{
			_tcpClient.Close();
		}

		/// <summary>
		/// Reads all available data from the <see cref="NetworkStream"/> and converts it to their corresponding <see cref="EventData"/>.
		/// </summary>
		/// <returns>A <see cref="List{T}"/> of <see cref="EventData"/>.</returns>
		/// <exception cref="InvalidOperationException"/>
		/// <exception cref="IOException"/>
		/// <exception cref="SocketException"/>
		/// <exception cref="ObjectDisposedException"/>
		public List<EventData> ReceiveEvents()
		{
			var _stream = _tcpClient.GetStream();
			using var received = new MemoryStream();

			do
			{
				var numRead = _stream.Read(_buffer.AsSpan(0, _buffer.Length));
				received.Write(_buffer.AsSpan(0, numRead));
			}
			while (_stream.DataAvailable);

			received.Seek(0, SeekOrigin.Begin);
			return ProccessMessage(received);
		}

		/// <summary>
		/// Reads all available data from the <see cref="NetworkStream"/> and converts it to their corresponding <see cref="EventData"/> as an asynchronous operation.
		/// </summary>
		/// <returns>A <see cref="ValueTask"/> representing the asynchronous read operation. The value of its result contains a <see cref="List{T}"/> of received <see cref="EventData"/>.</returns>
		/// <exception cref="OperationCanceledException"/>
		/// <exception cref="InvalidOperationException"/>
		/// <exception cref="IOException"/>
		/// <exception cref="SocketException"/>
		/// <exception cref="ObjectDisposedException"/>
		/// <exception cref="JsonException"/>
		public async Task<List<EventData>> ReceiveEventsAsync(CancellationToken cancellationToken = default)
		{
			var _stream = _tcpClient.GetStream();
			using var received = new MemoryStream();

			do
			{
				var numRead = await _stream.ReadAsync(_buffer.AsMemory(0, _buffer.Length), cancellationToken);
				await received.WriteAsync(_buffer.AsMemory(0, numRead), cancellationToken);
			}
			while (_stream.DataAvailable);

			received.Seek(0, SeekOrigin.Begin);
			return await ProccessMessageAsync(received, cancellationToken);
		}

		private List<EventData> ProccessMessage(MemoryStream dataStream)
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

				var eventData = eventMessage.DeserializeEventData(_jsonOptions);
				if (eventData is null) continue;

				events.Add(eventData);
			}

			return events;
		}

		private async Task<List<EventData>> ProccessMessageAsync(MemoryStream dataStream, CancellationToken cancellationToken = default)
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
