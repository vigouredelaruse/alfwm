using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.library.uwp.activity.queueing.httprequest
{
    public class HttpRequestQueueingActivityResult : IPipelineToolConfiguration<List<Tuple<String, String>>>, IPipelineToolConfiguration
    {
        public List<Tuple<string, string>> Payload {get; set; }
        public string Id {get; set; }
        public string Key {get; set; }
        public string DisplayName {get; set; }
        public DateTime DeploymentTime {get; set; }
        public string ConfigurationJson {get; set; }
        public string ConfigurationJsonSchema {get; set; }
        object IPipelineToolConfiguration.Configuration {get; set; }
    }
}
