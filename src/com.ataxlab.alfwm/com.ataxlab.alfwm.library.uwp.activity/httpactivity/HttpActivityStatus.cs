using com.ataxlab.alfwm.core.taxonomy.activity;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.library.activity.httpactivity
{
    public class HttpActivityStatus : ActivityStatus
    {
        public HttpActivityStatus() : base()
        {
        }

        public override string StatusJson { get; set; }
        public override string StatusJsonSchema { get; set; }
    }
}
