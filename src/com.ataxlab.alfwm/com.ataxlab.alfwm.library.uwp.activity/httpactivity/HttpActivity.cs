using com.ataxlab.alfwm.core.taxonomy;
using com.ataxlab.alfwm.core.taxonomy.activity;
using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.WebUI;
using Windows.Web.Http;

namespace com.ataxlab.alfwm.library.activity.httpactivity
{
    public class HttpActivity : Activity
    {
        private HttpClient httpClient = new HttpClient();

        HttpActivityConfiguration RequiredParameters { get; set; }

        public HttpActivity() : base()
        {
            RequiredParameters = new HttpActivityConfiguration();
        }

        public override string InstanceId { get; set; }
        public override IPipelineToolStatus Status { get; set; }
        public override IPipelineToolContext Context { get; set; }
        public override IPipelineToolConfiguration Configuration { get; set; }
        public override IPipelineToolBinding OutputBinding { get; set; }

        /// <summary>
        /// todo implement this
        /// </summary>
        /// <typeparam name="StartResult"></typeparam>
        /// <typeparam name="StartConfiguration"></typeparam>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public override void Start<StartResult, StartConfiguration>(StartConfiguration configuration, Action<StartResult> callback)
        {

        }

        public override StopResult Stop<StopResult>(string instanceId)
        {
            throw new NotImplementedException();
        }

        public async override void Start<StartResult>(Action<StartResult> callback)
        {

            StartResult result = new StartResult();
            var config = Configuration as HttpActivityConfiguration;

            try
            {
                PipelineToolStartEventArgs args = new PipelineToolStartEventArgs();
                args.InstanceId = this.InstanceId;

                OnPipelineToolStarted(this, args);
                
                try
                {
                    //                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", RequiredParameters.AuthorizationToken);
                    var uri = new Uri(config.HttpUrl);
                    var request = new HttpRequestMessage(config.HttpMethod, uri);
                    var response = await httpClient.SendRequestAsync(request); 
                    var data = response.ToString();

                    //if (RequiredParameters.AuthorizationToken != null)
                    //{
                    //    int i = 0;
                    //}

                    // trying out a couple of strategies
                    // for returning results
                    PipelineToolCompletedEventArgs completionArgs = new PipelineToolCompletedEventArgs();
                    completionArgs.Payload = data;
                    result.StatusJson = data;
                    callback(result);
                    OnPipelineToolCompleted(this, completionArgs);
                }
                catch(Exception ex)
                {
                    throw new HttpActivityException(ex.Message);
                }
            }
            catch(Exception e)
            { 
                throw new HttpActivityException(e.Message); 
            } 
        }
    }
}
