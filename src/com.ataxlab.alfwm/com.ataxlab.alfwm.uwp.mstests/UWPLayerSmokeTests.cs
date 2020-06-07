
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using com.ataxlab.alfwm.core.scheduler;
using com.ataxlab.alfwm.core.taxonomy;
using com.ataxlab.alfwm.library.activity.httpactivity;
using com.ataxlab.alfwm.scheduler.windowsthreadpool;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.Web.Http;

namespace com.ataxlab.alfwm.uwp.mstests
{
    [TestClass]
    public class UnitTest
    {
        private bool activityCompleted = false;

        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void CanScheduleAndGetJobCompleted()
        {
            IScheduler scheduler = new WindowsThreadpoolScheduler();
            scheduler.ActivityStarted += OnActivityStarted;
            scheduler.ActivityCompleted += OnActivityCompleted;
            scheduler.ActivityProgressUpdated += OnActivityProgressUpdated;
            scheduler.ActivityFailed += OnActivityFailed;

            HttpActivity activity = new HttpActivity();

            HttpActivityConfiguration config = new HttpActivityConfiguration();
            config.HttpMethod = HttpMethod.Get;
            config.HttpUrl = "https://images-api.nasa.gov/search?q=apollo%2011";

            try
            {
                scheduler.StartActivity<HttpActivity, HttpActivityStatus, HttpActivityConfiguration>(activity, config, something =>
                {
                    Debug.WriteLine(String.Format("scheduler started with start callback data {0}", something.StatusJson));
                });
            }
            catch(Exception e)
            {
                int xxx = 99;
            }
            int i = 0;

            // give background task time enough to complete
            // i have no idea how many iterations to wait for
            // but it needs to be enough to complete debugging too
            while (i++ < 60 && activityCompleted == false)
                {
                    // now wait for background threads to complete 
                    Task.Delay(5).Wait();
                }

            Assert.IsTrue(activityCompleted, "test failed - scheduler did not report activity completed");
        }

        private void OnActivityFailed(object sender, PipelineToolFailedEventArgs e)
        {
            Debug.Write("pipeline failed");
        }

        private void OnActivityProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs arg2)
        {
            Debug.Write("pipeline progress updated");
        }

        private void OnActivityCompleted(object sender, PipelineToolCompletedEventArgs e)
        {
            Debug.WriteLine(String.Format("pipeline completed with payload {0}", e.Payload));
            activityCompleted = true;
        }

        private void OnActivityStarted(object sender, PipelineToolStartEventArgs e)
        {
            Debug.WriteLine(String.Format("Scheduler started activity with instance id {0}", e.InstanceId));
        }
    }
}
