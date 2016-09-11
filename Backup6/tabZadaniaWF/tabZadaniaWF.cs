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

namespace Workflows.tabZadaniaWF
{
    public sealed partial class tabZadaniaWF : SequentialWorkflowActivity
    {
        public tabZadaniaWF()
        {
            InitializeComponent();
        }

        public Guid workflowId = default(System.Guid);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();
        public String logErrorMessage_HistoryDescription = default(System.String);
        SPListItem item;

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

        private void onWorkflowActivated1_Invoked(object sender, ExternalDataEventArgs e)
        {
            Debug.WriteLine("tabZadaniaWF:{" + workflowProperties.WorkflowId + "} initiated");
            item = workflowProperties.Item;
        }


        // cmdFormatka
        private const string _CMD_ZAPISZ_WYNIKI = "Zapisz wyniki na karcie kontrolnej";
        private const string _CMD_ZATWIERDZ_I_WYSLIJ = "Zatwierdź wyniki i wyślij do klienta";
        private const string _CMD_ZATWIERDZ_I_ZAKONCZ = "Zatwierdź wyniki i zakończ zadanie";
        private const string _CMD_WYCOFAJ = "Wycofaj z karty kontrolnej";

        // enumStatusZadania
        private const string _ZADANIE_GOTOWE = "Gotowe";
        private const string _ZADANIE_ZWOLNIONE = "Zwolnione do wysyłki";
        private const string _ZADANIE_OBSLUGA = "Obsługa";
        private const string _ZADANIE_ZAKONCZONE = "Zakończone";
        private const string _ZADANIE_ANULOWANE = "Anulowane";





        private void Set_Operator(ref SPListItem item, int procId)
        {
            if (procId > 0 && item["selOperator"] == null)
            {
                int operatorId = BLL.tabProcedury.Get_OperatorById(item.Web, procId);
                if (operatorId > 0)
                {
                    item["selOperator"] = operatorId;
                }
            }
        }

        private void Set_TerminRealizacji(ref SPListItem item, int procId)
        {
            if (procId > 0 && (item["colTerminRealizacji"] == null || (DateTime)item["colTerminRealizacji"] != new DateTime()))
            {

                int termin = BLL.tabProcedury.Get_TerminRealizacjiOfsetById(item.Web, procId);
                if (termin > 0)
                {
                    item["colTerminRealizacji"] = DateTime.Today.AddDays(termin);
                }
            }
        }



        private SPListItem Set_OperatorUser(SPListItem item)
        {
            int operatorId = BLL.Tools.Get_LookupId(item, "selOperator");
            if (operatorId > 0)
            {
                int userId = BLL.dicOperatorzy.Get_UserIdById(item.Web, operatorId);
                BLL.Tools.Set_Value(item, "_KontoOperatora", userId);
            }
            else
            {
                BLL.Tools.Set_Value(item, "_KontoOperatora", 0);
            }
            return item;
        }

        #region ZUS
        private void Manage_ZUS(SPListItem item)
        {
            Debug.WriteLine("EventReceivers.tabZadania.tabZadania.Manage_ZUS");

            string cmd = BLL.Tools.Get_Text(item, "cmdFormatka");

            if (string.IsNullOrEmpty(cmd)) return;

            if (cmd.Equals(_CMD_WYCOFAJ))
            {
                Update_StatusZadania(item, cmd);
                return;
            }

            if (IsValid_ZUS_Form(item))
            {
                if (IsValid_ZUS_MessageDetails(item))
                {
                    if (BLL.admSetup.IsADEnabled(item.Web)
                        && BLL.Tools.Get_Flag(item, "colAudytDanych")
                        && (cmd.Equals(_CMD_ZATWIERDZ_I_ZAKONCZ) || cmd.Equals(_CMD_ZATWIERDZ_I_WYSLIJ)))
                    {
                        Set_StatusZadania(item, _ZADANIE_GOTOWE);

                    }
                    else
                    {
                        Update_StatusZadania(item, cmd);
                    }

                    BLL.tabKartyKontrolne.Update_ZUS_Data(item);
                }
            }
        }

