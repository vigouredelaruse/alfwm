using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Web.Http;

namespace com.ataxlab.alfwm.library.activity.httpactivity
{

    public class HttpActivityConfiguration : IPipelineToolConfiguration
    {
        public string HttpUrl { get; set; }
        public Dictionary<string, string> HttpHeaders { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public string AuthorizationToken { get; set; }
        public string DisplayName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ConfigurationJson { get; set; }
        public string ConfigurationJsonSchema { get; set; }
        public string Id { get; set; }
        public string Key { get; set; }
        public object Configuration { get; set; }
    }

}
