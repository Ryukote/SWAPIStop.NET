using SWAPIStop.Constants;
using SWAPIStop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SWAPIStop.Data
{
    public class StoppingHandler
    {
        public string NormalizePeriodName(string period)
        {
            return char.ToUpper(period[0]) + period.Substring(1);
        }

        public int PeriodToDays(string period)
        {
            if (period.Any(char.IsDigit))
            {
                char[] separators = { ' ' };
                var splitted = period.Split(separators);

                var parsedValue = (PeriodEnumeration)Enum.Parse(typeof(PeriodEnumeration), NormalizePeriodName(splitted[1]));

                return Convert.ToInt32(splitted[0]) * (int)parsedValue;
            }

            else
            {
                var parsedValue = (PeriodEnumeration)Enum.Parse(typeof(PeriodEnumeration), NormalizePeriodName(period));

                return (int)parsedValue;
            }
        }

        public int NumberOfStops(int input, int mglt, int consumables)
        {
            return (int) Math.Truncate((decimal)(input / mglt / 24 / consumables));
        }

        public void DisplayStops(int input, List<Starship> collection)
        {
            int nonValidItems = 0;
            foreach(var item in collection)
            {
                if(item.Consumables != "unknown" && item.MGLT != "unknown")
                {
                    Console.WriteLine($"Starsip name: {item.Name}");
                    var stops = NumberOfStops(input, Convert.ToInt32(item.MGLT), PeriodToDays(item.Consumables));
                    Console.WriteLine($"Number of stops: {stops}");
                    Console.WriteLine("-------------------------");
                }

                else
                {
                    nonValidItems++;
                }
            }

            Console.WriteLine($"There were {nonValidItems} starships for which could not be determined how many stops they need for resupply to cover a given distance.");
        }
    }
}