        private bool IsValid_ZUS_Form(SPListItem item)
        {
            bool result = true;
            StringBuilder errLog = new StringBuilder();

            //Składki ZUS
            bool zp = BLL.Tools.Get_Flag(item, "colZatrudniaPracownikow");
            string fo = BLL.Tools.Get_Text(item, "colFormaOpodakowania_ZUS");
            switch (fo)
            {
                case "Tylko zdrowotna":
                    if (true)
                    {
                        if (zp)
                        {
                            if (HasValue(item, "colZUS_SP_Skladka")
                                && HasValue(item, "colZUS_ZD_Skladka")
                                && HasValue(item, "colZUS_FP_Skladka"))
                            { }
                            else
                            {
                                errLog.AppendLine("Nieprawidłowa warotść składki");
                                result = false;
                            }
                        }
                        else
                        {
                            if (HasValue(item, "colZUS_ZD_Skladka"))
                            { }
                            else
                            {
                                errLog.AppendLine("Nieprawidłowa warotść składki zdrowotnej");
                                result = false;

                                BLL.Tools.Set_Value(item, "colZUS_SP_Skladka", 0);
                                BLL.Tools.Set_Value(item, "colZUS_FP_Skladka", 0);
                            }
                        }
                    }
                    break;

                case "Tylko pracownicy":
                    if (!zp)
                    {
                        zp = true;
                        BLL.Tools.Set_Flag(item, "colZatrudniaPracownikow", zp);
                    }

                    if (HasValue(item, "colZUS_SP_Skladka")
                        & HasValue(item, "colZUS_ZD_Skladka")
                        & HasValue(item, "colZUS_FP_Skladka"))
                    { }
                    else
                    {
                        errLog.AppendLine("Nieprawidłowa warotść składki");
                        return result;
                    }


                    break;

                default:
                    if (HasValue(item, "colZUS_SP_Skladka")
                        & HasValue(item, "colZUS_ZD_Skladka")
                        & HasValue(item, "colZUS_FP_Skladka"))
                    { }
                    else
                    {
                        errLog.AppendLine("Nieprawidłowa warotść składki");
                        result = false;
                    }

                    break;

            }

            //PIT-4R
            if (result && zp)
            {
                bool pit4R = BLL.Tools.Get_Flag(item, "colZUS_PIT-4R_Zalaczony");
                if (pit4R)
                {
                    if (HasValue(item, "colZUS_PIT-4R"))
                    { }
                    else
                    {
                        errLog.AppendLine("Nieprawidłowa warotść PIT-4R");
                        result = false;
                    }
                }
                else BLL.Tools.Set_Value(item, "colZUS_PIT-4R", 0);
            }

            //PIT-8AR
            if (result && zp)
            {
                bool pit8AR = BLL.Tools.Get_Flag(item, "colZUS_PIT-8AR_Zalaczony");
                if (pit8AR)
                {
                    if (HasValue(item, "colZUS_PIT-8AR"))
                    { }
                    else
                    {
                        errLog.AppendLine("Nieprawidłowa warotść PIT-8AR");
                        result = false;
                    }
                }
                else BLL.Tools.Set_Value(item, "colZUS_PIT-8AR", 0);
            }

            if (!zp)
            {
                BLL.Tools.Set_Flag(item, "colZUS_PIT-4R_Zalaczony", false);
                BLL.Tools.Set_Value(item, "colZUS_PIT-4R", 0);
                BLL.Tools.Set_Flag(item, "colZUS_PIT-8AR_Zalaczony", false);
                BLL.Tools.Set_Value(item, "colZUS_PIT-8AR", 0);
            }


            //Załączniki
            bool lpz = BLL.Tools.Get_Flag(item, "colZUS_ListaPlac_Zalaczona");
            bool rz = BLL.Tools.Get_Flag(item, "colZUS_Rachunki_Zalaczone");

            if (result && lpz && rz)
            {
                if (item.Attachments.Count >= 2)
                { }
                else
                {
                    errLog.AppendLine("Brak załączników");
                    result = false;
                }

            }
            else if (result && (lpz || rz))
            {
                if (item.Attachments.Count >= 1)
                { }
                else
                {
                    errLog.AppendLine("Brak załączników");
                    result = false;
                }

            }

            Update_ValidationInfo(item, result, errLog);

            return result;
        }

