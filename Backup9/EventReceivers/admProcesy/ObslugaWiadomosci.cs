using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using System.Diagnostics;

namespace EventReceivers.admProcesy
{
    internal class ObslugaWiadomosci
    {
        const string targetList = @"Wiadomości";
        

        internal static void Execute(SPListItem item)
        {
            BLL.Logger.LogEvent("Obsłga wiadomomości", item.ID.ToString());

            SPList list = item.Web.Lists.TryGetList(targetList);

            list.Items.Cast<SPListItem>()
                .Where(i => (bool)i["colCzyWyslana"] != true)
                .Where(i => i["colPlanowanaDataNadania"] == null
                    || (i["colPlanowanaDataNadania"] != null
                       && (DateTime)i["colPlanowanaDataNadania"] <= DateTime.Now))
                .ToList()
                .ForEach(i =>
                {
                    SPWorkflow wf = BLL.Workflows.StartWorkflow(i, "Obsługa wiadomości");
                    Debug.WriteLine("StartWorkflow: Obsługa wiadomości " + wf.InternalState.ToString());
                    BLL.Logger.LogEvent(BLL.Tools.Get_LookupValue(i, "selKlient_NazwaSkrocona").ToString(), i.ID.ToString());
                });
        }
    }
}
