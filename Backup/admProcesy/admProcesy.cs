using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using BLL;

namespace EventReceivers.admProcesy
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public class admProcesy : SPItemEventReceiver
    {
        private string _ZAKONCZONY = "Zakończony";
        private string _ANULOWANY = "Anulowany";

        public override void ItemAdded(SPItemEventProperties properties)
        {
            this.EventFiringEnabled = false;

            SPWorkflow wf;

            try
            {
                switch (properties.ListItem.ContentType.Name)
                {
                    case "Generowanie formatek rozliczeniowych":
                        wf = BLL.Workflows.StartWorkflow(properties.ListItem, "Generuj formatki rozliczeniowe");
                        Debug.WriteLine("StartWorkflow: Generuj formatki rozliczeniowe " + wf.InternalState.ToString());
                        break;
                    case "Generowanie formatek rozliczeniowych dla klienta":
                        wf = BLL.Workflows.StartWorkflow(properties.ListItem, "Generuj formatki rozliczeniowe dla klienta");
                        Debug.WriteLine("StartWorkflow: Generuj formatki rozliczeniowe dla klienta " + wf.InternalState.ToString());
                        break;
                    case "Obsługa wiadomości":
                        wf = BLL.Workflows.StartSiteWorkflow(properties.Web.Site, "Obsługa wiadomości oczekujących", properties.ListItemId.ToString());
                        //wf = BLL.Workflows.StartSiteWorkflow(properties.Web.Site, "Obsługa wiadomości oczekujących", null);
                        Debug.WriteLine("StartWorkflow: Generuj formatki rozliczeniowe dla klienta " + wf.InternalState.ToString());
                        break;
                    case "Przygotuj wiadomości z kart kontrolnych":
                        wf = BLL.Workflows.StartSiteWorkflow(properties.Web.Site, "Obsługa kart kontrolnych", properties.ListItemId.ToString());
                        Debug.WriteLine("StartWorkflow: Generuj formatki rozliczeniowe dla klienta " + wf.InternalState.ToString());
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                BLL.Logger.LogEvent(properties.WebUrl, ex.ToString());
                var result = ElasticEmail.EmailGenerator.ReportError(ex, properties.WebUrl.ToString());
                BLL.Tools.Set_Text(properties.ListItem, "enumStatusZlecenia", _ANULOWANY);
                properties.ListItem.Update();
            }

            this.EventFiringEnabled = true;

        }
    }
}
