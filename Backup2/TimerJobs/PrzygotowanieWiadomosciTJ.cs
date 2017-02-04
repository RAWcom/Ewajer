using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Workflow;
using System.Diagnostics;

namespace Stafix.TimerJobs
{
    class PrzygotowanieWiadomosciTJ: Microsoft.SharePoint.Administration.SPJobDefinition
    {
        public static void CreateTimerJob(SPSite site)
        {
            var timerJob = new PrzygotowanieWiadomosciTJ(site);
            timerJob.Schedule = new SPDailySchedule
            {
                   BeginHour=6,
                   BeginMinute=0,
                   EndHour =6,
                   EndMinute = 15
            };

            timerJob.Update();
        }

        public static void DelteTimerJob(SPSite site)
        {
            site.WebApplication.JobDefinitions
                .OfType<PrzygotowanieWiadomosciTJ>()
                .Where(i => string.Equals(i.SiteUrl, site.Url, StringComparison.InvariantCultureIgnoreCase))
                .ToList()
                .ForEach(i => i.Delete());
        }

        public PrzygotowanieWiadomosciTJ()
            : base()
        {

        }

        public PrzygotowanieWiadomosciTJ(SPSite site)
            : base(string.Format("Ewajer_Przygotowanie wiadomosci ({0})", site.Url), site.WebApplication, null, SPJobLockType.Job)
        {
            Title = Name;
            SiteUrl = site.Url;
        }

        public string SiteUrl
        {
            get { return (string)this.Properties["SiteUrl"]; }
            set { this.Properties["SiteUrl"] = value; }
        }

        public override void Execute(Guid targetInstanceId)
        {
            using (var site = new SPSite(SiteUrl))
            {
                try
                {
                    SPWorkflow wf = BLL.Workflows.StartSiteWorkflow(site, "Obsługa kart kontrolnych", null);
                    Debug.WriteLine("StartSiteWorkflow: Obsługa kart kontrolnych " + wf.InternalState.ToString());
                }
                catch (Exception ex)
                {
                    ElasticEmail.EmailGenerator.ReportError(ex, site.Url);
                }
            }
        }
    }
}
