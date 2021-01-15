using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.runtimehost.queueing
{
    public interface IDefaultQueueingPipelineRuntimeHostContext : IRuntimeHostContext
    {

    }

    public class DefaultQueueingPipelineRuntimeHostContext : IDefaultQueueingPipelineRuntimeHostContext
    {
        public DefaultQueueingPipelineRuntimeHostContext()
        {

        }

        public DateTime HostStartedAt {get; set; }
        public string RuntimeHostAddress {get; set; }
    }

    public interface IDefaultQueueingPipelineRuntimeHost : IRuntimeHost
    {

    }

    public class QueueingPipelineRuntimeHost : IDefaultQueueingPipelineRuntimeHost
    {
        public QueueingPipelineRuntimeHost()
        {
            Context = new DefaultQueueingPipelineRuntimeHostContext();
        }

        public string RuntimeHostId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string RuntimeHostDisplayName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IRuntimeHostContext Context { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
