﻿using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.uwp.mstests.datasetprovider.litedb.model
{

    public class TaskItemPipelineVariable : PipelineVariable<TaskItem, List<TaskItem>, TaskItemPipelineVariable>, ICloneable
    {
        public TaskItemPipelineVariable()
        {

        }

        /// <summary>
        /// test entity
        /// concrete implementation of IPipelineVariable<>
        /// </summary>
        /// <param name="payload"></param>
        public TaskItemPipelineVariable(TaskItem payload) : base(payload)
        {
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

}