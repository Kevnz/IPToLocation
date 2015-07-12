using System;
using System.Collections.Generic;
using IPToLocation;

namespace Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<IPRangeCountry> range = IPToLocationHandler.Download();

            //Save to database or whatever your backend storage is
            foreach (IPRangeCountry ipRangeCountry in range)
            {
                Console.WriteLine("From {0} to {1} is: {2}", ipRangeCountry.StartIP, ipRangeCountry.EndIP,
                    ipRangeCountry.ISO_Code_2);
            }
	        Console.ReadLine();
        }
    }
}