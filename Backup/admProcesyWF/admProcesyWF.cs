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
using System.Diagnostics;

namespace Workflows.admProcesyWF
{
    public sealed partial class admProcesyWF : SequentialWorkflowActivity
    {
        public admProcesyWF()
        {
            InitializeComponent();
        }

        public Guid workflowId = default(System.Guid);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();
        public String logErrorMessage_HistoryDescription = default(System.String);
        public SPListItem item;
        private string _ANULOWANY = "Anulowany";

        #region Error Handler
        private void ErrorHandler_ExecuteCode(object sender, EventArgs e)
        {
            FaultHandlerActivity fa = ((Activity)sender).Parent as FaultHandlerActivity;
            if (fa != null)
            {
                Debug.WriteLine(fa.Fault.Source);
                Debug.WriteLine(fa.Fault.Message);
                Debug.WriteLine(fa.Fault.StackTrace);

                logErrorMessage_HistoryDescription = string.Format("{0}::{1}",
                    fa.Fault.Message,
                    fa.Fault.StackTrace);


                ElasticEmail.EmailGenerator.ReportErrorFromWorkflow(workflowProperties, fa.Fault.Message, fa.Fault.StackTrace);
            }
        }

        private void UpdateStatus_Anulowane_ExecuteCode(object sender, EventArgs e)
        {
            BLL.Tools.Set_Text(item, "enumStatusZlecenia", _ANULOWANY);
            item.SystemUpdate();
        }
        #endregion

        private void onWorkflowActivated1_Invoked(object sender, ExternalDataEventArgs e)
        {
            item = workflowProperties.Item;
        }


        private void isGFR(object sender, ConditionalEventArgs e)
        {
            if (item.ContentType.Name.Equals("Generowanie formatek rozliczeniowych")) e.Result = true;
        }


        private void Manage_GFR_ExecuteCode(object sender, EventArgs e)
        {
            //BLL.Workflows.StartWorkflow(item, "Generuj formatki rozliczeniowe");
            BLL.Logger.LogEvent("Generowanie formatek rozliczeniowych", item.ID.ToString());

            string mask = BLL.Tools.Get_Text(item, "colMaskaSerwisu");
            string kmask = BLL.Tools.Get_Text(item, "colMaskaTypuKlienta");

            if (!string.IsNullOrEmpty(kmask))
            {
                if (!string.IsNullOrEmpty(mask))
                {
                    Create_Bulk_FormsBy_KMask_Mask(item, kmask, mask);
                }
                else
                {
                    Crate_Bulk_FormsBy_KMask(item, kmask);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(mask))
                {
                    Create_Bulk_FormsBy_Mask(item, mask);
                }
                else
                {
                    Crate_Bulk_Forms(item);
                }
            }
        }

        #region Manage GFR Helpers
        private static void Create_Bulk_FormsBy_KMask_Mask(SPListItem item, string kmask, string mask)
        {
            Array klienci = BLL.tabKlienci.Get_AktywniKlienci_ByTypKlienta_BySerwisMask(item.Web, kmask, mask);
            Create_Forms(item, klienci);
        }

        private static void Crate_Bulk_FormsBy_KMask(SPListItem item, string kmask)
        {
            Array klienci = BLL.tabKlienci.Get_AktywniKlienci_ByTypKlientaMask(item.Web, kmask);
            Create_Forms(item, klienci);
        }

        private static void Create_Bulk_FormsBy_Mask(SPListItem item, string mask)
        {
            Array klienci = BLL.tabKlienci.Get_AktywniKlienci_BySerwisMask(item.Web, mask);
            Create_Forms(item, klienci);
        }

        private static void Crate_Bulk_Forms(SPListItem item)
        {
            Array klienci = BLL.tabKlienci.Get_AktywniKlienci_Serwis(item.Web);
            Create_Forms(item, klienci);
        }

        private static void Create_Forms(SPListItem item, Array klienci)
        {
            SPList list = BLL.admProcesy.GetList(item.Web);

            string mask = BLL.Tools.Get_Text(item, "colMaskaSerwisu");

            foreach (SPListItem k in klienci)
            {
                if (string.IsNullOrEmpty(mask))
                {
                    if (BLL.Tools.Has_SerwisAssigned(k, "selSewisy", "ZUS-*"))
                        Create_New_GFR_K(item, "ZUS-*", list, k);
                    if (BLL.Tools.Has_SerwisAssigned(k, "selSewisy", "PD-*"))
                        Create_New_GFR_K(item, "PD-*", list, k);
                    if (BLL.Tools.Has_SerwisAssigned(k, "selSewisy", "PDS-*"))
                        Create_New_GFR_K(item, "PDS-*", list, k);
                    if (BLL.Tools.Has_SerwisAssigned(k, "selSewisy", "VAT-*"))
                        Create_New_GFR_K(item, "VAT-*", list, k);
                    if (BLL.Tools.Has_SerwisAssigned(k, "selSewisy", "RBR"))
                        Create_New_GFR_K(item, "RBR", list, k);
                    if (BLL.Tools.Has_SerwisAssigned(k, "selSewisy", "RB"))
                        Create_New_GFR_K(item, "RB", list, k);
                }
                else
                {
                    if (BLL.Tools.Has_SerwisAssigned(k, "selSewisy", mask))
                        Create_New_GFR_K(item, mask, list, k);
                }

            }
        }

        private static void Create_New_GFR_K(Microsoft.SharePoint.SPListItem item, string mask, SPList list, SPListItem klientItem)
        {
            string ct = "Generowanie formatek rozliczeniowych dla klienta";
            int okresId = BLL.Tools.Get_LookupId(item, "selOkres");

            SPListItem newItem = list.AddItem();
            newItem["ContentType"] = ct;
            newItem["selKlient"] = klientItem.ID;
            newItem["selOkres"] = okresId;
            newItem["colMaskaSerwisu"] = mask;

            newItem.SystemUpdate();
        }

        #endregion

        private void SetStatus_Zakonczony_ExecuteCode(object sender, EventArgs e)
        {
            BLL.Tools.Set_Text(item, "enumStatusZlecenia", "Zakończony");
        }

        private void UpdateItem_ExecuteCode(object sender, EventArgs e)
        {
            item.SystemUpdate();
        }
    }
}
