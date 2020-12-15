using com.ataxlab.alfwm.core.taxonomy;
using com.ataxlab.alfwm.core.taxonomy.activity;
using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
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
            /// todo something useful here
            return default(StopResult);
        }


        public override void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args)
        {
           PipelineToolCompleted?.Invoke(this, new PipelineToolCompletedEventArgs() { Payload = args.Payload });
        }

        private IAsyncAction HttpRequestAsyncAction;

        public async override void StartPipelineTool<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback)
        {
            HttpRequestAsyncAction = Windows.System.Threading.ThreadPool.RunAsync(
                async (state) =>
                {
                    await EnsureHttpRequest(configuration, callback);

                });
      
        }

        private async Task EnsureHttpRequest<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback) where StartConfiguration : class, IPipelineToolConfiguration, new()
        {
            this.PipelineToolConfiguration =
                new PipelineToolConfiguration<HttpActivityConfiguration>()
                { Payload = configuration as HttpActivityConfiguration }; // { configuration as HttpActivityConfiguration; ;

            // StartResult result = default(StartResult); // = new StartResult();
            // var config = configuration;

            try
            {
                PipelineToolStartEventArgs args = new PipelineToolStartEventArgs();
                args.InstanceId = this.PipelineToolInstanceId;

                OnPipelineToolStarted(this, args);

                try
                {
                    //                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", RequiredParameters.AuthorizationToken);
                    var uri = new Uri((configuration as HttpActivityConfiguration).HttpUrl); //  new Uri(this.PipelineToolConfiguration.Configuration.HttpUrl);
                    var request = new HttpRequestMessage(this.PipelineToolConfiguration.Payload.HttpMethod, uri);
                    var response = await httpClient.SendRequestAsync(request);
                    var data = await response.Content.ReadAsStringAsync();

                    //if (RequiredParameters.AuthorizationToken != null)
                    //{
                    //    int i = 0;
                    //}

                    // trying out a couple of strategies
                    // for returning results
                    PipelineToolCompletedEventArgs<PipelineVariable<String>> completionArgs = new PipelineToolCompletedEventArgs<PipelineVariable<String>>();
                    completionArgs.Payload = new PipelineVariable<String>() { Payload = data };
                    // result.StatusJson = data;
                    callback(configuration);
                    OnPipelineToolCompleted<PipelineVariable<String>>(this, completionArgs);
                }
                catch (Exception ex)
                {
                    OnPipelineToolFailed(this,
                        new PipelineToolFailedEventArgs()
                        {
                            InstanceId = this.PipelineToolInstanceId,
                            Status = new HttpActivityStatus() { StatusJson = JsonConvert.SerializeObject(ex) }
                        });
                }
            }
            catch (Exception e)
            {

                OnPipelineToolFailed(this,
                    new PipelineToolFailedEventArgs()
                    {
                        InstanceId = this.PipelineToolInstanceId,
                        Status = new HttpActivityStatus() { StatusJson = JsonConvert.SerializeObject(e) }
                    });
            }
        }

        public override void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args)
        {
           PipelineToolFailed?.Invoke(this, args);
        }

        public override void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            PipelineToolProgressUpdated?.Invoke(this, args);

        }

        public override void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args)
        {
            PipelineToolStarted?.Invoke(this, args);
        }

        public override void StartPipelineTool(HttpActivityConfiguration configuration, Action<HttpActivityConfiguration> callback)
        {
            this.PipelineToolConfiguration =  new PipelineToolConfiguration<HttpActivityConfiguration>() { Payload = configuration };
            HttpRequestAsyncAction = Windows.System.Threading.ThreadPool.RunAsync(
              async (state) =>
              {
                  await EnsureHttpRequest(configuration, callback);

              });
        }
    }
}