        private static void Update_ValidationInfo(SPListItem item, bool result, StringBuilder errLog)
        {
            if (!result)
            {
                //ustaw flagę walidacji
                BLL.Tools.AppendNote_Top(item, "colNotatka", errLog.ToString(), true);
                BLL.Tools.Set_Flag(item, "_Validation", true);
            }
            else
            {
                //wyczyść flagę walidacji jeżeli ustawiona
                if (BLL.Tools.Get_Flag(item, "_Validation"))
                {
                    BLL.Tools.Set_Flag(item, "_Validation", false);
                }
            }
        }

        private bool IsValid_ZUS_MessageDetails(SPListItem item)
        {
            bool result = true;
            StringBuilder errLog = new StringBuilder();

            if (!HasIndex(item, "selOddzialZUS"))
            {
                errLog.AppendLine("Brak przypisania oddziału ZUS");
                result = false;
            };


            if (!HasText(item, "colFormaOpodakowania_ZUS"))
            {
                errLog.AppendLine("Brak informacji o formie opodatkowania");
                result = false;
            };



            if (!HasDate(item, "colZUS_TerminPlatnosciSkladek"))
            {
                errLog.AppendLine("Brak informacji o terminie płatności składek");
                result = false;
            };

            Update_ValidationInfo(item, result, errLog);

            return result;
        }


        #endregion

        #region PD
        private void Manage_PD(SPListItem item)
        {
            Debug.WriteLine("EventReceivers.tabZadania.tabZadania.Manage_PD");

            string cmd = BLL.Tools.Get_Text(item, "cmdFormatka");

            if (string.IsNullOrEmpty(cmd)) return;

            if (cmd.Equals(_CMD_WYCOFAJ))
            {
                Update_StatusZadania(item, cmd);
                return;
            }

            if (IsValid_PD_Form(item))
            {
                if (IsValid_PD_MessageDetails(item))
                {
                    if (BLL.admSetup.IsADEnabled(item.Web)
                        && BLL.Tools.Get_Flag(item, "colAudytDanych")
                        && (cmd.Equals(_CMD_ZATWIERDZ_I_ZAKONCZ) || cmd.Equals(_CMD_ZATWIERDZ_I_WYSLIJ)))
                    {
                        Set_StatusZadania(item, _ZADANIE_GOTOWE);
                    }
                    else
                    {
                        Update_StatusZadania(item, cmd);
                    }

                    BLL.tabKartyKontrolne.Update_PD_Data(item);
                }
            }
            else
            {

            }
        }

        private static void Update_StatusZadania(SPListItem item, string cmd)
        {
            Debug.WriteLine("EventReceivers.tabZadania.tabZadania.Update_StatusZadania");
            switch (cmd)
            {
                case _CMD_ZAPISZ_WYNIKI:
                    BLL.Tools.Set_Text(item, "enumStatusZadania", _ZADANIE_GOTOWE);
                    break;
                case _CMD_ZATWIERDZ_I_WYSLIJ:
                    BLL.Tools.Set_Text(item, "enumStatusZadania", _ZADANIE_ZWOLNIONE);
                    break;
                case _CMD_ZATWIERDZ_I_ZAKONCZ:
                    BLL.Tools.Set_Text(item, "enumStatusZadania", _ZADANIE_ZAKONCZONE);
                    break;
                case _CMD_WYCOFAJ:
                    BLL.Tools.Set_Text(item, "enumStatusZadania", _ZADANIE_OBSLUGA);
                    break;
            }

            Debug.WriteLine("enumStatusZadania=" + BLL.Tools.Get_Text(item, "enumStatusZadania"));
        }

