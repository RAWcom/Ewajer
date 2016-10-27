using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using System.Diagnostics;
using System.Text;

namespace Stafix.Features.Workflows
{
    [Guid("f43ab3ab-4aa0-4abb-b953-e0308ce6327e")]
    public class WorkflowsEventReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            try
            {
                var site = properties.Feature.Parent as SPSite;
                string associationName;
                SPWeb web = site.RootWeb;
                StringBuilder errMsg = new StringBuilder();

                // tabZadania
                EnsureWorkflowAssociation(web, "Zadania", "tabZadaniaWF", ref errMsg, false, false, false);

                // tabKartyKontrolne
                EnsureWorkflowAssociation(web, "Karty kontrolne", "Przygotuj wiadomość dla klienta", ref errMsg, true, false, false);

                // tabWiadomości
                EnsureWorkflowAssociation(web, "Wiadomości", "Obsługa wiadomości", ref errMsg, true, false, false);
                //associationName = EnsureWorkflowAssociation(web, "Wiadomosci", "Wyślij kopię wiadomości", ref errMsg);

                //admProcesy
                EnsureWorkflowAssociation(web, "admProcesy", "admProcesyWF", ref errMsg, false, false, false);
                EnsureWorkflowAssociation(web, "admProcesy", "Generuj formatki rozliczeniowe", ref errMsg, false, false, false);
                EnsureWorkflowAssociation(web, "admProcesy", "Generuj formatki rozliczeniowe dla klienta", ref errMsg, false, false, false);

                if (errMsg.Length > 0)
                {
                    string subject = string.Format("Workflow Feature ({0}) issues", site.Url.ToString());
                    string bodyHTML = string.Format("<ol>{0}</ol>", errMsg.ToString());
                    ElasticEmail.EmailGenerator.SendMail(subject, bodyHTML);
                }
                else
                {
                    //string subject = string.Format("Workflow Feature ({0}) activated", site.Url.ToString());
                    //ElasticEmail.EmailGenerator.SendMail(subject, string.Empty);
                }
            }
            catch (Exception ex)
            {
                ElasticEmail.EmailGenerator.ReportError(ex, (properties.Feature.Parent as SPSite).Url);
            }

        }

        private static void EnsureWorkflowAssociation(SPWeb web, string listName, string workflowTemplateName, ref StringBuilder errMsg, bool allowManual, bool startCreate, bool startUpdate)
        {
            SPList list = web.Lists.TryGetList(listName);
            if (list!=null)
            {
                string associationName = workflowTemplateName;
                if (list != null) BLL.Workflows.EnsureWorkflowAssociation(list, workflowTemplateName, associationName, allowManual, startCreate, startUpdate);
                Debug.WriteLine("Workflow: " + workflowTemplateName + " - associated");
            }
            else
            {
                errMsg.AppendFormat("<li>Lista: '{0}' nie istnieje - workflow '{1}' nie może być skojarzony</li>", listName, workflowTemplateName); 
            }
        }
    }
}
