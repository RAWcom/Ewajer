using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Text;
using System.Diagnostics;

namespace EventReceivers.tabZadania
{
    public class tabZadania : SPItemEventReceiver
    {
        public override void ItemAdded(SPItemEventProperties properties)
        {
            Execute(properties);
        }

        public override void ItemUpdated(SPItemEventProperties properties)
        {
            Execute(properties);
        }

        private void Execute(SPItemEventProperties properties)
        {
            this.EventFiringEnabled = false;

            try
            {
                SPWorkflow wf = BLL.Workflows.StartWorkflow(properties.ListItem, "tabZadaniaWF");
                Debug.WriteLine("StartWorkflow: tabZadaniaWF " + wf.InternalState.ToString());
            }
            catch (Exception ex)
            {
                BLL.Logger.LogEvent(properties.WebUrl, ex.ToString());
                var result = ElasticEmail.EmailGenerator.ReportError(ex, properties.WebUrl.ToString());
            }

            this.EventFiringEnabled = true;
        } 

    }
}
