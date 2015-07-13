namespace IPToLocation
{
	public class IpAddressLocation
	{
		public string CountryCode { get; private set; }
		public bool PrivateNetwork { get; private set; }
		public bool UnrecognizedNetwork { get; private set; }

		public IpAddressLocation()
		{
			CountryCode = string.Empty;
			PrivateNetwork = false;
			UnrecognizedNetwork = true;
		}

		public IpAddressLocation(string countryCode)
		{
			CountryCode = countryCode;

			if (CountryCode == "-")
			{
				CountryCode = string.Empty;
				PrivateNetwork = true;
				UnrecognizedNetwork = false;
			}
		}
	}
}