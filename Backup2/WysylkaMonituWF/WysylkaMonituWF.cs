using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.WorkflowActions;
using STAFix24_Bcfuture.Models;
using System.Net.Mail;
using System.Text;

namespace STAFix24_Bcfuture.WysylkaMonituWF
{
    public sealed partial class WysylkaMonituWF : SequentialWorkflowActivity
    {
        public WysylkaMonituWF()
        {
            InitializeComponent();
        }

        public Guid workflowId = default(System.Guid);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();
        public String HistoryDescription = default(System.String);
        public String HistoryOutcome = default(System.String);
        private string _CT_WYSYLKA_MONITU = @"Wysyłka monitu";
        private string _MODE_WYSYLKA = @"Wysyłka";
        private Array aNierozliczoneRekordy;
        private ArrayList alRecipientIds;
        private ArrayList alRecipients;
        private IEnumerator myEnum;
        private string _SUBJECT_SALDO_ZADLUZENIA = @"Informacja o saldzie zadłużenia na dzień {0}";
        private MailMessage oMsg;
        private string _SUBJECT_SALDO_ZADLUZENIA_WERYFIKACJA = @"::Weryfikacja::Informacja o saldzie zadłużenia na dzień {0}";
        private string _SUBJECT_SALDO_ZADLUZENIA_BRAKDANYCH = @"Informacja o saldzie zadłużenia na dzień {0} - Brak danych do wysyłki";
        private string _STATUS_ZLECENIA_ANULOWANY = "Anulowany";
        private string _STATUS_ZLECENIA_ZAKONCZONY = "Zakończony";

        private void logToHistoryListActivity1_MethodInvoking(object sender, EventArgs e)
        {
            HistoryDescription = "CT=" + workflowProperties.Item.ContentType.Name;
            HistoryOutcome = string.Empty;
        }

        private void isCTWysylkaMonitu(object sender, ConditionalEventArgs e)
        {
            if (workflowProperties.Item.ContentType.Name.Equals(_CT_WYSYLKA_MONITU))
            {
                e.Result = true;
            }
            else
            {
                HistoryDescription = string.Format("Nieobsługiwany format CT={0}", workflowProperties.Item.ContentType.Name);
                HistoryOutcome = string.Empty;
            }
        }

        private void isModeWysylka(object sender, ConditionalEventArgs e)
        {
            if (Tools.GetText(workflowProperties.Item, "enumMode").Equals(_MODE_WYSYLKA))
            {
                e.Result = true;
            }
        }

        private void Select_TargetList_ExecuteCode(object sender, EventArgs e)
        {
            aNierozliczoneRekordy = WysylkaMonitu.Get_NierozliczoneRekordy(workflowProperties.Web);
        }

        private void IsRecordsFound(object sender, ConditionalEventArgs e)
        {
            if (aNierozliczoneRekordy.Length > 0)
            {
                e.Result = true;
            }
        }

        private void logToHistoryListActivity3_MethodInvoking(object sender, EventArgs e)
        {
            HistoryDescription = string.Format("Znaleziono {0} rekordow spelniających kryteria", aNierozliczoneRekordy.Length.ToString());
            HistoryOutcome = string.Empty;
        }

        private void Select_RecipientsList_ExecuteCode(object sender, EventArgs e)
        {
            alRecipientIds = new ArrayList();
            foreach (SPListItem item in aNierozliczoneRekordy)
            {
                int klientId = Tools.GetLookupId(item, "selKlient");
                if (alRecipientIds.IndexOf(klientId) < 0)
                {
                    alRecipientIds.Add(klientId);
                }
            }
        }

        private void logToHistoryListActivity8_MethodInvoking(object sender, EventArgs e)
        {
            HistoryDescription = string.Format("Wybrano {0} klientów", alRecipientIds.Count.ToString());
            HistoryOutcome = string.Empty;
        }

        private void Validate_Recipients_ExecuteCode(object sender, EventArgs e)
        {
            Array aKlienci = WysylkaMonitu.Get_ListaAktywnychKlientow(workflowProperties.Web);

            alRecipients = new ArrayList();
            foreach (int r in alRecipientIds)
            {
                foreach (SPListItem item in aKlienci)
                {
                    if (item.ID == r)
                    {
                        // dodaj do listy odbiorców
                        Models.Recipient newRecipient = new Models.Recipient
                        {
                            ID = r,
                            Email = Tools.GetText(item, "colEmail"),
                            NazwaWyswietlana = Tools.GetText(item, "_NazwaPrezentowana")
                        };

                        if (!string.IsNullOrEmpty(newRecipient.Email))
                        {
                            alRecipients.Add(newRecipient);
                        }
                    }
                }
            }

            alRecipientIds = null;
        }

        private void logToHistoryListActivity4_MethodInvoking(object sender, EventArgs e)
        {
            HistoryDescription = string.Format("Zweryfikowano {0} klientów", alRecipients.Count.ToString());
            HistoryOutcome = string.Empty;
        }

        private void GetRecipientEnumerator_ExecuteCode(object sender, EventArgs e)
        {
            myEnum = alRecipients.GetEnumerator();
        }

        private void whileRecordExist(object sender, ConditionalEventArgs e)
        {
            if (myEnum.MoveNext() && myEnum != null) e.Result = true;
            else e.Result = false;
        }

