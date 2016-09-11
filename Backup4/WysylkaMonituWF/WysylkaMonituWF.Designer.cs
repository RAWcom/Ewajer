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

namespace STAFix24_Bcfuture.WysylkaMonituWF
{
    public sealed partial class WysylkaMonituWF
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
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind11 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind12 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind13 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind14 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition4 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind16 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken1 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind15 = new System.Workflow.ComponentModel.ActivityBind();
            this.InitMail_Weryfikacja = new System.Workflow.Activities.CodeActivity();
            this.logToHistoryListActivity6 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.InitMail_Wysylka = new System.Workflow.Activities.CodeActivity();
            this.Send_RecipientReports = new System.Workflow.Activities.CodeActivity();
            this.logToHistoryListActivity5 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.Create_RecipientReportPart = new System.Workflow.Activities.CodeActivity();
            this.elseMode = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifModeWysylka = new System.Workflow.Activities.IfElseBranchActivity();
            this.sequence = new System.Workflow.Activities.SequenceActivity();
            this.InitMail_BrakDanych = new System.Workflow.Activities.CodeActivity();
            this.logToHistoryListActivity7 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.Check_Mode = new System.Workflow.Activities.IfElseActivity();
            this.whileActivity1 = new System.Workflow.Activities.WhileActivity();
            this.Get_RecipientEnumerator = new System.Workflow.Activities.CodeActivity();
            this.logToHistoryListActivity4 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.Validate_Recipients = new System.Workflow.Activities.CodeActivity();
            this.logToHistoryListActivity8 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.Select_RecipientsList = new System.Workflow.Activities.CodeActivity();
            this.logToHistoryListActivity3 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.ifElseBranchActivity3 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifRecordsFound = new System.Workflow.Activities.IfElseBranchActivity();
            this.terminateActivity1 = new System.Workflow.ComponentModel.TerminateActivity();
            this.logErrorMessage = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.ErrorHandler = new System.Workflow.Activities.CodeActivity();
            this.logToHistoryListActivity2 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.logToHistoryListActivity9 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.Send_SummaryReport = new System.Workflow.Activities.CodeActivity();
            this.Check_RecordsFound = new System.Workflow.Activities.IfElseActivity();
            this.Select_TargetList = new System.Workflow.Activities.CodeActivity();
            this.logToHistoryListActivity1 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.faultHandlerActivity1 = new System.Workflow.ComponentModel.FaultHandlerActivity();
            this.ifElseBranchActivity2 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifCTWysylkaMonitu = new System.Workflow.Activities.IfElseBranchActivity();
            this.faultHandlersActivity1 = new System.Workflow.ComponentModel.FaultHandlersActivity();
            this.Check_CT = new System.Workflow.Activities.IfElseActivity();
            this.onWorkflowActivated1 = new Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated();
            // 
            // InitMail_Weryfikacja
            // 
            this.InitMail_Weryfikacja.Name = "InitMail_Weryfikacja";
            this.InitMail_Weryfikacja.ExecuteCode += new System.EventHandler(this.InitMail_Weryfikacja_ExecuteCode);
            // 
            // logToHistoryListActivity6
            // 
            this.logToHistoryListActivity6.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity6.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logToHistoryListActivity6.HistoryDescription = "Mode<>Wysyłka";
            this.logToHistoryListActivity6.HistoryOutcome = "";
            this.logToHistoryListActivity6.Name = "logToHistoryListActivity6";
            this.logToHistoryListActivity6.OtherData = "";
            this.logToHistoryListActivity6.UserId = -1;
            // 
            // InitMail_Wysylka
            // 
            this.InitMail_Wysylka.Name = "InitMail_Wysylka";
            this.InitMail_Wysylka.ExecuteCode += new System.EventHandler(this.InitMail_Wysylka_ExecuteCode);
            // 
            // Send_RecipientReports
            // 
            this.Send_RecipientReports.Name = "Send_RecipientReports";
            this.Send_RecipientReports.ExecuteCode += new System.EventHandler(this.Create_RecipientsReports_ExecuteCode);
            // 
            // logToHistoryListActivity5
            // 
            this.logToHistoryListActivity5.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity5.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logToHistoryListActivity5.HistoryDescription = "Mode=Wysyłka";
            this.logToHistoryListActivity5.HistoryOutcome = "";
            this.logToHistoryListActivity5.Name = "logToHistoryListActivity5";
            this.logToHistoryListActivity5.OtherData = "";
            this.logToHistoryListActivity5.UserId = -1;
            // 
            // Create_RecipientReportPart
            // 
            this.Create_RecipientReportPart.Name = "Create_RecipientReportPart";
            this.Create_RecipientReportPart.ExecuteCode += new System.EventHandler(this.Create_RecipientReportPart_ExecuteCode);
            // 
            // elseMode
            // 
            this.elseMode.Activities.Add(this.logToHistoryListActivity6);
            this.elseMode.Activities.Add(this.InitMail_Weryfikacja);
            this.elseMode.Name = "elseMode";
            // 
            // ifModeWysylka
            // 
            this.ifModeWysylka.Activities.Add(this.logToHistoryListActivity5);
            this.ifModeWysylka.Activities.Add(this.Send_RecipientReports);
            this.ifModeWysylka.Activities.Add(this.InitMail_Wysylka);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isModeWysylka);
            this.ifModeWysylka.Condition = codecondition1;
            this.ifModeWysylka.Name = "ifModeWysylka";
            // 
            // sequence
            // 
            this.sequence.Activities.Add(this.Create_RecipientReportPart);
            this.sequence.Name = "sequence";
            // 
            // InitMail_BrakDanych
            // 
            this.InitMail_BrakDanych.Name = "InitMail_BrakDanych";
            this.InitMail_BrakDanych.ExecuteCode += new System.EventHandler(this.InitMail_BrakDanych_ExecuteCode);
            // 
            // logToHistoryListActivity7
            // 
            this.logToHistoryListActivity7.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity7.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logToHistoryListActivity7.HistoryDescription = "Brak rekordów spełniających kryteria";
            this.logToHistoryListActivity7.HistoryOutcome = "";
            this.logToHistoryListActivity7.Name = "logToHistoryListActivity7";
            this.logToHistoryListActivity7.OtherData = "";
            this.logToHistoryListActivity7.UserId = -1;
            // 
            // Check_Mode
            // 
            this.Check_Mode.Activities.Add(this.ifModeWysylka);
            this.Check_Mode.Activities.Add(this.elseMode);
            this.Check_Mode.Name = "Check_Mode";
            // 
            // whileActivity1
            // 
            this.whileActivity1.Activities.Add(this.sequence);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.whileRecordExist);
            this.whileActivity1.Condition = codecondition2;
            this.whileActivity1.Name = "whileActivity1";
            // 
            // Get_RecipientEnumerator
            // 
            this.Get_RecipientEnumerator.Name = "Get_RecipientEnumerator";
            this.Get_RecipientEnumerator.ExecuteCode += new System.EventHandler(this.GetRecipientEnumerator_ExecuteCode);
            // 
            // logToHistoryListActivity4
            // 
            this.logToHistoryListActivity4.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity4.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind1.Name = "WysylkaMonituWF";
            activitybind1.Path = "HistoryDescription";
            activitybind2.Name = "WysylkaMonituWF";
            activitybind2.Path = "HistoryOutcome";
            this.logToHistoryListActivity4.Name = "logToHistoryListActivity4";
            this.logToHistoryListActivity4.OtherData = "";
            this.logToHistoryListActivity4.UserId = -1;
            this.logToHistoryListActivity4.MethodInvoking += new System.EventHandler(this.logToHistoryListActivity4_MethodInvoking);
            this.logToHistoryListActivity4.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.logToHistoryListActivity4.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // Validate_Recipients
            // 
            this.Validate_Recipients.Name = "Validate_Recipients";
            this.Validate_Recipients.ExecuteCode += new System.EventHandler(this.Validate_Recipients_ExecuteCode);
            // 
            // logToHistoryListActivity8
            // 
            this.logToHistoryListActivity8.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity8.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind3.Name = "WysylkaMonituWF";
            activitybind3.Path = "HistoryDescription";
            activitybind4.Name = "WysylkaMonituWF";
            activitybind4.Path = "HistoryOutcome";
            this.logToHistoryListActivity8.Name = "logToHistoryListActivity8";
            this.logToHistoryListActivity8.OtherData = "";
            this.logToHistoryListActivity8.UserId = -1;
            this.logToHistoryListActivity8.MethodInvoking += new System.EventHandler(this.logToHistoryListActivity8_MethodInvoking);
            this.logToHistoryListActivity8.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.logToHistoryListActivity8.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            // 
            // Select_RecipientsList
            // 
            this.Select_RecipientsList.Name = "Select_RecipientsList";
            this.Select_RecipientsList.ExecuteCode += new System.EventHandler(this.Select_RecipientsList_ExecuteCode);
            // 
            // logToHistoryListActivity3
            // 
            this.logToHistoryListActivity3.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity3.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind5.Name = "WysylkaMonituWF";
            activitybind5.Path = "HistoryDescription";
            activitybind6.Name = "WysylkaMonituWF";
            activitybind6.Path = "HistoryOutcome";
            this.logToHistoryListActivity3.Name = "logToHistoryListActivity3";
            this.logToHistoryListActivity3.OtherData = "";
            this.logToHistoryListActivity3.UserId = -1;
            this.logToHistoryListActivity3.MethodInvoking += new System.EventHandler(this.logToHistoryListActivity3_MethodInvoking);
            this.logToHistoryListActivity3.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.logToHistoryListActivity3.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            // 
            // ifElseBranchActivity3
            // 
            this.ifElseBranchActivity3.Activities.Add(this.logToHistoryListActivity7);
            this.ifElseBranchActivity3.Activities.Add(this.InitMail_BrakDanych);
            this.ifElseBranchActivity3.Name = "ifElseBranchActivity3";
            // 
            // ifRecordsFound
            // 
            this.ifRecordsFound.Activities.Add(this.logToHistoryListActivity3);
            this.ifRecordsFound.Activities.Add(this.Select_RecipientsList);
            this.ifRecordsFound.Activities.Add(this.logToHistoryListActivity8);
            this.ifRecordsFound.Activities.Add(this.Validate_Recipients);
            this.ifRecordsFound.Activities.Add(this.logToHistoryListActivity4);
            this.ifRecordsFound.Activities.Add(this.Get_RecipientEnumerator);
            this.ifRecordsFound.Activities.Add(this.whileActivity1);
            this.ifRecordsFound.Activities.Add(this.Check_Mode);
            codecondition3.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.IsRecordsFound);
            this.ifRecordsFound.Condition = codecondition3;
            this.ifRecordsFound.Name = "ifRecordsFound";
            // 
            // terminateActivity1
            // 
            this.terminateActivity1.Name = "terminateActivity1";
            // 
            // logErrorMessage
            // 
            this.logErrorMessage.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logErrorMessage.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind7.Name = "WysylkaMonituWF";
            activitybind7.Path = "HistoryDescription";
            activitybind8.Name = "WysylkaMonituWF";
            activitybind8.Path = "HistoryOutcome";
            this.logErrorMessage.Name = "logErrorMessage";
            this.logErrorMessage.OtherData = "";
            this.logErrorMessage.UserId = -1;
            this.logErrorMessage.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            this.logErrorMessage.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            // 
            // ErrorHandler
            // 
            this.ErrorHandler.Name = "ErrorHandler";
            this.ErrorHandler.ExecuteCode += new System.EventHandler(this.ErrorHandler_ExecuteCode);
            // 
            // logToHistoryListActivity2
            // 
            this.logToHistoryListActivity2.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity2.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind9.Name = "WysylkaMonituWF";
            activitybind9.Path = "HistoryDescription";
            activitybind10.Name = "WysylkaMonituWF";
            activitybind10.Path = "HistoryOutcome";
            this.logToHistoryListActivity2.Name = "logToHistoryListActivity2";
            this.logToHistoryListActivity2.OtherData = "";
            this.logToHistoryListActivity2.UserId = -1;
            this.logToHistoryListActivity2.MethodInvoking += new System.EventHandler(this.logToHistoryListActivity2_MethodInvoking);
            this.logToHistoryListActivity2.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            this.logToHistoryListActivity2.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            // 
            // logToHistoryListActivity9
            // 
            this.logToHistoryListActivity9.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity9.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind11.Name = "WysylkaMonituWF";
            activitybind11.Path = "HistoryDescription";
            activitybind12.Name = "WysylkaMonituWF";
            activitybind12.Path = "HistoryOutcome";
            this.logToHistoryListActivity9.Name = "logToHistoryListActivity9";
            this.logToHistoryListActivity9.OtherData = "";
            this.logToHistoryListActivity9.UserId = -1;
            this.logToHistoryListActivity9.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            this.logToHistoryListActivity9.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            // 
            // Send_SummaryReport
            // 
            this.Send_SummaryReport.Name = "Send_SummaryReport";
            this.Send_SummaryReport.ExecuteCode += new System.EventHandler(this.Send_SummaryReport_ExecuteCode);
            // 
            // Check_RecordsFound
            // 
            this.Check_RecordsFound.Activities.Add(this.ifRecordsFound);
            this.Check_RecordsFound.Activities.Add(this.ifElseBranchActivity3);
            this.Check_RecordsFound.Name = "Check_RecordsFound";
            // 
            // Select_TargetList
            // 
            this.Select_TargetList.Name = "Select_TargetList";
            this.Select_TargetList.ExecuteCode += new System.EventHandler(this.Select_TargetList_ExecuteCode);
            // 
            // logToHistoryListActivity1
            // 
            this.logToHistoryListActivity1.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity1.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind13.Name = "WysylkaMonituWF";
            activitybind13.Path = "HistoryDescription";
            activitybind14.Name = "WysylkaMonituWF";
            activitybind14.Path = "HistoryOutcome";
            this.logToHistoryListActivity1.Name = "logToHistoryListActivity1";
            this.logToHistoryListActivity1.OtherData = "";
            this.logToHistoryListActivity1.UserId = -1;
            this.logToHistoryListActivity1.MethodInvoking += new System.EventHandler(this.logToHistoryListActivity1_MethodInvoking);
            this.logToHistoryListActivity1.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
            this.logToHistoryListActivity1.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind14)));
            // 
            // faultHandlerActivity1
            // 
            this.faultHandlerActivity1.Activities.Add(this.ErrorHandler);
            this.faultHandlerActivity1.Activities.Add(this.logErrorMessage);
            this.faultHandlerActivity1.Activities.Add(this.terminateActivity1);
            this.faultHandlerActivity1.FaultType = typeof(System.Exception);
            this.faultHandlerActivity1.Name = "faultHandlerActivity1";
            // 
            // ifElseBranchActivity2
            // 
            this.ifElseBranchActivity2.Activities.Add(this.logToHistoryListActivity2);
            this.ifElseBranchActivity2.Name = "ifElseBranchActivity2";
            // 
            // ifCTWysylkaMonitu
            // 
            this.ifCTWysylkaMonitu.Activities.Add(this.logToHistoryListActivity1);
            this.ifCTWysylkaMonitu.Activities.Add(this.Select_TargetList);
            this.ifCTWysylkaMonitu.Activities.Add(this.Check_RecordsFound);
            this.ifCTWysylkaMonitu.Activities.Add(this.Send_SummaryReport);
            this.ifCTWysylkaMonitu.Activities.Add(this.logToHistoryListActivity9);
            codecondition4.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isCTWysylkaMonitu);
            this.ifCTWysylkaMonitu.Condition = codecondition4;
            this.ifCTWysylkaMonitu.Name = "ifCTWysylkaMonitu";
            // 
            // faultHandlersActivity1
            // 
            this.faultHandlersActivity1.Activities.Add(this.faultHandlerActivity1);
            this.faultHandlersActivity1.Name = "faultHandlersActivity1";
            // 
            // Check_CT
            // 
            this.Check_CT.Activities.Add(this.ifCTWysylkaMonitu);
            this.Check_CT.Activities.Add(this.ifElseBranchActivity2);
            this.Check_CT.Name = "Check_CT";
            activitybind16.Name = "WysylkaMonituWF";
            activitybind16.Path = "workflowId";
            // 
            // onWorkflowActivated1
            // 
            correlationtoken1.Name = "workflowToken";
            correlationtoken1.OwnerActivityName = "WysylkaMonituWF";
            this.onWorkflowActivated1.CorrelationToken = correlationtoken1;
            this.onWorkflowActivated1.EventName = "OnWorkflowActivated";
            this.onWorkflowActivated1.Name = "onWorkflowActivated1";
            activitybind15.Name = "WysylkaMonituWF";
            activitybind15.Path = "workflowProperties";
            this.onWorkflowActivated1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind16)));
            this.onWorkflowActivated1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind15)));
            // 
            // WysylkaMonituWF
            // 
            this.Activities.Add(this.onWorkflowActivated1);
            this.Activities.Add(this.Check_CT);
            this.Activities.Add(this.faultHandlersActivity1);
            this.Name = "WysylkaMonituWF";
            this.CanModifyActivities = false;

        }

        #endregion

        private TerminateActivity terminateActivity1;

        private FaultHandlersActivity faultHandlersActivity1;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logErrorMessage;

        private CodeActivity ErrorHandler;

        private FaultHandlerActivity faultHandlerActivity1;

        private CodeActivity InitMail_BrakDanych;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity9;

        private CodeActivity Validate_Recipients;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity8;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity7;

        private IfElseBranchActivity ifElseBranchActivity3;

        private IfElseBranchActivity ifRecordsFound;

        private IfElseActivity Check_RecordsFound;

        private CodeActivity InitMail_Weryfikacja;

        private CodeActivity InitMail_Wysylka;

        private CodeActivity Send_SummaryReport;

        private CodeActivity Create_RecipientReportPart;

        private CodeActivity Send_RecipientReports;

        private SequenceActivity sequence;

        private WhileActivity whileActivity1;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity6;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity5;

        private CodeActivity Get_RecipientEnumerator;

        private IfElseBranchActivity elseMode;

        private IfElseBranchActivity ifModeWysylka;

        private IfElseActivity Check_Mode;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity4;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity3;

        private CodeActivity Select_RecipientsList;

        private CodeActivity Select_TargetList;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity2;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity1;

        private IfElseBranchActivity ifElseBranchActivity2;

        private IfElseBranchActivity ifCTWysylkaMonitu;

        private IfElseActivity Check_CT;

        private Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated onWorkflowActivated1;

















































    }
}
