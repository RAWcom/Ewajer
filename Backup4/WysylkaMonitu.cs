using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace STAFix24_Bcfuture
{
    class WysylkaMonitu
    {
        private static string _STATUS_AKTYWNY = @"Aktywny";

        internal static Array Get_NierozliczoneRekordy(SPWeb web)
        {
            return DB.tabRozliczeniaGotowkowe.GetList(web).Items.Cast<SPListItem>()
                .Where(i => Tools.GetValue(i, "colZaplacono") < Tools.GetValue(i, "colDoZaplaty"))
                .ToArray();
        }

        internal static Array Get_ListaAktywnychKlientow(SPWeb web)
        {
            return DB.tabKlienci.GetList(web).Items.Cast<SPListItem>()
                .Where(i => Tools.GetText(i, "enumStatus").Equals(_STATUS_AKTYWNY))
                .ToArray();
        }

        internal static string Get_MessageLayout(SPWeb web)
        {
            return string.Empty;
        }

        internal static SPListItem Send(SPWeb web, MailMessage msg, int klientId)
        {
            SPList list = DB.tabWiadomosci.GetList(web);
            SPListItem newItem = list.AddItem();
            newItem["Title"] = msg.Subject;

            if (msg.From==null || string.IsNullOrEmpty(msg.From.Address))
                msg.From = new MailAddress(DB.admSetup.GetValue(web, "EMAIL_BIURA"));

            newItem["colNadawca"] = msg.From.Address;
            newItem["colOdbiorca"] = msg.To[0].Address;

            if (msg.IsBodyHtml)
            {
            newItem["colTresc"] = string.Empty;
            newItem["colTrescHTML"] = msg.Body;
            }
            else
	        {
                    newItem["colTresc"] = msg.Body;
                    newItem["colTrescHTML"] = string.Empty;
	        }

            if (klientId > 0) newItem["selKlient_NazwaSkrocona"] = klientId;

            newItem.Update();
            return newItem;
        }
    }
}
