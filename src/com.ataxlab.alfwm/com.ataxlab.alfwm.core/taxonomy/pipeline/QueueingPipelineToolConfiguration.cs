﻿using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public class QueueingPipelineToolConfiguration : IPipelineToolConfiguration
    {
        public QueueingPipelineToolConfiguration()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PipelineVariables = new List<PipelineVariable>();

        }
        public string Id { get; set;}
        public string Key { get; set; }
        public string DisplayName { get; set; }
        public object Configuration { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ConfigurationJson { get; set; }
        public string ConfigurationJsonSchema { get; set; }
        public List<PipelineVariable> PipelineVariables { get; set;}
    }
}
