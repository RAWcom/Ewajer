﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using System.Diagnostics;

namespace EventReceivers.admProcesy
{
    public class GFR_Request
    {
        public static void Create(Microsoft.SharePoint.SPListItem item)
        {

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
                    if (BLL.admSetup.IsRBREnabled(item.Web)
                        && BLL.Tools.Has_SerwisAssigned(k, "selSewisy", "RBR"))
                        Create_New_GFR_K(item, "RBR", list, k);
                    if (BLL.admSetup.IsRBEnabled(item.Web)
                        && BLL.Tools.Has_SerwisAssigned(k, "selSewisy", "RB"))
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

            SPWorkflow wf = BLL.Workflows.StartWorkflow(newItem, "Generuj formatki rozliczeniowe dla klienta");
            Debug.WriteLine("StartWorkflow: Generuj formatki rozliczeniowe dla klienta " + wf.InternalState.ToString());
        }
    }
}