        private bool IsValid_PD_Form(SPListItem item)
        {
            bool result = true;
            StringBuilder errLog = new StringBuilder();

            switch (BLL.Tools.Get_Text(item, "colPD_OcenaWyniku"))
            {
                case "Dochód":
                    if (HasValue(item, "colPD_WartoscDochodu")
                        & HasValue(item, "colPD_WartoscDoZaplaty"))
                    {
                        BLL.Tools.Set_Value(item, "colPD_WartoscStraty", 0);
                    }
                    else
                    {
                        errLog.AppendLine("Nieprawidłowa wartość dochodu lub do zapłaty");
                        result = false;
                    }

                    break;
                case "Strata":
                    if (HasValue(item, "colPD_WartoscStraty"))
                    {
                        BLL.Tools.Set_Value(item, "colPD_WartoscDochodu", 0);
                        BLL.Tools.Set_Value(item, "colPD_WartoscDoZaplaty", 0);
                    }
                    else
                    {
                        errLog.AppendLine("Nieprawidłowa wartość straty");
                        result = false;
                    }


                    break;
                default:
                    errLog.AppendLine("Nieprawidłowa wartość oceny wyniku");
                    result = false;
                    break;
            }

            if (result)
            {
                BLL.Tools.Set_Flag(item, "colPotwierdzenieOdbioruDokumentow", true);
            }

            Update_ValidationInfo(item, result, errLog);

            return result;
        }

        private bool IsValid_PD_MessageDetails(SPListItem item)
        {
            bool result = true;
            StringBuilder errLog = new StringBuilder();

            if (!HasIndex(item, "selUrzadSkarbowy"))
            {
                errLog.AppendLine("Brak przypisania urzędu skarbowego");
                result = false;
            };

            if (!HasText(item, "colFormaOpodatkowaniaPD"))
            {
                errLog.AppendLine("Brak informacji o formie opodatkowania");
                result = false;
            };

            if (!HasText(item, "enumRozliczeniePD"))
            {
                errLog.AppendLine("Brak informacji o sposobie rozliczenia podatku");
                result = false;
            };

            Update_ValidationInfo(item, result, errLog);

            return result;
        }


        #endregion

        #region PDS
        private void Manage_PDS(SPListItem item)
        {
            Debug.WriteLine("EventReceivers.tabZadania.tabZadania.Manage_PDS");

            string cmd = BLL.Tools.Get_Text(item, "cmdFormatka");

            if (string.IsNullOrEmpty(cmd)) return;

            if (cmd.Equals(_CMD_WYCOFAJ))
            {
                Update_StatusZadania(item, cmd);
                return;
            }

            if (IsValid_PDS_Form(item))
            {
                if (IsValid_PDS_MessageDetails(item))
                {
                    if (BLL.admSetup.IsADEnabled(item.Web)
                        && BLL.Tools.Get_Flag(item, "colAudytDanych")
                        && (cmd.Equals(_CMD_ZATWIERDZ_I_ZAKONCZ) || cmd.Equals(_CMD_ZATWIERDZ_I_WYSLIJ)))
                    {
                        Set_StatusZadania(item, _ZADANIE_GOTOWE);
                    }
                    else
                    {
                        Update_StatusZadania(item, cmd);
                    }
                }
            }
            else
            {

            }
        }

        private bool IsValid_PDS_Form(SPListItem item)
        {
            //todo: throw new NotImplementedException();
            return true;
        }

        private bool IsValid_PDS_MessageDetails(SPListItem item)
        {
            //todo: throw new NotImplementedException();
            return true;
        }
        #endregion

