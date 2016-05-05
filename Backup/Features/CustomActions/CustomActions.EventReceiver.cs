using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace Stafix.Features.CustomActions
{
    [Guid("363b4ac9-e5f0-4251-b1df-058273f5f60d")]
    public class CustomActionsEventReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb web = properties.Feature.Parent as SPWeb;

            try
            {
                BLL.tabKlienci.Setup(web);
            }
            catch (Exception ex)
            {
                var result = ElasticEmail.EmailGenerator.ReportError(ex, web.Url);
            }
        }
    }
}
