using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.SharePoint.Workflow;

namespace STAFix24_Bcfuture.Features.SiteFeatures
{


    [Guid("33565599-24f6-4a1b-ab5f-e4220bf78dbc")]
    public class SiteFeaturesEventReceiver : SPFeatureReceiver
    {
        public const string workflowHistoryListName = "Workflow History";
        public const string workflowTaskListName = "Workflow Tasks";

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {

            try
            {
                var site = properties.Feature.Parent as SPSite;
                SPWeb web = site.RootWeb;
                StringBuilder errMsg = new StringBuilder();

                // Import faktur
                EnsureWorkflowAssociationCT(web, "Import faktur", "ImportFakturWF", ref errMsg);
                //EnsureWorkflowAssociation(web, "admProcesy", "ImportFakturWF", "ImportFakturWF", ref errMsg);

                // Wysyłka monitu
                EnsureWorkflowAssociationCT(web, "Wysyłka monitu", "WysylkaMonituWF", ref errMsg);

                if (errMsg.Length > 0)
                {
                    string subject = string.Format("Feature activation ({0}) issues", site.Url.ToString());
                    string bodyHTML = string.Format("<ol>{0}</ol>", errMsg.ToString());
                    ElasticEmail.EmailGenerator.SendMail(subject, bodyHTML);
                }
                else
                {
                    string subject = string.Format("Feature ({0}) activated", site.Url.ToString());
                    ElasticEmail.EmailGenerator.SendMail(subject, string.Empty);
                }
            }
            catch (Exception ex)
            {
                ElasticEmail.EmailGenerator.ReportError(ex, (properties.Feature.Parent as SPSite).Url);
            }
        }

        private static void EnsureWorkflowAssociationCT(SPWeb web, string ctName, string workflowTemplateName, ref StringBuilder errMsg)
        {
            SPContentType ct = web.ContentTypes[ctName];
            if (ct != null)
            {
                string associationName = workflowTemplateName;
                if (ct != null) EnsureWorkflowAssociationCT(ct, workflowTemplateName, associationName);
                Debug.WriteLine("Workflow: " + workflowTemplateName + " - associated");
            }
            else
            {
                errMsg.AppendFormat("<li>CT: '{0}' nie istnieje - workflow '{1}' nie może być skojarzony</li>", ctName, workflowTemplateName);
            }
        }

        private static void EnsureWorkflowAssociationCT(SPContentType ct, string workflowTemplateName, string associationName)
        {
            SPSite site = ct.ParentWeb.Site;
            SPWeb web = site.OpenWeb();
            var lcid = (int)web.Language;
            var defaultCulture = new CultureInfo(lcid);

            // Create the workflow association
            SPList taskList = EnsureListExist(web, workflowTaskListName);
            SPList historyList = EnsureListExist(web, workflowHistoryListName);

            // Get a template
            SPWorkflowTemplate workflowTemplate = null;
            foreach (SPWorkflowTemplate template in web.WorkflowTemplates)
            {
                workflowTemplate = template;
                if (workflowTemplate.Name.Equals(workflowTemplateName)) break;
            }

            SPWorkflowAssociation workflowAssociation = SPWorkflowAssociation.CreateWebContentTypeAssociation(workflowTemplate, associationName, workflowTaskListName, workflowHistoryListName);

            //ElasticEmail.EmailGenerator.SendMail("wartość zmiennej", ct.WorkflowAssociations.GetAssociationByName(workflowAssociation.Name, web.Locale).Name);

            // Add the association to the content type or update it if it already exists.
            if (ct.WorkflowAssociations.GetAssociationByName(workflowAssociation.Name, web.Locale) == null)
            {
                //ElasticEmail.EmailGenerator.SendMail("ct:add", "");
                ct.WorkflowAssociations.Add(workflowAssociation);
            }
            else
            {
                //ElasticEmail.EmailGenerator.SendMail("ct:update", "");
                //ct.WorkflowAssociations.Update(workflowAssociation);
                ct.WorkflowAssociations.UpdateAssociationsToLatestVersion();
            }

            // Propagate to children of this content type.
            ct.UpdateWorkflowAssociationsOnChildren(false,  // Do not generate full change list
                                                    true,   // Push down to derived content types
                                                    true,   // Push down to list content types
                                                    false); // Do not throw exception if sealed or readonly

            web.Dispose();
            site.Dispose();
        }

        private static void EnsureWorkflowAssociation(SPWeb web, string listName, string workflowTemplateName, string associationName, ref StringBuilder errMsg)
        {
            SPList list = web.Lists.TryGetList(listName);
            if (list != null)
            {
                //string associationName = workflowTemplateName;
                if (list != null) BLL.Workflows.EnsureWorkflowAssociation(list, workflowTemplateName, associationName, false, false, false);
                Debug.WriteLine("Workflow: " + workflowTemplateName + " - associated");
            }
            else
            {
                errMsg.AppendFormat("<li>Lista: '{0}' nie istnieje - workflow '{1}' nie może być skojarzony</li>", listName, workflowTemplateName);
            }
        }

        private static SPList EnsureListExist(SPWeb web, string listName)
        {
            SPList list = web.Lists.TryGetList(listName);
            if (list == null)
            {
                list = BLL.Workflows.CreateTaskList(web, listName);
            }
            return list;
        }
    }
}
