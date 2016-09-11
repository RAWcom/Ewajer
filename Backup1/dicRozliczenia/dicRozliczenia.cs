using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace EventReceivers.dicRozliczenia
{
    public class dicRozliczenia : SPItemEventReceiver
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
           SPListItem item = properties.ListItem;
           BLL.Logger.LogEvent_EventReceiverInitiated(item);

           try
           {
               // aktualizuje pole opisowe _konta

               item["_LINK"] = String.Format(@"{0}::{1} [{2}]",
                   BLL.Tools.Get_LookupValue(item, "selKategoriaRozliczenia"),
                   item.Title,
                   BLL.Tools.Get_Text(item, "colJednostkaMiary"));
               item.SystemUpdate();
           }
           catch (Exception ex)
           {
               BLL.Logger.LogEvent(properties.WebUrl, ex.ToString());
               var result = ElasticEmail.EmailGenerator.ReportError(ex, properties.WebUrl.ToString());
           }
           finally
           {
               BLL.Logger.LogEvent_EventReceiverCompleted(item);
               this.EventFiringEnabled = true;
           }
       }


    }
}
