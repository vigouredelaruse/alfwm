using com.ataxlab.alfwm.core.taxonomy;
using com.ataxlab.alfwm.core.taxonomy.activity;
using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.WebUI;
using Windows.Web.Http;

namespace com.ataxlab.alfwm.library.activity.httpactivity
{
    public class HttpActivity : Activity<HttpActivityConfiguration>
    {
        private HttpClient httpClient = new HttpClient();

        public override event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public override event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public override event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public override event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        HttpActivityConfiguration RequiredParameters { get; set; }

        public HttpActivity() : base()
        {
            RequiredParameters = new HttpActivityConfiguration();
        }

        public override string PipelineToolInstanceId { get; set; }
        public override IPipelineToolStatus PipelineToolStatus { get; set; }
        public override IPipelineToolContext PipelineToolContext { get; set; }
        public override IPipelineToolConfiguration<HttpActivityConfiguration> PipelineToolConfiguration { get; set; }
        public override IPipelineToolBinding PipelineToolOutputBinding { get; set; }
        public override string PipelineToolId { get; set; }
        public override string PipelineToolDisplayName { get; set; }
        public override string PipelineToolDescription { get; set; }
        public override ObservableCollection<IPipelineVariable> PipelineToolVariables { get; set; }

        public override StopResult StopPipelineTool<StopResult>(string instanceId)
        {
            throw new NotImplementedException();
        }

        [Obsolete]
        public async void Start<StartResult>(Action<StartResult> callback)
        {

            StartResult result = default(StartResult); // = new StartResult();
            var config = PipelineToolConfiguration as HttpActivityConfiguration;

            try
            {
                PipelineToolStartEventArgs args = new PipelineToolStartEventArgs();
                args.InstanceId = this.PipelineToolInstanceId;

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
                    // result.StatusJson = data;
                    callback(result);
                    // OnPipelineToolCompleted<>(this, completionArgs);
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

        public override void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args)
        {
            throw new NotImplementedException();
        }

        public override void StartPipelineTool<StartResult, StartConfiguration>(StartConfiguration configuration, Func<StartConfiguration, StartResult> callback)
        {
            throw new NotImplementedException();
        }

  
        public override void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
