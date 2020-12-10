using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.library.uwp.activity.queueing.httprequest
{
    public class HttpRequestQueueingActivityConfiguration 
    {
        public HttpRequestHeaders RequestHeaders { get; set; }

        public HttpRequestMessage RequestMessage { get; set; }
   

        public HttpRequestQueueingActivityConfiguration()
        {
                       
        }
    }
}
