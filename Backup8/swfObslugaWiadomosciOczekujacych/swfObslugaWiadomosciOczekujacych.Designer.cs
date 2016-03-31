using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace Workflows.swfObslugaWiadomosciOczekujacych
{
    public sealed partial class swfObslugaWiadomosciOczekujacych
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken1 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind11 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind13 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind12 = new System.Workflow.ComponentModel.ActivityBind();
            this.Update_Request_Canceled = new System.Workflow.Activities.CodeActivity();
            this.logErrorMessage = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.cmdErrorHandler = new System.Workflow.Activities.CodeActivity();
            this.logReportSent = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.logMsgTo2 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.sendEmail2 = new Microsoft.SharePoint.WorkflowActions.SendEmail();
            this.logEmailSent = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.logMsgTo = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.sendEmail1 = new Microsoft.SharePoint.WorkflowActions.SendEmail();
            this.logRequestUpated = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.Update_Request_Completed = new System.Workflow.Activities.CodeActivity();
            this.Update_MessageBody = new System.Workflow.Activities.CodeActivity();
            this.Initialize_ChildWorkflow = new System.Workflow.Activities.CodeActivity();
            this.faultHandlerActivity1 = new System.Workflow.ComponentModel.FaultHandlerActivity();
            this.ifResults = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity2 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifInitParamsNotNull = new System.Workflow.Activities.IfElseBranchActivity();
            this.ObsługaPojedynczejWiadomości = new System.Workflow.Activities.SequenceActivity();
            this.faultHandlersActivity1 = new System.Workflow.ComponentModel.FaultHandlersActivity();
            this.logCompleted = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.Report = new System.Workflow.Activities.IfElseActivity();
            this.TestInitParams = new System.Workflow.Activities.IfElseActivity();
            this.Finalize_MessageBody = new System.Workflow.Activities.CodeActivity();
            this.whileActivity1 = new System.Workflow.Activities.WhileActivity();
            this.Init_MessageBody = new System.Workflow.Activities.CodeActivity();
            this.logSelected = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.Select_ListaWiadomosciOczekujacych = new System.Workflow.Activities.CodeActivity();
            this.onWorkflowActivated1 = new Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated();
            // 
            // Update_Request_Canceled
            // 
            this.Update_Request_Canceled.Name = "Update_Request_Canceled";
            this.Update_Request_Canceled.ExecuteCode += new System.EventHandler(this.Update_Request_Canceled_ExecuteCode);
            // 
            // logErrorMessage
            // 
            this.logErrorMessage.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logErrorMessage.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind1.Name = "swfObslugaWiadomosciOczekujacych";
            activitybind1.Path = "logErrorMessage_HistoryDescription";
            activitybind2.Name = "swfObslugaWiadomosciOczekujacych";
            activitybind2.Path = "logErrorMessage_HistoryOutcome";
            this.logErrorMessage.Name = "logErrorMessage";
            this.logErrorMessage.OtherData = "";
            this.logErrorMessage.UserId = -1;
            this.logErrorMessage.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.logErrorMessage.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // cmdErrorHandler
            // 
            this.cmdErrorHandler.Name = "cmdErrorHandler";
            this.cmdErrorHandler.ExecuteCode += new System.EventHandler(this.cmdErrorHandler_ExecuteCode);
            // 
            // logReportSent
            // 
            this.logReportSent.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logReportSent.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logReportSent.HistoryDescription = "Report";
            this.logReportSent.HistoryOutcome = "Sent";
            this.logReportSent.Name = "logReportSent";
            this.logReportSent.OtherData = "";
            this.logReportSent.UserId = -1;
            // 
            // logMsgTo2
            // 
            this.logMsgTo2.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logMsgTo2.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind3.Name = "swfObslugaWiadomosciOczekujacych";
            activitybind3.Path = "msgTo";
            this.logMsgTo2.HistoryOutcome = "";
            this.logMsgTo2.Name = "logMsgTo2";
            this.logMsgTo2.OtherData = "";
            this.logMsgTo2.UserId = -1;
            this.logMsgTo2.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            // 
            // sendEmail2
            // 
            this.sendEmail2.BCC = null;
            activitybind4.Name = "swfObslugaWiadomosciOczekujacych";
            activitybind4.Path = "msgBody";
            this.sendEmail2.CC = null;
            correlationtoken1.Name = "workflowToken";
            correlationtoken1.OwnerActivityName = "swfObslugaWiadomosciOczekujacych";
            this.sendEmail2.CorrelationToken = correlationtoken1;
            this.sendEmail2.From = null;
            this.sendEmail2.Headers = null;
            this.sendEmail2.IncludeStatus = false;
            this.sendEmail2.Name = "sendEmail2";
            activitybind5.Name = "swfObslugaWiadomosciOczekujacych";
            activitybind5.Path = "msgSubject";
            activitybind6.Name = "swfObslugaWiadomosciOczekujacych";
            activitybind6.Path = "msgTo";
            this.sendEmail2.MethodInvoking += new System.EventHandler(this.sendEmail2_MethodInvoking);
            this.sendEmail2.SetBinding(Microsoft.SharePoint.WorkflowActions.SendEmail.SubjectProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.sendEmail2.SetBinding(Microsoft.SharePoint.WorkflowActions.SendEmail.BodyProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.sendEmail2.SetBinding(Microsoft.SharePoint.WorkflowActions.SendEmail.ToProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            // 
            // logEmailSent
            // 
            this.logEmailSent.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logEmailSent.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logEmailSent.HistoryDescription = "Confirmation Email";
            this.logEmailSent.HistoryOutcome = "Sent";
            this.logEmailSent.Name = "logEmailSent";
            this.logEmailSent.OtherData = "";
            this.logEmailSent.UserId = -1;
            // 
            // logMsgTo
            // 
            this.logMsgTo.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logMsgTo.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind7.Name = "swfObslugaWiadomosciOczekujacych";
            activitybind7.Path = "msgTo";
            this.logMsgTo.HistoryOutcome = "";
            this.logMsgTo.Name = "logMsgTo";
            this.logMsgTo.OtherData = "";
            this.logMsgTo.UserId = -1;
            this.logMsgTo.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            // 
            // sendEmail1
            // 
            this.sendEmail1.BCC = null;
            activitybind8.Name = "swfObslugaWiadomosciOczekujacych";
            activitybind8.Path = "msgBody";
            this.sendEmail1.CC = null;
            this.sendEmail1.CorrelationToken = correlationtoken1;
            this.sendEmail1.From = null;
            this.sendEmail1.Headers = null;
            this.sendEmail1.IncludeStatus = false;
            this.sendEmail1.Name = "sendEmail1";
            activitybind9.Name = "swfObslugaWiadomosciOczekujacych";
            activitybind9.Path = "msgSubject";
            activitybind10.Name = "swfObslugaWiadomosciOczekujacych";
            activitybind10.Path = "msgTo";
            this.sendEmail1.MethodInvoking += new System.EventHandler(this.sendEmail1_MethodInvoking);
            this.sendEmail1.SetBinding(Microsoft.SharePoint.WorkflowActions.SendEmail.SubjectProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            this.sendEmail1.SetBinding(Microsoft.SharePoint.WorkflowActions.SendEmail.ToProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            this.sendEmail1.SetBinding(Microsoft.SharePoint.WorkflowActions.SendEmail.BodyProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            // 
            // logRequestUpated
            // 
            this.logRequestUpated.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logRequestUpated.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logRequestUpated.HistoryDescription = "Request";
            this.logRequestUpated.HistoryOutcome = "Updated";
            this.logRequestUpated.Name = "logRequestUpated";
            this.logRequestUpated.OtherData = "";
            this.logRequestUpated.UserId = -1;
            // 
            // Update_Request_Completed
            // 
            this.Update_Request_Completed.Name = "Update_Request_Completed";
            this.Update_Request_Completed.ExecuteCode += new System.EventHandler(this.Update_Request_Completed_ExecuteCode);
            // 
            // Update_MessageBody
            // 
            this.Update_MessageBody.Name = "Update_MessageBody";
            this.Update_MessageBody.ExecuteCode += new System.EventHandler(this.Update_MessageBody_ExecuteCode);
            // 
            // Initialize_ChildWorkflow
            // 
            this.Initialize_ChildWorkflow.Name = "Initialize_ChildWorkflow";
            this.Initialize_ChildWorkflow.ExecuteCode += new System.EventHandler(this.Initialize_ChildWorkflow_ExecuteCode);
            // 
            // faultHandlerActivity1
            // 
            this.faultHandlerActivity1.Activities.Add(this.cmdErrorHandler);
            this.faultHandlerActivity1.Activities.Add(this.logErrorMessage);
            this.faultHandlerActivity1.Activities.Add(this.Update_Request_Canceled);
            this.faultHandlerActivity1.FaultType = typeof(System.SystemException);
            this.faultHandlerActivity1.Name = "faultHandlerActivity1";
            // 
            // ifResults
            // 
            this.ifResults.Activities.Add(this.sendEmail2);
            this.ifResults.Activities.Add(this.logMsgTo2);
            this.ifResults.Activities.Add(this.logReportSent);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.hasResults);
            this.ifResults.Condition = codecondition1;
            this.ifResults.Name = "ifResults";
            // 
            // ifElseBranchActivity2
            // 
            this.ifElseBranchActivity2.Activities.Add(this.sendEmail1);
            this.ifElseBranchActivity2.Activities.Add(this.logMsgTo);
            this.ifElseBranchActivity2.Activities.Add(this.logEmailSent);
            this.ifElseBranchActivity2.Name = "ifElseBranchActivity2";
            // 
            // ifInitParamsNotNull
            // 
            this.ifInitParamsNotNull.Activities.Add(this.Update_Request_Completed);
            this.ifInitParamsNotNull.Activities.Add(this.logRequestUpated);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.hasInitParamsNotNull);
            this.ifInitParamsNotNull.Condition = codecondition2;
            this.ifInitParamsNotNull.Name = "ifInitParamsNotNull";
            // 
            // ObsługaPojedynczejWiadomości
            // 
            this.ObsługaPojedynczejWiadomości.Activities.Add(this.Initialize_ChildWorkflow);
            this.ObsługaPojedynczejWiadomości.Activities.Add(this.Update_MessageBody);
            this.ObsługaPojedynczejWiadomości.Name = "ObsługaPojedynczejWiadomości";
            // 
            // faultHandlersActivity1
            // 
            this.faultHandlersActivity1.Activities.Add(this.faultHandlerActivity1);
            this.faultHandlersActivity1.Name = "faultHandlersActivity1";
            // 
            // logCompleted
            // 
            this.logCompleted.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logCompleted.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logCompleted.HistoryDescription = "END";
            this.logCompleted.HistoryOutcome = "";
            this.logCompleted.Name = "logCompleted";
            this.logCompleted.OtherData = "";
            this.logCompleted.UserId = -1;
            // 
            // Report
            // 
            this.Report.Activities.Add(this.ifResults);
            this.Report.Name = "Report";
            // 
            // TestInitParams
            // 
            this.TestInitParams.Activities.Add(this.ifInitParamsNotNull);
            this.TestInitParams.Activities.Add(this.ifElseBranchActivity2);
            this.TestInitParams.Name = "TestInitParams";
            // 
            // Finalize_MessageBody
            // 
            this.Finalize_MessageBody.Name = "Finalize_MessageBody";
            this.Finalize_MessageBody.ExecuteCode += new System.EventHandler(this.Finalize_MessageBody_ExecuteCode);
            // 
            // whileActivity1
            // 
            this.whileActivity1.Activities.Add(this.ObsługaPojedynczejWiadomości);
            codecondition3.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.whileRecordExist);
            this.whileActivity1.Condition = codecondition3;
            this.whileActivity1.Name = "whileActivity1";
            // 
            // Init_MessageBody
            // 
            this.Init_MessageBody.Name = "Init_MessageBody";
            this.Init_MessageBody.ExecuteCode += new System.EventHandler(this.Init_MessageBody_ExecuteCode);
            // 
            // logSelected
            // 
            this.logSelected.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logSelected.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logSelected.HistoryDescription = "Liczba wiadomości do obsługi";
            activitybind11.Name = "swfObslugaWiadomosciOczekujacych";
            activitybind11.Path = "logSelected_HistoryOutcome";
            this.logSelected.Name = "logSelected";
            this.logSelected.OtherData = "";
            this.logSelected.UserId = -1;
            this.logSelected.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            // 
            // Select_ListaWiadomosciOczekujacych
            // 
            this.Select_ListaWiadomosciOczekujacych.Name = "Select_ListaWiadomosciOczekujacych";
            this.Select_ListaWiadomosciOczekujacych.ExecuteCode += new System.EventHandler(this.Select_ListaWiadomosciOczekujacych_ExecuteCode);
            activitybind13.Name = "swfObslugaWiadomosciOczekujacych";
            activitybind13.Path = "workflowId";
            // 
            // onWorkflowActivated1
            // 
            this.onWorkflowActivated1.CorrelationToken = correlationtoken1;
            this.onWorkflowActivated1.EventName = "OnWorkflowActivated";
            this.onWorkflowActivated1.Name = "onWorkflowActivated1";
            activitybind12.Name = "swfObslugaWiadomosciOczekujacych";
            activitybind12.Path = "workflowProperties";
            this.onWorkflowActivated1.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onWorkflowActivated1_Invoked);
            this.onWorkflowActivated1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
            this.onWorkflowActivated1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            // 
            // swfObslugaWiadomosciOczekujacych
            // 
            this.Activities.Add(this.onWorkflowActivated1);
            this.Activities.Add(this.Select_ListaWiadomosciOczekujacych);
            this.Activities.Add(this.logSelected);
            this.Activities.Add(this.Init_MessageBody);
            this.Activities.Add(this.whileActivity1);
            this.Activities.Add(this.Finalize_MessageBody);
            this.Activities.Add(this.TestInitParams);
            this.Activities.Add(this.Report);
            this.Activities.Add(this.logCompleted);
            this.Activities.Add(this.faultHandlersActivity1);
            this.Name = "swfObslugaWiadomosciOczekujacych";
            this.CanModifyActivities = false;

        }

        #endregion

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logMsgTo;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logMsgTo2;

        private CodeActivity Finalize_MessageBody;

        private CodeActivity Update_MessageBody;

        private CodeActivity Init_MessageBody;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logReportSent;

        private Microsoft.SharePoint.WorkflowActions.SendEmail sendEmail2;

        private IfElseBranchActivity ifResults;

        private IfElseActivity Report;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logEmailSent;

        private Microsoft.SharePoint.WorkflowActions.SendEmail sendEmail1;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logRequestUpated;

        private IfElseBranchActivity ifElseBranchActivity2;

        private IfElseBranchActivity ifInitParamsNotNull;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logCompleted;

        private IfElseActivity TestInitParams;

        private CodeActivity Update_Request_Canceled;

        private CodeActivity Update_Request_Completed;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logErrorMessage;

        private CodeActivity cmdErrorHandler;

        private FaultHandlerActivity faultHandlerActivity1;

        private FaultHandlersActivity faultHandlersActivity1;

        private CodeActivity Initialize_ChildWorkflow;

        private SequenceActivity ObsługaPojedynczejWiadomości;

        private WhileActivity whileActivity1;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logSelected;

        private CodeActivity Select_ListaWiadomosciOczekujacych;

        private Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated onWorkflowActivated1;









































    }
}
