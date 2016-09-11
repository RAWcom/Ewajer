using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace STAFix24_Bcfuture
{
    class Tools
    {
        private static string _EMPTY_MARKER = "---";
        internal static string GetText(SPListItem item    , string col)
        {
            if (item!=null)
            {
                return item[col] != null ? item[col].ToString() : string.Empty;
            }
            return null;
        }

        internal static decimal GetValue(SPListItem item, string col)
        {
            if (item != null)
            {
                return item[col] != null ? decimal.Parse(item[col].ToString()) : 0;
            }
            return 0;
        }

        internal static int GetLookupId(SPListItem item, string col)
        {
            return item[col] != null ? new SPFieldLookupValue(item[col].ToString()).LookupId : 0;
        }

        internal static DateTime GetDate(SPListItem item, string col)
        {
            if (item!=null)
            {
                return item[col]!=null?DateTime.Parse(item[col].ToString()):new DateTime();
            }

            return new DateTime();
        }

        internal static string FormatDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        internal static string FormatCurrency(decimal value)
        {
            if (value > 0) return value.ToString("c", new CultureInfo("pl-PL"));
            else return _EMPTY_MARKER;
        }
    }
}
