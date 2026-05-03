namespace RocketLeagueGameDataAPI.Models
{
	public struct UniqueNetId
	{
		/// <summary>
		/// The platform the player is currently playing on.
		/// </summary>
		public OnlinePlatform Platform { get; set; }
		/// <summary>
		/// The player's platform unique identifier.
		/// </summary>
		/// <remarks>Always <see langword="0"/> for <see cref="OnlinePlatform.OnlinePlatform_Epic"/>.</remarks>
		public ulong Uid { get; set; }
		/// <summary>
		/// The player's Epic AccountId.
		/// </summary>
		/// <remarks>Always <see langword="null"/> except for <see cref="OnlinePlatform.OnlinePlatform_Epic"/>.</remarks>
		public string? EpicAccountId { get; set; }
		/// <summary>
		/// The player's splitscreen index.
		/// </summary>
		public byte SplitscreenID { get; set; }

		/// <summary>
		/// Initializes a new <see cref="UniqueNetId"/> with the given platform, uid and splitscreenId.
		/// </summary>
		/// <remarks>If you want to initialize a <see cref="UniqueNetId"/> for <see cref="OnlinePlatform.OnlinePlatform_Epic"/>, use <see cref="UniqueNetId(OnlinePlatform platform, string epicAccountId, byte splitscreenId)"/> instead.</remarks>
		/// <param name="platform">The platform for this identifier.</param>
		/// <param name="uid">The uid for this identifier.</param>
		/// <param name="splitscreenId">The splitscreen index for this identifier.</param>
		/// <exception cref="ArgumentException"></exception>
		public UniqueNetId(OnlinePlatform platform, ulong uid, byte splitscreenId)
		{
			if (platform == OnlinePlatform.OnlinePlatform_Epic) throw new ArgumentException("Platform cannot be Epic", nameof(platform));
			Platform = platform;
			Uid = uid;
			SplitscreenID = splitscreenId;
		}

		/// <summary>
		/// Initializes a new <see cref="UniqueNetId"/> with the given platform, epicId and splitscreenId.
		/// </summary>
		/// <remarks>If you don't want to initialize a <see cref="UniqueNetId"/> for <see cref="OnlinePlatform.OnlinePlatform_Epic"/>, use <see cref="UniqueNetId(OnlinePlatform platform, ulong uid, byte splitscreenId)"/> instead.</remarks>
		/// <param name="platform">The platform for this identifier.</param>
		/// <param name="epicAccountId">The Epic AccountId for this identifier.</param>
		/// <param name="splitscreenId">The splitscreen index for this identifier.</param>
		/// <exception cref="ArgumentException"></exception>
		public UniqueNetId(OnlinePlatform platform, string epicAccountId, byte splitscreenId)
		{
			if (platform != OnlinePlatform.OnlinePlatform_Epic) throw new ArgumentException("Platform must be Epic", nameof(platform));
			Platform = platform;
			EpicAccountId = epicAccountId;
			SplitscreenID = splitscreenId;
		}

		/// <summary>
		/// Converts this platform identifier to the format Platform|Uid|SplitscreenID (e.g. "Steam|123|0", "Epic|456|0").
		/// </summary>
		/// <returns>This platform identifier's <see langword="string"/> representation.</returns>
		public override readonly string ToString()
		{
			return $"{OnlinePlatformToString(Platform)}|{(Platform == OnlinePlatform.OnlinePlatform_Epic ? EpicAccountId : Uid)}|{SplitscreenID}";
		}

		/// <summary>
		/// Converts the <see langword="string"/> representation of a platform identifier to it's <see cref="UniqueNetId"/> equivalent.
		/// </summary>
		/// <param name="id">A <see langword="string"/> that contains a platform identifier to convert.</param>
		/// <returns>A <see cref="UniqueNetId"/> that is equivalent to the platform identifier contained in <paramref name="id"/>.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="FormatException"></exception>
		public static UniqueNetId Parse(string id)
		{
			ArgumentNullException.ThrowIfNull(id);

			var pieces = id.Split("|");
			if (pieces.Length < 3) throw new FormatException($"Could not convert '{id}' to {nameof(UniqueNetId)}");

			var platform = StringToOnlinePlatform(pieces[0]);
			var splitscreenId = byte.Parse(pieces[2]);
			if (platform != OnlinePlatform.OnlinePlatform_Epic)
			{
				var uid = ulong.Parse(pieces[1]);
				return new UniqueNetId(platform, uid, splitscreenId);
			}

			var epicAccountId = pieces[1];
			return new UniqueNetId(platform, epicAccountId, splitscreenId);
		}

		/// <summary>
		/// Converts a <see cref="OnlinePlatform"/> to it's <see langword="string"/> representation.
		/// </summary>
		/// <param name="platform">The platform to convert.</param>
		/// <returns>The given platform's <see langword="string"/> representation.</returns>
		/// <exception cref="ArgumentException"></exception>
		public static string OnlinePlatformToString(OnlinePlatform platform) => platform switch
		{
			OnlinePlatform.OnlinePlatform_Unknown => "Unknown",
			OnlinePlatform.OnlinePlatform_Steam => "Steam",
			OnlinePlatform.OnlinePlatform_PS4 => "PS4",
			OnlinePlatform.OnlinePlatform_Dingo => "XboxOne",
			OnlinePlatform.OnlinePlatform_NNX => "Switch",
			OnlinePlatform.OnlinePlatform_Epic => "Epic",
			_ => throw new ArgumentException("Platform not supported!", nameof(platform)),
		};

		/// <summary>
		/// Converts the <see langword="string"/> representation of a platform to it's <see cref="OnlinePlatform"/> equivalent.
		/// </summary>
		/// <param name="platform">A <see langword="string"/> that contains a platform to convert.</param>
		/// <returns>A <see cref="OnlinePlatform"/> that is equivalent to the platform contained in <paramref name="platform"/>.</returns>
		/// <exception cref="ArgumentException"></exception>
		public static OnlinePlatform StringToOnlinePlatform(string platform) => platform switch
		{
			"Unknown" => OnlinePlatform.OnlinePlatform_Unknown,
			"Steam" => OnlinePlatform.OnlinePlatform_Steam,
			"PS4" => OnlinePlatform.OnlinePlatform_PS4,
			"XboxOne" => OnlinePlatform.OnlinePlatform_Dingo,
			"Switch" => OnlinePlatform.OnlinePlatform_NNX,
			"Epic" => OnlinePlatform.OnlinePlatform_Epic,
			_ => throw new ArgumentException("Platform not supported!", nameof(platform)),
		};
	}

	public enum OnlinePlatform : byte
	{
		/// <summary>Unknown platform, bots use this.</summary>
		OnlinePlatform_Unknown = 0,
		/// <summary> Codename for Steam.</summary>
		OnlinePlatform_Steam = 1,
		/// <summary>Codename for PS4.</summary>
		OnlinePlatform_PS4 = 2,
		/// <summary>Unused.</summary>
		OnlinePlatform_PS3 = 3,
		/// <summary>Codename for XboxOne.</summary>
		OnlinePlatform_Dingo = 4,
		/// <summary>Unused.</summary>
		OnlinePlatform_QQ_DEPRECATED = 5,
		/// <summary>Old codename for Switch.</summary>
		OnlinePlatform_OldNNX = 6,
		/// <summary>Codename for Switch.</summary>
		OnlinePlatform_NNX = 7,
		/// <summary>Unused.</summary>
		OnlinePlatform_PsyNet = 8,
		/// <summary>Unused.</summary>
		OnlinePlatform_Deleted = 9,
		/// <summary>Unused.</summary>
		OnlinePlatform_WeGame_DEPRECATED = 10,
		/// <summary>Codename for Epic.</summary>
		OnlinePlatform_Epic = 11,
		/// <summary>Maximum index +1.</summary>
		OnlinePlatform_MAX = 12
	}
}
