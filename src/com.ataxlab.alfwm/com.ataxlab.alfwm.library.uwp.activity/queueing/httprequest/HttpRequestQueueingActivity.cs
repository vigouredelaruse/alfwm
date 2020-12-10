using com.ataxlab.alfwm.core.taxonomy;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.library.uwp.activity.queueing.httprequest
{
    /// <summary>
    /// canonical implementation of a Queueing Pipeline Tool
    /// that performs an HTTP Request
    /// it accepts the PipelineTool configuration as an Input Queue
    /// message and outputs a List of Tuple<string,string> 
    /// on its output queue binding
    /// </summary>
    public class HttpRequestQueueingActivity : QueueingPipelineToolBase<HttpRequestQueueingActivityConfiguration, List<Tuple<String, String>>, HttpRequestQueueingActivityConfiguration>
    {
        public HttpRequestQueueingActivity()
        {

        }

        public override void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args)
        {
            throw new NotImplementedException();
        }

        public override void OnQueueHasData(object sender, HttpRequestQueueingActivityConfiguration availableData)
        {
            throw new NotImplementedException();
        }

        public override void StartPipelineTool<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback)
        {
            this.PipelineToolConfiguration = new PipelineToolConfiguration<HttpRequestQueueingActivityConfiguration>() { Configuration = configuration as HttpRequestQueueingActivityConfiguration };
        }

        public override void StartPipelineTool(HttpRequestQueueingActivityConfiguration configuration, Action<HttpRequestQueueingActivityConfiguration> callback)
        {
            this.PipelineToolConfiguration = new PipelineToolConfiguration<HttpRequestQueueingActivityConfiguration>() { Configuration = configuration };
        }

        public override StopResult StopPipelineTool<StopResult>(string instanceId)
        {
            // TODO something useful here
            return default(StopResult);
                
        }
    }
}
