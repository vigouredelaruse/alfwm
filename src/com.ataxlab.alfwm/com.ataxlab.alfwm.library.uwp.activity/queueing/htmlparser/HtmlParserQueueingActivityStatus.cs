using com.ataxlab.alfwm.core.taxonomy.activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.library.uwp.activity.queueing.htmlparser
{
    public class HtmlParserQueueingActivityStatus : ActivityStatus
    {
        public HtmlParserQueueingActivityStatus() : base()
        {

        }


        public override string StatusJson { get; set; }
        public override string StatusJsonSchema { get; set; }
    }
}
