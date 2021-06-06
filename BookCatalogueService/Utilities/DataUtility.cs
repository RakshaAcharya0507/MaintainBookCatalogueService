using BookCatalogue.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BookCatalogue.Utilities
{
    public static class DataUtility
    {

        public static bool tryIntParse(string value)
        {
            try
            {
                long.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool tryDateTimeParse(string value)
        {
            try
            {
                DateTime date= DateTime.ParseExact(value, AppConstants.DATETIMEFORMAT, CultureInfo.InvariantCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}