        private void Create_RecipientReportPart_ExecuteCode(object sender, EventArgs e)
        {
            Models.Recipient recipient = myEnum.Current as Recipient;

            ArrayList reportItems = new ArrayList();

            foreach (SPListItem item in aNierozliczoneRekordy)
            {
                if (Tools.GetLookupId(item, "selKlient").Equals(recipient.ID))
                {
                    Models.ReportItem ri = new Models.ReportItem()
                    {
                        DoZaplaty = Tools.GetValue(item, "colDoZaplaty"),
                        Zaplacono = Tools.GetValue(item, "colZaplacono"),
                        Tytulem = item.Title,
                        NumerFaktury = Tools.GetText(item, "colBR_NumerFaktury"),
                        DataWystawieniaFaktury = Tools.GetDate(item, "colBR_DataWystawieniaFaktury"),
                        TerminPlatnosci = Tools.GetDate(item, "colBR_TerminPlatnosci")
                    };

                    if (ri.Roznica() > 0)
                    {
                        reportItems.Add(ri);
                    }
                }
            }

            recipient.ReportItems = reportItems;

        }

        private void Create_RecipientsReports_ExecuteCode(object sender, EventArgs e)
        {
            string nazwaBiura = DB.admSetup.GetValue(workflowProperties.Web, "BR_NAZWA");

            foreach (Recipient recipient in alRecipients)
            {
                MailMessage msg = new MailMessage()
                {
                    Subject = recipient.GetSubject(),
                    Body = recipient.GetBody(WysylkaMonitu.Get_MessageLayout(workflowProperties.Web)),
                };
                foreach (MailAddress ma in recipient.To())
                {
                    msg.To.Add(ma);
                }

                StringBuilder sb = new StringBuilder(msg.Body);
                sb.Replace("[[NazwaBiura]]", nazwaBiura);

                msg.Body = sb.ToString();
                msg.IsBodyHtml = true;

                //add standard layout
                sb = new StringBuilder(BLL.dicSzablonyKomunikacji.Get_HTMLByKod(workflowProperties.Web, "EMAIL_DEFAULT_BODY"));
                sb.Replace("___FOOTER___", string.Empty);
                sb.Replace("___BODY___", msg.Body);
                msg.Body = sb.ToString();

                SPListItem m = WysylkaMonitu.Send(workflowProperties.Web, msg, recipient.ID);
            }
        }

        private void InitMail_Wysylka_ExecuteCode(object sender, EventArgs e)
        {
            oMsg = InitMessage(string.Format(_SUBJECT_SALDO_ZADLUZENIA, Tools.FormatDate(DateTime.Today)));
        }

        private MailMessage InitMessage(string subject)
        {
            oMsg = new MailMessage()
            {
                Subject = subject,
                Body = WysylkaMonitu.Get_MessageLayout(workflowProperties.Web)
            };
            oMsg.To.Add(new MailAddress(workflowProperties.OriginatorEmail));

            return oMsg;
        }

        private void InitMail_Weryfikacja_ExecuteCode(object sender, EventArgs e)
        {
            oMsg = InitMessage(string.Format(_SUBJECT_SALDO_ZADLUZENIA_WERYFIKACJA, Tools.FormatDate(DateTime.Today)));
        }

        private void Send_SummaryReport_ExecuteCode(object sender, EventArgs e)
        {
            string nazwaBiura = DB.admSetup.GetValue(workflowProperties.Web, "BR_NAZWA");

            StringBuilder sb = new StringBuilder();

            foreach (Recipient recipient in alRecipients)
            {
                sb.Append(recipient.GetBody(string.Empty));
                sb.Append("<hr>");
            }

            if (!oMsg.Body.Contains("[[Body]]"))
            {
                oMsg.Body = oMsg.Body + "[[Body]]";
            }
            StringBuilder sb2 = new StringBuilder(oMsg.Body);
            sb2.Replace("[[Body]]", sb.ToString());

            sb2.Replace("[[NazwaBiura]]", nazwaBiura);

            oMsg.Body = sb2.ToString();
            oMsg.IsBodyHtml = true;

            SPListItem m = WysylkaMonitu.Send(workflowProperties.Web, oMsg, 0);

            HistoryDescription = string.Format("Raport zbiorczy wysłany do {0}", oMsg.To[0].Address);
            HistoryOutcome = string.Format("MessageId={0}", m.ID);
        }

        private void InitMail_BrakDanych_ExecuteCode(object sender, EventArgs e)
        {
            oMsg = InitMessage(string.Format(_SUBJECT_SALDO_ZADLUZENIA_BRAKDANYCH, Tools.FormatDate(DateTime.Today)));
        }

        private void ErrorHandler_ExecuteCode(object sender, EventArgs e)
        {
            FaultHandlerActivity fa = ((Activity)sender).Parent as FaultHandlerActivity;
            if (fa != null)
            {
                ElasticEmail.EmailGenerator.ReportErrorFromWorkflow(workflowProperties, fa.Fault.Message, fa.Fault.StackTrace + "*****" + fa.Fault.Source.ToString() + "*****" + fa.Fault.Data.ToString());

                BLL.Tools.Set_Text(workflowProperties.Item, "enumStatusZlecenia", _STATUS_ZLECENIA_ANULOWANY);
                workflowProperties.Item.SystemUpdate();

                HistoryDescription = fa.Fault.Message;
                HistoryOutcome = fa.Fault.StackTrace;
            }
        }

        private void SetStatus_Zakończony_ExecuteCode(object sender, EventArgs e)
        {
            BLL.Tools.Set_Text(workflowProperties.Item, "enumStatusZlecenia", _STATUS_ZLECENIA_ZAKONCZONY);
            workflowProperties.Item.SystemUpdate();
        }

    }
}
