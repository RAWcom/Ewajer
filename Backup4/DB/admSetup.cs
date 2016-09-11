using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STAFix24_Bcfuture.DB
{
    class admSetup
    {
        private const string LIST_URL = "Lists/admSetup";

        public static SPList GetList(SPWeb web)
        {
            return web.GetList(SPUtility.ConcatUrls(web.Url, LIST_URL));
        }

        internal static string GetValue(Microsoft.SharePoint.SPWeb web, string key)
        {
            SPListItem item = GetList(web).Items.Cast<SPListItem>()
                .Where(i => i["KEY"].ToString() == key)
                .FirstOrDefault();

            if (item != null)
            {
                return item["VALUE"].ToString();
            }
            //}

            return string.Empty;
        }
    }
}
