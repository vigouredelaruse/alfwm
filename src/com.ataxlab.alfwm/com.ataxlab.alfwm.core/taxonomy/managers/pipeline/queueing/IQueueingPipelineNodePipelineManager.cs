using com.ataxlab.alfwm.core.deployment;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.core.taxonomy.pipeline.queueing;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.managers.pipeline.queueing
{
    /// <summary>
    /// furnishing a pipeline manager that IS a deployment container
    /// </summary>
    public interface IDefaultQueueingPipelineNodePipelineManager : 
        IDeploymentContainer<IDefaultQueueingPipeline, IDefaultQueueingPipelineProcessInstance>
    {
        Dictionary<string, IDefaultQueueingPipeline> Pipelines { get; set; }
    }
}
