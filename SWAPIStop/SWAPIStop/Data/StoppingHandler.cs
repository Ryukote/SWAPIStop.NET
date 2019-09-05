using SWAPIStop.Constants;
using System;
using System.Linq;

namespace SWAPIStop.Data
{
    public class StoppingHandler
    {
        private string NormalizePeriodName(string period)
        {
            return char.ToUpper(period[0]) + period.Substring(1);
        }

        public int PeriodToDays(string period)
        {
            if (period.Any(char.IsDigit))
            {
                char[] separators = { ' ' };
                var splitted = period.Split(separators);

                var parsedValue = (PeriodEnumaration)Enum.Parse(typeof(PeriodEnumaration), NormalizePeriodName(splitted[1]));

                return Convert.ToInt32(splitted[0]) * (int)parsedValue;
            }

            else
            {
                var parsedValue = (PeriodEnumaration)Enum.Parse(typeof(PeriodEnumaration), NormalizePeriodName(period));

                return (int)parsedValue;
            }
        }

        public int NumberOfStops(int input, int mglt, int consumables)
        {
            return (int) Math.Truncate((decimal)(input / mglt / 24 / consumables));
        }
    }
}
