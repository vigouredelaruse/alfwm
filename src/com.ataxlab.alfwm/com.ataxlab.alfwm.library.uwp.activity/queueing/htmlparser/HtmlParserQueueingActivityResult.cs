using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.library.uwp.activity.queueing.htmlparser
{
    public class HtmlParserQueueingActivityResult : IPipelineToolConfiguration<HtmlDocument>, IPipelineToolConfiguration
    {

        public HtmlParserQueueingActivityResult()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PipelineVariables = new List<PipelineVariable>();
            this.TimeStamp = DateTime.UtcNow;
        }

        public HtmlDocument Payload { get; set; }
        public string Id { get; set; }
        public string Key { get; set; }
        public string DisplayName { get; set; }
        public object Configuration { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ConfigurationJson { get; set; }
        public string ConfigurationJsonSchema { get; set; }
        public List<PipelineVariable> PipelineVariables { get; set;}
    }
}