        #region VAT
        private void Manage_VAT(SPListItem item)
        {
            Debug.WriteLine("EventReceivers.tabZadania.tabZadania.Manage_VAT");

            string cmd = BLL.Tools.Get_Text(item, "cmdFormatka");

            if (string.IsNullOrEmpty(cmd)) return;

            if (cmd.Equals(_CMD_WYCOFAJ))
            {
                Update_StatusZadania(item, cmd);
                return;
            }

            if (IsValid_VAT_Form(item))
            {
                if (IsValid_VAT_MessageDetails(item))
                {
                    if (BLL.admSetup.IsADEnabled(item.Web)
                        && BLL.Tools.Get_Flag(item, "colAudytDanych")
                        && (cmd.Equals(_CMD_ZATWIERDZ_I_ZAKONCZ) || cmd.Equals(_CMD_ZATWIERDZ_I_WYSLIJ)))
                    {
                        Set_StatusZadania(item, _ZADANIE_GOTOWE);
                    }
                    else
                    {
                        Update_StatusZadania(item, cmd);
                    }

                    BLL.tabKartyKontrolne.Update_VAT_Data(item);

                    // opcojnalne generowanie formatek KKDVAT

                    if (BLL.admSetup.IsKKDVATEnabled(item.Web))
                    {
                        if (!BLL.Tools.Get_Text(item, "enumStatusZadania").Equals(_ZADANIE_GOTOWE)
                            && (cmd.Equals(_CMD_ZATWIERDZ_I_ZAKONCZ) || cmd.Equals(_CMD_ZATWIERDZ_I_WYSLIJ)))
                        {
                            if (BLL.Tools.Get_Flag(item, "colVAT_eDeklaracja"))
                            {
                                // wygeneruj zadanie KKDVAT
                                try
                                {
                                    BLL.tabZadania.Create_KKDVAT(item.Web, item);
                                }
                                catch (Exception ex)
                                {
                                    ElasticEmail.EmailGenerator.ReportError(ex, item.Web.Url.ToString());
                                }

                            }
                        }
                    }
                }
            }
            else
            {

            }
        }

        private bool IsValid_VAT_Form(SPListItem item)
        {
            bool result = true;
            StringBuilder errLog = new StringBuilder();

            switch (BLL.Tools.Get_Text(item, "colVAT_Decyzja"))
            {
                case "Do zapłaty":
                    if (HasValue(item, "colVAT_WartoscDoZaplaty"))
                    {
                        BLL.Tools.Set_Value(item, "colVAT_WartoscDoPrzeniesienia", 0);
                        BLL.Tools.Set_Value(item, "colVAT_WartoscDoZwrotu", 0);
                    }
                    else
                    {
                        errLog.AppendLine("Nieprawidłowa wartość do zapłaty");
                        result = false;
                    }

                    break;
                case "Do przeniesienia":
                    if (HasValue(item, "colVAT_WartoscDoPrzeniesienia"))
                    {
                        BLL.Tools.Set_Value(item, "colVAT_WartoscDoZaplaty", 0);
                        BLL.Tools.Set_Value(item, "colVAT_WartoscDoZwrotu", 0);
                    }
                    else
                    {
                        errLog.AppendLine("Nieprawidłowa wartość do przeniesienia");
                        result = false;
                    }

                    break;
                case "Do zwrotu":
                    if (HasValue(item, "colVAT_WartoscDoZwrotu"))
                    {
                        BLL.Tools.Set_Value(item, "colVAT_WartoscDoZaplaty", 0);
                        BLL.Tools.Set_Value(item, "colVAT_WartoscDoPrzeniesienia", 0);
                    }
                    else
                    {
                        errLog.AppendLine("Nieprawidłowa wartość do zwrotu");
                        result = false;
                    }

                    if (!HasText(item, "colVAT_TerminZwrotuPodatku"))
                    {
                        errLog.AppendLine("brak informacji o terminie zwrotu podatku");
                        result = false;
                    }

                    break;
                case "Do przeniesienia i do zwrotu":
                    if (HasValue(item, "colVAT_WartoscDoPrzeniesienia")
                        & HasValue(item, "colVAT_WartoscDoZwrotu"))
                    {
                        BLL.Tools.Set_Value(item, "colVAT_WartoscDoZaplaty", 0);
                    }
                    else
                    {
                        errLog.AppendLine("Nieprawidłowa wartość do przeniesienia lub do zwrotu");
                    }

                    if (!HasText(item, "colVAT_TerminZwrotuPodatku"))
                    {
                        errLog.AppendLine("brak informacji o terminie zwrotu podatku");
                        result = false;
                    }


                    break;
                default:

                    errLog.AppendLine("Nieprawidłowa decyzja dotycząca rozliczenia VAT");
                    result = false;
                    break;
            }

            if (!HasValue(item, "colVAT_WartoscNadwyzkiZaPoprzedniMiesiac"))
            {
                errLog.AppendLine("Nieprawidłowa wartość nadwyżki a poprzedni miesiąc");
                result = false;
            }

            Update_ValidationInfo(item, result, errLog);

            return result;
        }



