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

namespace Workflows.GFR
{
    public sealed partial class GFR : SequentialWorkflowActivity
    {
        public GFR()
        {
            InitializeComponent();
        }

        public Guid workflowId = default(System.Guid);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();
        SPListItem item;

        public String logErrorMessage_HistoryDescription = default(System.String);
        private string _ANULOWANY = "Anulowany";
        private string _ZAKONCZONY = "Zakończony";



        private void onWorkflowActivated1_Invoked(object sender, ExternalDataEventArgs e)
        {
            item = workflowProperties.Item;
        }

        private void isCT_GFR(object sender, ConditionalEventArgs e)
        {
            if (item.ContentType.Name.Equals("Generowanie formatek rozliczeniowych")) e.Result = true;
            else e.Result = false;

        }

        private void Select_Klienci_ExecuteCode(object sender, EventArgs e)
        {
            EventReceivers.admProcesy.GFR_Request.Create(item);
        }

        private void UpdatStatusZadania_ExecuteCode(object sender, EventArgs e)
        {
            BLL.Tools.Set_Text(item, "enumStatusZlecenia", _ZAKONCZONY);
            item.SystemUpdate();
        }

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








    }
}
