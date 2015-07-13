using System.Linq;
using System.Net;

namespace IPToLocation
{
	public static class IpAddressExtensions
	{
		public static uint ToInteger(this IPAddress ipAddress)
		{
			var bytes = ipAddress.GetAddressBytes();
			if (bytes.Any())
			{
				return (uint)(bytes[0] << 24 | bytes[1] << 16 | bytes[2] << 8 | bytes[3]);
			}
			return 0;
		}
	}
}