using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace STAFix24_Bcfuture.Models
{
    class Recipient
    {
        public System.Collections.ArrayList ReportItems;
        public Recipient()
        {}
        public int ID { get; set; }
        public string Email { get; set; }
        public string NazwaWyswietlana { get; set; }

        internal string GetSubject()
        {
            return string.Format("Informacja o saldzie zadłużenia na dzień {0}", Tools.FormatDate(DateTime.Today));
        }

        internal string GetBody(string reportTemplate)
        {
            if (string.IsNullOrEmpty(reportTemplate))
            {
                reportTemplate = @"[[Body]]";
            }
            #region Szablony

            string tableTemplate = @"<blockquote><div style=""text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: x-small""><table cellpadding=""3""><tr><td>Płatnik: </td><td>[[Firma]]</td></tr></table><br>Informujemy, że do dnia [[Data]] r. nie odnotowaliśmy płatności za faktury VAT wymienione poniżej za usługi świadczone przez [[NazwaBiura]]</div></blockquote><h3 style=""text-align: center; font-family: Arial, Helvetica, sans-serif; color: #FF0000;"">ZESTAWIENIE NIEZAPŁACONYCH FAKTUR</h3><table align=""center"" cellpadding=""5"" cellspacing=""2"" style=""border: 1px solid #808080; width: 100%; font-family: Arial, Helvetica, sans-serif; font-size: x-small;"" visible=""false""><tr><th style=""background-color: #E4E4E4""><span style=""font-weight: normal"">Numer&nbsp;faktury</span></th><th style=""background-color: #E4E4E4""><span style=""font-weight: normal"">Data<br>wystawienia<br>dokumentu</span></th><th style=""background-color: #E4E4E4""><span style=""font-weight: normal"">Termin<br>płatności</span></th><th style=""background-color: #E4E4E4""><span style=""font-weight: normal"">Wartość<br>faktury brutto</span></th><th style=""background-color: #E4E4E4""><span style=""font-weight: normal"">Zapłacono</span></th><th style=""background-color: #E4E4E4""><span style=""font-weight: normal"">Pozostało<br>do zapłaty</span></th></tr>[[TABLE_ROW]] <tr><th colspan=""5"" style=""height: 16px; background-color: #E4E4E4; text-align: right;"">razem:</th><td style=""height: 16px; white-space: nowrap; text-align: center;""><nobr><strong>[[KwotaDoZaplaty]]</strong></nobr></td></tr></table><blockquote><br><div style=""text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: x-small"">Do zapłaty na dzień [[Data]] kwota: <strong>[[KwotaDoZaplaty]]</strong>.<br><br>Uprzejmie prosimy o niezwłoczne uregulowanie zaległej należności.<ul><li>Jeżeli nie otrzymali Państwo faktury lub mają Państwo pytania prosimy o kontakt z naszym biurem.</li><li>Jeżeli faktura została już zapłacona prosimy o zignorowanie tej wiadomości.</li></ul></div></blockquote>";
            string rowTemplate = @"<tr><td style=""height: 16px; background-color: #E4E4E4""><nobr><span>[[NumerFaktury]]<br><small>[[Tytulem]]<small></span></nobr></td> <td style=""height: 16px; white-space: nowrap; text-align: center;""><nobr>[[DataWystawienia]]</nobr></td> <td style=""height: 16px; white-space: nowrap; text-align: center;""><nobr><strong>[[TerminPlatnosci]]</strong></nobr></td> <td style=""height: 16px; white-space: nowrap; text-align: center;""><nobr>[[KwotaFaktury]]</nobr></td> <td style=""height: 16px; white-space: nowrap; text-align: center;""><nobr><strong>[[Zaplacono]]</strong></nobr></td> <td style=""height: 16px; white-space: nowrap; text-align: center;""><nobr><strong>[[KwotaDlugu]]</strong></nobr></td> </tr>";

            #endregion

            StringBuilder sbr = new StringBuilder();
            foreach (ReportItem ri in this.ReportItems)
            {
                sbr.AppendFormat(rowTemplate);
                sbr.Replace("[[NumerFaktury]]", ri.NumerFaktury);
                sbr.Replace("[[Tytulem]]", ri.Tytulem);
                sbr.Replace("[[DataWystawienia]]",Tools.FormatDate(ri.DataWystawieniaFaktury));
                sbr.Replace("[[TerminPlatnosci]]",Tools.FormatDate(ri.TerminPlatnosci));
                sbr.Replace("[[KwotaFaktury]]",Tools.FormatCurrency(ri.DoZaplaty));
                sbr.Replace("[[Zaplacono]]",Tools.FormatCurrency(ri.Zaplacono));
                sbr.Replace("[[KwotaDlugu]]",Tools.FormatCurrency(ri.DoZaplaty-ri.Zaplacono));
            }

            StringBuilder sbt = new StringBuilder(tableTemplate);
            sbt.Replace("[[TABLE_ROW]]", sbr.ToString());
            

            StringBuilder sb = new StringBuilder(reportTemplate);
            // update table
            sb.Replace("[[Body]]", sbt.ToString());
            sb.Replace("[[Firma]]", this.NazwaWyswietlana);
            sb.Replace("[[Data]]", Tools.FormatDate(DateTime.Today));
            sb.Replace("[[KwotaDoZaplaty]]", Tools.FormatCurrency(this.KwotaDoZaplaty()));

            return sb.ToString();
        }

        private decimal KwotaDoZaplaty()
        {
            decimal result = 0;
            foreach (ReportItem ri in this.ReportItems)
            {
                result = result + ri.DoZaplaty - ri.Zaplacono;
            }

            return result;
        }

        internal MailAddressCollection To()
        {
            MailAddressCollection mac = new MailAddressCollection();
            mac.Add(new MailAddress(this.Email));
            return mac;
        }
    }
}
