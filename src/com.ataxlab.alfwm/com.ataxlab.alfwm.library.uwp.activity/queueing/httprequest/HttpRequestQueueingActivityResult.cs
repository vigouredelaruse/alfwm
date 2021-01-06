using com.ataxlab.alfwm.core.taxonomy.pipeline;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.library.uwp.activity.queueing.httprequest
{
    [JsonObject(MemberSerialization.OptIn)]
    public class HttpRequestQueueingActivityResult : IPipelineToolConfiguration<List<Tuple<String, String>>>, IPipelineToolConfiguration
    {

        public HttpRequestQueueingActivityResult()
        {
            Payload = new List<Tuple<string, string>>();
            Id = Guid.NewGuid().ToString();

        }

        [JsonProperty]
        public List<Tuple<string, string>> Payload {get; set; }

        [JsonProperty]
        public string Id {get; set; }

        [JsonProperty]
        public string Key {get; set; }

        [JsonProperty]
        public string DisplayName {get; set; }
        public DateTime DeploymentTime {get; set; }
        public string ConfigurationJson {get; set; }
        public string ConfigurationJsonSchema {get; set; }
        object IPipelineToolConfiguration.Configuration {get; set; }


        [JsonProperty]
        public System.Net.HttpStatusCode ResponseStatusCode { get; set; }


        [JsonProperty]
        public System.Net.Http.Headers.HttpResponseHeaders ResponseHeaders { get; set; }

        [JsonProperty]
        public string ReasonPhrase { get;  set; }
    }
}
