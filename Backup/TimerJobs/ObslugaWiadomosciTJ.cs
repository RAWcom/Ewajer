using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System.Diagnostics;
using Microsoft.SharePoint.Workflow;

namespace Stafix.TimerJobs
{
    public class ObslugaWiadomosciTJ : Microsoft.SharePoint.Administration.SPJobDefinition
    {
        public static void CreateTimerJob(SPSite site)
        {
            var timerJob = new ObslugaWiadomosciTJ(site);
            timerJob.Schedule = new SPHourlySchedule
            {
                BeginMinute = 0,
                EndMinute = 15,
            };

            //timerJob.Schedule = new SPDailySchedule
            //{
            //    BeginHour = 18,
            //    BeginMinute = 0,
            //    EndHour = 18,
            //    EndMinute = 15
            //};

            timerJob.Update();
        }

        public static void DelteTimerJob(SPSite site)
        {
            site.WebApplication.JobDefinitions
                .OfType<ObslugaWiadomosciTJ>()
                .Where(i => string.Equals(i.SiteUrl, site.Url, StringComparison.InvariantCultureIgnoreCase))
                .ToList()
                .ForEach(i => i.Delete());
        }

        public ObslugaWiadomosciTJ()
            : base()
        {

        }

        public ObslugaWiadomosciTJ(SPSite site)
            : base(string.Format("Ewajer_Obsluga wiadomosci ({0})", site.Url), site.WebApplication, null, SPJobLockType.Job)
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
                    //nie chce zadziałać na serwerze produkcyjnym
                    SPWorkflow wf = BLL.Workflows.StartSiteWorkflow(site, "Obsługa wiadomości oczekujących",null);
                    Debug.WriteLine("StartSiteWorkflow: Obsługa wiadomości oczekujących " + wf.InternalState.ToString());

                    //using (SPWeb web = site.RootWeb)
                    //{
                    //    SPList list = web.Lists.TryGetList("admProcesy");
                    //    if (list != null)
                    //    {
                    //        SPListItem item = list.AddItem();
                    //        BLL.Tools.Set_Text(item, "ContentType", "Obsługa wiadomości");
                    //        item.Update();
                    //    }
                    //}   niew wywołuje zdarzenia obsługi procesu.
                }
                catch (Exception ex)
                {
                    ElasticEmail.EmailGenerator.ReportError(ex, site.Url);
                }
            }
        }
    }
}
