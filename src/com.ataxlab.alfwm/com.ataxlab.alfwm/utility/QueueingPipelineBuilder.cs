using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.utility
{
    /// <summary>
    /// see https://www.scottlilly.com/fluent-interface-creator
    /// </summary>
    /// <typeparam name="TPipeline"></typeparam>
    /// <typeparam name="TProcessDefinition"></typeparam>
    public class QueueingPipelineBuilder<TPipeline, TProcessDefinition>
        where TPipeline : class, IPipeline<TProcessDefinition>, new()
        where TProcessDefinition : class, new() 
    {
        private QueueingPipelineBuilder<TPipeline, TProcessDefinition> _queueingPipelineBuilder = new QueueingPipelineBuilder<TPipeline, TProcessDefinition>();

        public QueueingPipelineBuilder<TPipeline, TProcessDefinition> Build() => _queueingPipelineBuilder;

        // public QueueingPipelineBuilder 
    }

}
