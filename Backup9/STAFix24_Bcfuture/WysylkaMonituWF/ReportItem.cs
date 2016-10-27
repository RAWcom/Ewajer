using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STAFix24_Bcfuture.Models
{
    class ReportItem
    {
        public decimal DoZaplaty;
        public decimal Zaplacono;
        public string Tytulem;
        public string NumerFaktury;
        public DateTime DataWystawieniaFaktury;
        public DateTime TerminPlatnosci;

        public decimal Roznica()
        {
            return this.DoZaplaty - this.Zaplacono;
        }
    }
}
