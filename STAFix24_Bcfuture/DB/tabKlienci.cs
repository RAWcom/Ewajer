﻿using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STAFix24_Bcfuture.DB
{
    class tabKlienci
    {
        private const string LIST_URL = "Lists/tabKlienci";

        public static SPList GetList(SPWeb web)
        {
            return web.GetList(SPUtility.ConcatUrls(web.Url, LIST_URL));
        }
    }
}
