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

namespace Workflows.tabZadaniaWF
{
    public sealed partial class tabZadaniaWF
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
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition4 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition5 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition6 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken1 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            this.Update_KK_OdbiorDok = new System.Workflow.Activities.CodeActivity();
            this.logUpdateDOKInfo = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.ifCT_Podatek = new System.Workflow.Activities.IfElseBranchActivity();
            this.Reset_Command1 = new System.Workflow.Activities.CodeActivity();
            this.Manage_cmdForamatka_Zadania = new System.Workflow.Activities.CodeActivity();
            this.logcmdFormatka_Zadanie = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.Reset_Command2 = new System.Workflow.Activities.CodeActivity();
            this.Manage_cmdFormatka_Wiadomosci = new System.Workflow.Activities.CodeActivity();
            this.logcmdFormatka_Wiadomość = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.POD_info = new System.Workflow.Activities.IfElseActivity();
            this.Manage_NoCommand = new System.Workflow.Activities.CodeActivity();
            this.logCommandNonActive = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.Reset_Command = new System.Workflow.Activities.CodeActivity();
            this.Mange_cmdFormatka = new System.Workflow.Activities.CodeActivity();
            this.logToHistoryListActivity5 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.cmdFormatka_Zadanie = new System.Workflow.Activities.IfElseBranchActivity();
            this.cmdFormatka_Wiadomosc = new System.Workflow.Activities.IfElseBranchActivity();
            this.elseNoCommand = new System.Workflow.Activities.IfElseBranchActivity();
            this.cmdFormatka = new System.Workflow.Activities.IfElseBranchActivity();
            this.logErrorMessage = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.ErrorHandler = new System.Workflow.Activities.CodeActivity();
            this.Command_Zadanie = new System.Workflow.Activities.IfElseActivity();
            this.logToHistoryListActivity2 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.Command_Wiadomość = new System.Workflow.Activities.IfElseActivity();
            this.logToHistoryListActivity3 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.Command = new System.Workflow.Activities.IfElseActivity();
            this.logToHistoryListActivity1 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.faultHandlerActivity1 = new System.Workflow.ComponentModel.FaultHandlerActivity();
            this.elseZadanie = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifWiadomosc = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifSzablonZadania = new System.Workflow.Activities.IfElseBranchActivity();
            this.faultHandlersActivity1 = new System.Workflow.ComponentModel.FaultHandlersActivity();
            this.UpdateItem = new System.Workflow.Activities.CodeActivity();
            this.logCommand = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.ContentType = new System.Workflow.Activities.IfElseActivity();
            this.logSetupCompleted = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.Set_Operator1 = new System.Workflow.Activities.CodeActivity();
            this.Set_Title = new System.Workflow.Activities.CodeActivity();
            this.Set_Procedura = new System.Workflow.Activities.CodeActivity();
            this.Set_Status = new System.Workflow.Activities.CodeActivity();
            this.onWorkflowActivated1 = new Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated();
            // 
            // Update_KK_OdbiorDok
            // 
            this.Update_KK_OdbiorDok.Name = "Update_KK_OdbiorDok";
            this.Update_KK_OdbiorDok.ExecuteCode += new System.EventHandler(this.Update_KK_OdbiorDok_ExecuteCode);
            // 
            // logUpdateDOKInfo
            // 
            this.logUpdateDOKInfo.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logUpdateDOKInfo.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logUpdateDOKInfo.HistoryDescription = "Informacja o dostarczeniu dokumentów";
            this.logUpdateDOKInfo.HistoryOutcome = "Updated";
            this.logUpdateDOKInfo.Name = "logUpdateDOKInfo";
            this.logUpdateDOKInfo.OtherData = "";
            this.logUpdateDOKInfo.UserId = -1;
            // 
            // ifCT_Podatek
            // 
            this.ifCT_Podatek.Activities.Add(this.logUpdateDOKInfo);
            this.ifCT_Podatek.Activities.Add(this.Update_KK_OdbiorDok);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isCT_Podatek);
            this.ifCT_Podatek.Condition = codecondition1;
            this.ifCT_Podatek.Name = "ifCT_Podatek";
            // 
            // Reset_Command1
            // 
            this.Reset_Command1.Name = "Reset_Command1";
            this.Reset_Command1.ExecuteCode += new System.EventHandler(this.Reset_Command_ExecuteCode);
            // 
            // Manage_cmdForamatka_Zadania
            // 
            this.Manage_cmdForamatka_Zadania.Name = "Manage_cmdForamatka_Zadania";
            this.Manage_cmdForamatka_Zadania.ExecuteCode += new System.EventHandler(this.Manage_cmdForamatka_Zadania_ExecuteCode);
            // 
            // logcmdFormatka_Zadanie
            // 
            this.logcmdFormatka_Zadanie.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logcmdFormatka_Zadanie.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logcmdFormatka_Zadanie.HistoryDescription = "cmdFormatka_Zadanie";
            this.logcmdFormatka_Zadanie.HistoryOutcome = "Active";
            this.logcmdFormatka_Zadanie.Name = "logcmdFormatka_Zadanie";
            this.logcmdFormatka_Zadanie.OtherData = "";
            this.logcmdFormatka_Zadanie.UserId = -1;
            // 
            // Reset_Command2
            // 
            this.Reset_Command2.Name = "Reset_Command2";
            this.Reset_Command2.ExecuteCode += new System.EventHandler(this.Reset_Command_ExecuteCode);
            // 
            // Manage_cmdFormatka_Wiadomosci
            // 
            this.Manage_cmdFormatka_Wiadomosci.Name = "Manage_cmdFormatka_Wiadomosci";
            this.Manage_cmdFormatka_Wiadomosci.ExecuteCode += new System.EventHandler(this.Manage_cmdFormatka_Wiadomosci_ExecuteCode);
            // 
            // logcmdFormatka_Wiadomość
            // 
            this.logcmdFormatka_Wiadomość.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logcmdFormatka_Wiadomość.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logcmdFormatka_Wiadomość.HistoryDescription = "cmdFormatka_Wiadomosc";
            this.logcmdFormatka_Wiadomość.HistoryOutcome = "Active";
            this.logcmdFormatka_Wiadomość.Name = "logcmdFormatka_Wiadomość";
            this.logcmdFormatka_Wiadomość.OtherData = "";
            this.logcmdFormatka_Wiadomość.UserId = -1;
            // 
            // POD_info
            // 
            this.POD_info.Activities.Add(this.ifCT_Podatek);
            this.POD_info.Name = "POD_info";
            // 
            // Manage_NoCommand
            // 
            this.Manage_NoCommand.Name = "Manage_NoCommand";
            this.Manage_NoCommand.ExecuteCode += new System.EventHandler(this.Manage_NoCommand_ExecuteCode);
            // 
            // logCommandNonActive
            // 
            this.logCommandNonActive.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logCommandNonActive.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logCommandNonActive.HistoryDescription = "Command";
            this.logCommandNonActive.HistoryOutcome = "Non active";
            this.logCommandNonActive.Name = "logCommandNonActive";
            this.logCommandNonActive.OtherData = "";
            this.logCommandNonActive.UserId = -1;
            // 
            // Reset_Command
            // 
            this.Reset_Command.Name = "Reset_Command";
            this.Reset_Command.ExecuteCode += new System.EventHandler(this.Reset_Command_ExecuteCode);
            // 
            // Mange_cmdFormatka
            // 
            this.Mange_cmdFormatka.Name = "Mange_cmdFormatka";
            this.Mange_cmdFormatka.ExecuteCode += new System.EventHandler(this.Manage_cmdFormatka_ExecuteCode);
            // 
            // logToHistoryListActivity5
            // 
            this.logToHistoryListActivity5.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity5.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logToHistoryListActivity5.HistoryDescription = "cmdFormatka";
            this.logToHistoryListActivity5.HistoryOutcome = "Active";
            this.logToHistoryListActivity5.Name = "logToHistoryListActivity5";
            this.logToHistoryListActivity5.OtherData = "";
            this.logToHistoryListActivity5.UserId = -1;
            // 
            // cmdFormatka_Zadanie
            // 
            this.cmdFormatka_Zadanie.Activities.Add(this.logcmdFormatka_Zadanie);
            this.cmdFormatka_Zadanie.Activities.Add(this.Manage_cmdForamatka_Zadania);
            this.cmdFormatka_Zadanie.Activities.Add(this.Reset_Command1);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.iscmdFormatka_Zadanie);
            this.cmdFormatka_Zadanie.Condition = codecondition2;
            this.cmdFormatka_Zadanie.Name = "cmdFormatka_Zadanie";
            // 
            // cmdFormatka_Wiadomosc
            // 
            this.cmdFormatka_Wiadomosc.Activities.Add(this.logcmdFormatka_Wiadomość);
            this.cmdFormatka_Wiadomosc.Activities.Add(this.Manage_cmdFormatka_Wiadomosci);
            this.cmdFormatka_Wiadomosc.Activities.Add(this.Reset_Command2);
            codecondition3.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.iscmdFormatka_Wiadomosc);
            this.cmdFormatka_Wiadomosc.Condition = codecondition3;
            this.cmdFormatka_Wiadomosc.Name = "cmdFormatka_Wiadomosc";
            // 
            // elseNoCommand
            // 
            this.elseNoCommand.Activities.Add(this.logCommandNonActive);
            this.elseNoCommand.Activities.Add(this.Manage_NoCommand);
            this.elseNoCommand.Activities.Add(this.POD_info);
            this.elseNoCommand.Name = "elseNoCommand";
            // 
            // cmdFormatka
            // 
            this.cmdFormatka.Activities.Add(this.logToHistoryListActivity5);
            this.cmdFormatka.Activities.Add(this.Mange_cmdFormatka);
            this.cmdFormatka.Activities.Add(this.Reset_Command);
            codecondition4.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.iscmdFormatka);
            this.cmdFormatka.Condition = codecondition4;
            this.cmdFormatka.Name = "cmdFormatka";
            // 
            // logErrorMessage
            // 
            this.logErrorMessage.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logErrorMessage.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind1.Name = "tabZadaniaWF";
            activitybind1.Path = "logErrorMessage_HistoryDescription";
            this.logErrorMessage.HistoryOutcome = "";
            this.logErrorMessage.Name = "logErrorMessage";
            this.logErrorMessage.OtherData = "";
            this.logErrorMessage.UserId = -1;
            this.logErrorMessage.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // ErrorHandler
            // 
            this.ErrorHandler.Name = "ErrorHandler";
            this.ErrorHandler.ExecuteCode += new System.EventHandler(this.ErrorHandler_ExecuteCode);
            // 
            // Command_Zadanie
            // 
            this.Command_Zadanie.Activities.Add(this.cmdFormatka_Zadanie);
            this.Command_Zadanie.Name = "Command_Zadanie";
            // 
            // logToHistoryListActivity2
            // 
            this.logToHistoryListActivity2.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity2.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logToHistoryListActivity2.HistoryDescription = "Zadanie";
            this.logToHistoryListActivity2.HistoryOutcome = "";
            this.logToHistoryListActivity2.Name = "logToHistoryListActivity2";
            this.logToHistoryListActivity2.OtherData = "";
            this.logToHistoryListActivity2.UserId = -1;
            // 
            // Command_Wiadomość
            // 
            this.Command_Wiadomość.Activities.Add(this.cmdFormatka_Wiadomosc);
            this.Command_Wiadomość.Name = "Command_Wiadomość";
            // 
            // logToHistoryListActivity3
            // 
            this.logToHistoryListActivity3.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity3.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logToHistoryListActivity3.HistoryDescription = "Wiadomość";
            this.logToHistoryListActivity3.HistoryOutcome = "";
            this.logToHistoryListActivity3.Name = "logToHistoryListActivity3";
            this.logToHistoryListActivity3.OtherData = "";
            this.logToHistoryListActivity3.UserId = -1;
            // 
            // Command
            // 
            this.Command.Activities.Add(this.cmdFormatka);
            this.Command.Activities.Add(this.elseNoCommand);
            this.Command.Name = "Command";
            // 
            // logToHistoryListActivity1
            // 
            this.logToHistoryListActivity1.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity1.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logToHistoryListActivity1.HistoryDescription = "Szablon zadania";
            this.logToHistoryListActivity1.HistoryOutcome = "";
            this.logToHistoryListActivity1.Name = "logToHistoryListActivity1";
            this.logToHistoryListActivity1.OtherData = "";
            this.logToHistoryListActivity1.UserId = -1;
            // 
            // faultHandlerActivity1
            // 
            this.faultHandlerActivity1.Activities.Add(this.ErrorHandler);
            this.faultHandlerActivity1.Activities.Add(this.logErrorMessage);
            this.faultHandlerActivity1.FaultType = typeof(System.Exception);
            this.faultHandlerActivity1.Name = "faultHandlerActivity1";
            // 
            // elseZadanie
            // 
            this.elseZadanie.Activities.Add(this.logToHistoryListActivity2);
            this.elseZadanie.Activities.Add(this.Command_Zadanie);
            this.elseZadanie.Name = "elseZadanie";
            // 
            // ifWiadomosc
            // 
            this.ifWiadomosc.Activities.Add(this.logToHistoryListActivity3);
            this.ifWiadomosc.Activities.Add(this.Command_Wiadomość);
            codecondition5.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isWiadomosc);
            this.ifWiadomosc.Condition = codecondition5;
            this.ifWiadomosc.Name = "ifWiadomosc";
            // 
            // ifSzablonZadania
            // 
            this.ifSzablonZadania.Activities.Add(this.logToHistoryListActivity1);
            this.ifSzablonZadania.Activities.Add(this.Command);
            codecondition6.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isSzablonZadania);
            this.ifSzablonZadania.Condition = codecondition6;
            this.ifSzablonZadania.Name = "ifSzablonZadania";
            // 
            // faultHandlersActivity1
            // 
            this.faultHandlersActivity1.Activities.Add(this.faultHandlerActivity1);
            this.faultHandlersActivity1.Name = "faultHandlersActivity1";
            // 
            // UpdateItem
            // 
            this.UpdateItem.Name = "UpdateItem";
            this.UpdateItem.ExecuteCode += new System.EventHandler(this.UpdateItem_ExecuteCode);
            // 
            // logCommand
            // 
            this.logCommand.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logCommand.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logCommand.HistoryDescription = "Command";
            activitybind2.Name = "tabZadaniaWF";
            activitybind2.Path = "logCommand_HistoryOutcome";
            this.logCommand.Name = "logCommand";
            this.logCommand.OtherData = "";
            this.logCommand.UserId = -1;
            this.logCommand.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // ContentType
            // 
            this.ContentType.Activities.Add(this.ifSzablonZadania);
            this.ContentType.Activities.Add(this.ifWiadomosc);
            this.ContentType.Activities.Add(this.elseZadanie);
            this.ContentType.Name = "ContentType";
            // 
            // logSetupCompleted
            // 
            this.logSetupCompleted.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logSetupCompleted.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logSetupCompleted.HistoryDescription = "Setup";
            this.logSetupCompleted.HistoryOutcome = "Completed";
            this.logSetupCompleted.Name = "logSetupCompleted";
            this.logSetupCompleted.OtherData = "";
            this.logSetupCompleted.UserId = -1;
            // 
            // Set_Operator1
            // 
            this.Set_Operator1.Name = "Set_Operator1";
            this.Set_Operator1.ExecuteCode += new System.EventHandler(this.Set_Operator_ExecuteCode);
            // 
            // Set_Title
            // 
            this.Set_Title.Name = "Set_Title";
            this.Set_Title.ExecuteCode += new System.EventHandler(this.Set_Title_ExecuteCode);
            // 
            // Set_Procedura
            // 
            this.Set_Procedura.Name = "Set_Procedura";
            this.Set_Procedura.ExecuteCode += new System.EventHandler(this.Set_Procedura_ExecuteCode);
            // 
            // Set_Status
            // 
            this.Set_Status.Name = "Set_Status";
            this.Set_Status.ExecuteCode += new System.EventHandler(this.Set_Status_ExecuteCode);
            activitybind4.Name = "tabZadaniaWF";
            activitybind4.Path = "workflowId";
            // 
            // onWorkflowActivated1
            // 
            correlationtoken1.Name = "workflowToken";
            correlationtoken1.OwnerActivityName = "tabZadaniaWF";
            this.onWorkflowActivated1.CorrelationToken = correlationtoken1;
            this.onWorkflowActivated1.EventName = "OnWorkflowActivated";
            this.onWorkflowActivated1.Name = "onWorkflowActivated1";
            activitybind3.Name = "tabZadaniaWF";
            activitybind3.Path = "workflowProperties";
            this.onWorkflowActivated1.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onWorkflowActivated1_Invoked);
            this.onWorkflowActivated1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.onWorkflowActivated1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            // 
            // tabZadaniaWF
            // 
            this.Activities.Add(this.onWorkflowActivated1);
            this.Activities.Add(this.Set_Status);
            this.Activities.Add(this.Set_Procedura);
            this.Activities.Add(this.Set_Title);
            this.Activities.Add(this.Set_Operator1);
            this.Activities.Add(this.logSetupCompleted);
            this.Activities.Add(this.ContentType);
            this.Activities.Add(this.logCommand);
            this.Activities.Add(this.UpdateItem);
            this.Activities.Add(this.faultHandlersActivity1);
            this.Name = "tabZadaniaWF";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity Update_KK_OdbiorDok;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logUpdateDOKInfo;

        private IfElseBranchActivity ifCT_Podatek;

        private CodeActivity Reset_Command2;

        private CodeActivity Manage_cmdFormatka_Wiadomosci;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logcmdFormatka_Wiadomość;

        private CodeActivity Reset_Command1;

        private CodeActivity Manage_cmdForamatka_Zadania;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logcmdFormatka_Zadanie;

        private IfElseActivity POD_info;

        private CodeActivity Manage_NoCommand;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logCommandNonActive;

        private CodeActivity Reset_Command;

        private CodeActivity Mange_cmdFormatka;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity5;

        private IfElseBranchActivity cmdFormatka_Wiadomosc;

        private IfElseBranchActivity cmdFormatka_Zadanie;

        private IfElseBranchActivity elseNoCommand;

        private IfElseBranchActivity cmdFormatka;

        private IfElseActivity Command_Wiadomość;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity3;

        private IfElseActivity Command_Zadanie;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity2;

        private IfElseActivity Command;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity1;

        private IfElseBranchActivity ifWiadomosc;

        private IfElseBranchActivity elseZadanie;

        private IfElseBranchActivity ifSzablonZadania;

        private IfElseActivity ContentType;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logCommand;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logSetupCompleted;

        private CodeActivity Set_Operator1;

        private CodeActivity Set_Title;

        private CodeActivity Set_Procedura;

        private CodeActivity Set_Status;

        private CodeActivity UpdateItem;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logErrorMessage;

        private CodeActivity ErrorHandler;

        private FaultHandlerActivity faultHandlerActivity1;

        private FaultHandlersActivity faultHandlersActivity1;

        private Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated onWorkflowActivated1;
















































    }
}
