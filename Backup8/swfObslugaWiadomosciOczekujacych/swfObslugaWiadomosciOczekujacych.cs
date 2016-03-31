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
using System.Text;

namespace Workflows.swfObslugaWiadomosciOczekujacych
{
    public sealed partial class swfObslugaWiadomosciOczekujacych : SequentialWorkflowActivity
    {
        public swfObslugaWiadomosciOczekujacych()
        {
            InitializeComponent();
        }

        public Guid workflowId = default(System.Guid);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();
        public String logSelected_HistoryDescription = default(System.String);
        public String logCurrentMessage_HistoryDescription = default(System.String);
        private Array results;
        private IEnumerator myEnum;

        private void Select_ListaWiadomosciOczekujacych_ExecuteCode(object sender, EventArgs e)
        {
            results = BLL.tabWiadomosci.Select_Batch(workflowProperties.Web);
            myEnum = results.GetEnumerator();

            if (results != null) logSelected_HistoryOutcome = results.Length.ToString();
            else logSelected_HistoryOutcome = "0";
        }

        private void whileRecordExist(object sender, ConditionalEventArgs e)
        {
            if (myEnum.MoveNext() && myEnum != null) e.Result = true;
            else e.Result = false;
        }

        private void Initialize_ChildWorkflow_ExecuteCode(object sender, EventArgs e)
        {
            item = myEnum.Current as SPListItem;

            SPWorkflow wf = BLL.Workflows.StartWorkflow(item, "Obsługa wiadomości");
            Debug.WriteLine("StartWorkflow: Obsługa wiadomości " + wf.InternalState.ToString());

            logSelected_HistoryOutcome = item.ID.ToString();

        }

        private void onWorkflowActivated1_Invoked(object sender, ExternalDataEventArgs e)
        {
            Debug.WriteLine("swfObslugaWiadomosciOczekujacych - ACTIVATED");
        }

        public String logSelected_HistoryOutcome = default(System.String);
        public String logCurrentMessage_HistoryOutcome = default(System.String);
        public String logWorkflow_HistoryOutcome = default(System.String);
        public String logErrorMessage_HistoryDescription = default(System.String);
        public String logErrorMessage_HistoryOutcome = default(System.String);
        private string _ZAKONCZONY = "Zakończony";
        private string _ANULOWANY = "Anulowany";

        private void cmdErrorHandler_ExecuteCode(object sender, EventArgs e)
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

        private void Update_Request_Completed_ExecuteCode(object sender, EventArgs e)
        {

            Update_Request(_ZAKONCZONY);
        }

        private void Update_Request_Canceled_ExecuteCode(object sender, EventArgs e)
        {
            Update_Request(_ANULOWANY);
        }


        private void Update_Request(string status)
        {
            if (workflowProperties.InitiationData != null)
            {
                int procesItemId = 0;

                try
                {
                    int.TryParse(workflowProperties.InitiationData.ToString(), out procesItemId);
                }
                catch (Exception ex)
                {

                    ElasticEmail.EmailGenerator.ReportError(ex, string.Format("workflowPorperties.InitiationData={0}", workflowProperties.InitiationData));
                }

                if (procesItemId > 0)
                {
                    Debug.Write("Element do akutalizacji: " + procesItemId.ToString());

                    SPListItem o = BLL.admProcesy.GetItemById(workflowProperties.Web, procesItemId);
                    if (o != null)
                    {
                        BLL.Tools.Set_Text(o, "enumStatusZlecenia", status);
                        o.SystemUpdate();
                    }
                }
            }
        }

        private void hasInitParamsNotNull(object sender, ConditionalEventArgs e)
        {
            if (!string.IsNullOrEmpty(workflowProperties.InitiationData)) e.Result = true;
        }

        public String msgSubject = default(System.String);
        public String msgTo = default(System.String);


        private void sendEmail1_MethodInvoking(object sender, EventArgs e)
        {
            msgTo = workflowProperties.OriginatorEmail;
        }

        private void hasResults(object sender, ConditionalEventArgs e)
        {
            if (results.Length > 0) e.Result = true;
        }

        public String msgBody = default(System.String);
        private StringBuilder sb;
        private SPListItem item;

        private void Init_MessageBody_ExecuteCode(object sender, EventArgs e)
        {
            if (results.Length > 0) sb = new StringBuilder();
        }

        private void Finalize_MessageBody_ExecuteCode(object sender, EventArgs e)
        {
            msgSubject = string.Format("Zlecenie wysyłki wiadomości zakończone ({0})", results.Length.ToString());

            if (sb != null)
            {
                msgBody = string.Format(@"<h3>Lista przetworzonych wiadomości</h3><ol>{0}</ol>", sb.ToString());
            }
        }

        private void Update_MessageBody_ExecuteCode(object sender, EventArgs e)
        {
            if (item != null)
            {
                sb.AppendFormat("<li>{0}::{1}</li>",
                        BLL.Tools.Get_LookupValue(item, "selKlient_NazwaSkrocona"),
                        item.Title);
            }
        }

        private void sendEmail2_MethodInvoking(object sender, EventArgs e)
        {
            msgTo = "stafix24@hotmail.com";
        }


    }
}
