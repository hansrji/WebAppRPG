using System.Text;

namespace RPGWebAPI.Settings
{
	public class JwtSettings
	{
		public string? Key { get; set; }
		public string? Issuer { get; set; }
		public int ExpireDays { get; set; }

		public byte[] KeyAsBytes
		{
			get
			{
				if (Key == null) return Array.Empty<byte>();
				return Encoding.UTF8.GetBytes(Key);
			}
		}
	}
}
