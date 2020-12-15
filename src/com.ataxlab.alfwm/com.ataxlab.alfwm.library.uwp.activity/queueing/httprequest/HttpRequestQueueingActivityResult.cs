using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.library.uwp.activity.queueing.httprequest
{
    public class HttpRequestQueueingActivityResult : IPipelineToolConfiguration<List<Tuple<String, String>>>
    {
        public List<Tuple<string, string>> Payload { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Key { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string DisplayName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime DeploymentTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ConfigurationJson { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ConfigurationJsonSchema { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        object IPipelineToolConfiguration.Configuration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