        private bool IsValid_VAT_MessageDetails(SPListItem item)
        {
            bool result = true;
            StringBuilder errLog = new StringBuilder();

            if (!HasIndex(item, "selUrzadSkarbowy"))
            {
                errLog.AppendLine("Brak przypisania urzędu skarbowego");
                result = false;
            };

            if (!HasText(item, "colFormaOpodatkowaniaVAT"))
            {
                errLog.AppendLine("Brak informacji o formie opodatkowania");
                result = false;
            };

            if (!HasText(item, "enumRozliczenieVAT"))
            {
                errLog.AppendLine("Brak informacji o sposobie rozliczenia podatku VAT");
                result = false;
            };


            Update_ValidationInfo(item, result, errLog);

            return result;
        }
        #endregion

        #region RBR
        private void Manage_RBR(SPListItem item)
        {
            Debug.WriteLine("EventReceivers.tabZadania.tabZadania.Manage_RBR");

            string cmd = BLL.Tools.Get_Text(item, "cmdFormatka");

            if (string.IsNullOrEmpty(cmd)) return;

            if (cmd.Equals(_CMD_WYCOFAJ))
            {
                Update_StatusZadania(item, cmd);
                return;
            }

            if (IsValid_RBR_Form(item))
            {
                if (IsValid_RBR_MessageDetails(item))
                {
                    Update_StatusZadania(item, cmd);

                    BLL.tabKartyKontrolne.Update_RBR_Data(item);
                }
            }
            else
            {

            }
        }

        private bool IsValid_RBR_Form(SPListItem item)
        {
            bool result = true;
            StringBuilder errLog = new StringBuilder();

            result = result && HasDate(item, "colBR_DataWystawieniaFaktury");
            if (!result) errLog.AppendLine("Nieprawidłowa wartość dochodu lub do zapłaty");

            result = result && HasText(item, "colBR_NumerFaktury");
            if (!result) errLog.AppendLine("Nieprawidłowa wartość dochodu lub do zapłaty");

            result = result && HasValue(item, "colBR_WartoscDoZaplaty");
            if (!result) errLog.AppendLine("Nieprawidłowa wartość dochodu lub do zapłaty");

            result = result && HasDate(item, "colBR_TerminPlatnosci");
            if (!result) errLog.AppendLine("Nieprawidłowa wartość dochodu lub do zapłaty");

            Update_ValidationInfo(item, result, errLog);

            return result;
        }

        private bool IsValid_RBR_MessageDetails(SPListItem item)
        {
            return true;
        }
        #endregion

        private void Manage_Zadanie(SPListItem item)
        {
            Debug.WriteLine("EventReceivers.tabZadania.tabZadania.Manage_Zadanie");

            string cmd = BLL.Tools.Get_Text(item, "cmdFormatka_Zadanie");
            switch (cmd)
            {
                case "Zakończ":
                    Set_StatusZadania(item, _ZADANIE_ZAKONCZONE);
                    break;
                case "Anuluj":
                    Set_StatusZadania(item, _ZADANIE_ANULOWANE);
                    break;
            }


            Update_StatusZadania(item, cmd);

        }

        private void Set_StatusZadania(SPListItem item, string status)
        {
            BLL.Tools.Set_Text(item, "enumStatusZadania", status);
        }



