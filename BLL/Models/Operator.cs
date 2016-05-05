using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace BLL.Models
{
    public class Operator
    {
        public Operator(SPWeb web, int operatorId)
        {
            SPListItem item = BLL.dicOperatorzy.GetItemById(web, operatorId);
            this.Email = Get_Text(item, "colEmail");
            this.Telefon = Get_Text(item, "colTelefon");
            this.Name = item.Title;

            this.Stanowisko = string.Empty;
            try
            {
                this.Stanowisko = Get_Text(item, "colStanowisko");
            }
            catch (Exception)
            {}
        }

        private string Get_Text(SPListItem item, string col)
        {
            return item[col] != null ? item[col].ToString() : string.Empty;
        }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Telefon { get; set; }

        public string Stanowisko { get; set; }
    }
}
