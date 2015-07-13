using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IPToLocation;
using NUnit.Framework;


namespace IpToCountry.Test
{
	[TestFixture]
	public class CacheTests
	{
		public CacheTests()
		{
			IPToLocation.CountryCache.Load();
		}

		[Test]
		public void CacheShouldBeLoading()
		{
			Assert.IsTrue(CountryCache.Cache.Any(), "Cache should contain at least one item");
		}

		[Test]
		public void IpAddressShouldBeFromCanada()
		{
			var ipAddressLocation = CountryCache.GetIpAddressLocation(IPAddress.Parse("23.17.255.255"));
			Assert.IsTrue(ipAddressLocation.CountryCode == "CA");
		}

		[Test]
		public void IpAddressShouldBeFromLatvia()
		{
			var ipAddressLocation = CountryCache.GetIpAddressLocation(IPAddress.Parse("31.42.95.255"));
			Assert.IsTrue(ipAddressLocation.CountryCode == "LV");
		}

		[Test]
		public void IpAddressShouldBeFromLatviaAccordingToIncident4466()
		{
			var ipAddressLocation = CountryCache.GetIpAddressLocation(IPAddress.Parse("195.244.150.52"));
			Assert.IsTrue(ipAddressLocation.CountryCode == "LV");
		}

		[Test]
		public void IpAddressShouldBeFromPrivateNetwork()
		{
			var ipAddressLocation = CountryCache.GetIpAddressLocation(IPAddress.Parse("10.110.15.92"));
			Assert.IsTrue(ipAddressLocation.PrivateNetwork);
		}

		[Test]
		public void IpAddressShouldBeFromUnrecognizedNetwork()
		{
			var ipAddressLocation = CountryCache.GetIpAddressLocation(IPAddress.Parse("0.0.0.0"));
			Assert.IsTrue(ipAddressLocation.UnrecognizedNetwork);
		}
	}
}
