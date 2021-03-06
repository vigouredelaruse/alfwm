﻿using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.pipeline.queueing;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public interface IDefaultQueueingPipelineProcessInstance
    {
        [XmlAttribute]
        string Id { get; set; }

        [XmlElement]

        LinkedList<DefaultQueueingPipelineToolNode> QueueingPipelineNodes { get; set; }


    }

 
}
