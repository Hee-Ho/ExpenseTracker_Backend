using System.Globalization;

namespace ExpenseTrackerAPI.Utilities
{
    public class ParameterCheck
    {
        public static bool validateDateFormat(string? datetime) {
            string format = "yyyy-MM-dd";
            return DateTime.TryParseExact(datetime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _); 
        }

        public static bool validateMoneyFormat(decimal? amount)
        {
            return (amount * 100) % 1 == 0;
        }


    }
}
