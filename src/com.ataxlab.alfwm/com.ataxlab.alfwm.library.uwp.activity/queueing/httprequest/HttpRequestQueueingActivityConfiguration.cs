﻿using com.ataxlab.alfwm.core.taxonomy.binding;
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
    public class HttpRequestQueueingActivityConfiguration : IPipelineToolConfiguration
    {
        public HttpRequestHeaders RequestHeaders { get; set; }

        public HttpRequestMessage RequestMessage { get; set; }
        public string Id { get; set;}
        public string Key { get; set;}
        public string DisplayName { get; set;}
        public object Configuration { get; set;}
        public DateTime TimeStamp { get; set;}
        public string ConfigurationJson { get; set;}
        public string ConfigurationJsonSchema { get; set;}
        public List<PipelineVariable> PipelineVariables { get; set;}

        public HttpRequestQueueingActivityConfiguration()
        {
            this.Id = Guid.NewGuid().ToString();
            this.TimeStamp = DateTime.UtcNow;
            DisplayName = this.GetType().Name;
            this.PipelineVariables = new List<PipelineVariable>();
        }
    }
}