        private bool HasValue(SPListItem item, string col)
        {
            double v = BLL.Tools.Get_Value(item, col);
            if (v >= 0) return true;
            else return false;
        }

        private bool HasIndex(SPListItem item, string col)
        {
            int idx = BLL.Tools.Get_LookupId(item, col);
            if (idx > 0) return true;
            else return false;
        }

        private bool HasDate(SPListItem item, string col)
        {
            DateTime d = BLL.Tools.Get_Date(item, col);
            if (d > new DateTime()) return true;
            else return false;
        }

        private bool HasText(SPListItem item, string col)
        {
            string s = BLL.Tools.Get_Text(item, col);
            if (!string.IsNullOrEmpty(s)) return true;
            else return false;
        }

        private void UpdateItem_ExecuteCode(object sender, EventArgs e)
        {
            item.SystemUpdate();
        }

        private void Set_Status_ExecuteCode(object sender, EventArgs e)
        {
            //status
            if (BLL.Tools.Get_Text(item, "enumStatusZadania").Equals("Nowe")
                && BLL.Tools.Get_Date(item, "Created").CompareTo(BLL.Tools.Get_Date(item, "Modified")) != 0)
            {
                BLL.Tools.Set_Text(item, "enumStatusZadania", "Obsługa");
                item.SystemUpdate();
            }

            BLL.Tools.Set_Text(item, "_Validation", string.Empty);
        }

        private void Set_Procedura_ExecuteCode(object sender, EventArgs e)
        {
            //obsługa procedury

            int procId = BLL.Tools.Get_LookupId(item, "selProcedura");
            if (procId == 0) //aktualizuj procedurę
            {
                switch (item.ContentType.Name)
                {
                    case "Wiadomość z ręki":
                    case "Wiadomość z szablonu":
                    case "Wiadomość grupowa":
                    case "Wiadomość grupowa z szablonu":
                    case "Informacja uzupełniająca":
                        procId = BLL.tabProcedury.Ensure(item.Web, ": " + item.ContentType.Name, true);
                        break;
                    default:
                        //przypisz procedurę na podstawie tematu
                        if (string.IsNullOrEmpty(item.Title))
                        {
                            BLL.Tools.Set_Text(item, "Title", ": " + item.ContentType.Name);
                            procId = BLL.tabProcedury.Ensure(item.Web, item.Title, true);
                        }
                        else
                        {
                            procId = BLL.tabProcedury.Ensure(item.Web, item.Title, false);
                        }
                        break;
                }

                //update procedura
                BLL.Tools.Set_Value(item, "selProcedura", (int)procId);
            }

            if (procId > 0)
            {
                //update termin realizacji
                Set_TerminRealizacji(ref item, procId);

                //update operatora
                Set_Operator(ref item, procId);
            }
        }

        private void Set_Title_ExecuteCode(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(item.Title))
            {
                BLL.Tools.Set_Text(item, "Title", BLL.Tools.Get_LookupValue(item, "selProcedura"));
            }
        }

        private void Set_Operator_ExecuteCode(object sender, EventArgs e)
        {
            //operator
            item = Set_OperatorUser(item);
        }


        private void Reset_Command_ExecuteCode(object sender, EventArgs e)
        {
            Debug.WriteLine("EventReceivers.tabZadania.tabZadania.Reset_CMD");

            BLL.Tools.Set_Text(item, "cmdFormatka", string.Empty);
            BLL.Tools.Set_Text(item, "cmdFormatka_Wiadomosc", string.Empty);
            BLL.Tools.Set_Text(item, "cmdFormatka_Zadanie", string.Empty);
        }

        private void isCT_Podatek(object sender, ConditionalEventArgs e)
        {
            if (item.ContentType.Name == "Rozliczenie podatku dochodowego"
                | item.ContentType.Name == "Rozliczenie podatku dochodowego spółki") e.Result = true;
        }

        private void Update_KK_OdbiorDok_ExecuteCode(object sender, EventArgs e)
        {
            BLL.tabKartyKontrolne.Update_POD(item);
        }

