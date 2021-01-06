using com.ataxlab.alfwm.core.taxonomy.pipeline;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.library.uwp.activity.queueing.httprequest
{
    /// <summary>
    /// pipeline message emitted by HttpRequeustQueueingActivity
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class HttpRequestQueueingActivityResult : IPipelineToolConfiguration<List<Tuple<String, String>>>, IPipelineToolConfiguration
    {

        public HttpRequestQueueingActivityResult()
        {
            Payload = new List<Tuple<string, string>>();
            Id = Guid.NewGuid().ToString();

            ResponseHeaders = new List<Tuple<string, List<string>>>();
            RequestHeaders = new List<Tuple<string, List<string>>>();

            DisplayName = this.GetType().Name;
            TimeStamp = DateTime.UtcNow;
        }

        /// <summary>
        /// the message that produced the result represented by this class
        /// </summary>
        [JsonProperty]
        public HttpRequestQueueingActivityConfiguration CommandMessage { get; set; }

        /// <summary>
        /// the url that generated this result
        /// </summary>
        [JsonProperty]
        public String SourceUrl { get; set; }

        [JsonProperty]
        public List<Tuple<string, List<string>>> RequestHeaders { get; set; }

        [JsonProperty]
        public List<Tuple<string, List<string>>> ResponseHeaders { get; set; }

        [JsonProperty]
        public String HttpMethod { get; set; }

        [JsonProperty]
        public List<Tuple<string, string>> Payload {get; set; }

        [JsonProperty]
        public string Id {get; set; }

        [JsonProperty]
        public string Key {get; set; }

        [JsonProperty]
        public string DisplayName {get; set; }

        [JsonProperty]
        public DateTime TimeStamp {get; set; }

        [JsonProperty]
        public string ConfigurationJson {get; set; }

        [JsonProperty]
        public string ConfigurationJsonSchema {get; set; }

        [JsonProperty]
        object IPipelineToolConfiguration.Configuration {get; set; }


        [JsonProperty]
        public System.Net.HttpStatusCode ResponseStatusCode { get; set; }

        /// <summary>
        /// dear god don't do 
        ///         [JsonProperty] here
        /// because HttpResponseHeaders do not serialize without exception
        /// </summary>
        public System.Net.Http.Headers.HttpResponseHeaders HttpResponseHeaders { get; set; }

        [JsonProperty]
        public string ReasonPhrase { get;  set; }
    }
}
