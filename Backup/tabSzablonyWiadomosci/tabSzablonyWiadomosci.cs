using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Diagnostics;

namespace EventReceivers.tabSzablonyWiadomosci
{
    public class tabSzablonyWiadomosci : SPItemEventReceiver
    {
       private void Execute(SPItemEventProperties properties)
       {
           Debug.WriteLine("*** Szablon wiadomości. Execute ***");
           this.EventFiringEnabled = false;

           SPListItem item = properties.ListItem;

           try
           {
               string np = item.Title;

               // ozanacz szablon jako aktywny jeżeli ma wybrane jakieś funkcje
               if (BLL.Tools.Has_SelectedOptions(item, "colFunkcjeSzablonow"))
                   np = np + " *** AKTYWNY ***";

               BLL.Tools.Set_Text(item, "_NazwaPrezentowana", np);

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
           }
           this.EventFiringEnabled = true;
       }

       public override void ItemAdded(SPItemEventProperties properties)
       {
           base.ItemAdded(properties);
           Execute(properties);
       }

       public override void ItemUpdated(SPItemEventProperties properties)
       {
           base.ItemUpdated(properties);
           Execute(properties);
       }


    }
}