        public String logActiveCommand_HistoryOutcome = default(System.String);


        private void iscmdFormatka(object sender, ConditionalEventArgs e)
        {
            if (BLL.Tools.Get_Text(item, "cmdFormatka").Length > 0)
            {
                e.Result = true;
            }
        }

        private void iscmdFormatka_Zadanie(object sender, ConditionalEventArgs e)
        {
            if (BLL.Tools.Get_Text(item, "cmdFormatka_Zadanie").Length > 0)
            {
                e.Result = true;
            }
        }

        private void iscmdFormatka_Wiadomosc(object sender, ConditionalEventArgs e)
        {
            if (BLL.Tools.Get_Text(item, "cmdFormatka_Wiadomosc").Length > 0)
            {
                e.Result = true;
            }
        }

        private void Manage_cmdFormatka_ExecuteCode(object sender, EventArgs e)
        {
            string cmd = BLL.Tools.Get_Text(item, "cmdFormatka");
            logCommand_HistoryOutcome = cmd;

            switch (item.ContentType.Name)
            {

                case "Rozliczenie ZUS":
                    Manage_ZUS(item);
                    break;
                case "Rozliczenie podatku dochodowego":
                    Manage_PD(item);
                    break;
                case "Rozliczenie podatku dochodowego spółki":
                    Manage_PDS(item);
                    break;
                case "Rozliczenie podatku VAT":
                    Manage_VAT(item);
                    break;
                case "Rozliczenie z biurem rachunkowym":
                    Manage_RBR(item);
                    break;
            }
        }

        private void Manage_cmdFormatka_Wiadomosci_ExecuteCode(object sender, EventArgs e)
        {
            string cmd = BLL.Tools.Get_Text(item, "cmdFormatka_Wiadomosc");
            logCommand_HistoryOutcome = cmd;

            switch (item.ContentType.Name)
            {
                case "Wiadomość z ręki":
                case "Wiadomość z szablonu":
                case "Wiadomość grupowa":
                case "Wiadomość grupowa z szablonu":
                case "Informacja uzupełniająca":
                    BLL.tabWiadomosci.CreateMailMessage(item);
                    break;
            }
        }

        private void Manage_cmdForamatka_Zadania_ExecuteCode(object sender, EventArgs e)
        {
            string cmd = BLL.Tools.Get_Text(item, "cmdFormatka_Zadanie");
            logCommand_HistoryOutcome = cmd;

            switch (item.ContentType.Name)
            {
                case "Zadanie":
                    Manage_Zadanie(item);
                    break;
                default:
                    Manage_Zadanie(item);
                    break;

            }
        }

        public String logCommand_HistoryOutcome = default(System.String);

        private void Manage_NoCommand_ExecuteCode(object sender, EventArgs e)
        {
            logCommand_HistoryOutcome = "Not defined";
        }

        private void isSzablonZadania(object sender, ConditionalEventArgs e)
        {
            if (item.ContentType.Name.Equals("Rozliczenie podatku VAT")
                | item.ContentType.Name.Equals("Rozliczenie z biurem rachunkowym")
                | item.ContentType.Name.Equals("Rozliczenie ZUS")
                | item.ContentType.Name.Equals("Rozliczenie podatku dochodowego")
                | item.ContentType.Name.Equals("Rozliczenie podatku dochodowego spółki")
                | item.ContentType.Name.Equals("Informacja uzupełniająca")) e.Result = true;
        }

        private void isZadanie(object sender, ConditionalEventArgs e)
        {
            if (item.ContentType.Name.Equals("Zadanie")) e.Result = true;
        }

        private void isWiadomosc(object sender, ConditionalEventArgs e)
        {
            if (item.ContentType.Name.Equals("Wiadomość z szablonu")
                | item.ContentType.Name.Equals("Wiadomość z ręki")
                | item.ContentType.Name.Equals("Wiadomość grupowa z szablonu")
                | item.ContentType.Name.Equals("Wiadomość grupowa")) e.Result = true;
        }
    }
}
