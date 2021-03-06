﻿using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.workflow
{
    /// <summary>
    /// designed with ease of implementation replacement
    /// by the implementer
    /// </summary>
    public abstract class WorkflowConfiguration : IPipelineToolConfiguration
    {
        public WorkflowConfiguration()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PipelineVariables = new List<PipelineVariable>();
        }

        public virtual string DisplayName { get; set; }
        public virtual DateTime TimeStamp { get; set; }
        public virtual string ConfigurationJson { get; set; }
        public virtual string ConfigurationJsonSchema { get; set; }
        public string Id { get; set; }
        public string Key { get; set; }
        public object Configuration { get; set; }
        public abstract List<PipelineVariable> PipelineVariables { get; set; }
    }
}
