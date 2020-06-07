using com.ataxlab.alfwm.core.taxonomy.activity;
using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.scheduler.windowsthreadpool.taxonomy.activity
{
    public class HttpActivity : Activity
    {
        public override string InstanceId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IPipelineToolStatus Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IPipelineToolContext Context { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IPipelineToolConfiguration Configuration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IPipelineToolBinding OutputBinding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override Task<StartResult> Start<StartResult, StartConfiguration>(StartConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public override StopResult Stop<StopResult>(string instanceId)
        {
            throw new NotImplementedException();
        }
    }
}
