using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.uwp.mstests.datasetprovider.litedb.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.uwp.mstests.QueueingPipelineTool
{
    [TestClass]
    public class QueueingPipelineToolSmokeTest
    {

        [TestInitialize]
        public void Setup()
        {

        }

        /// <summary>
        /// initialize a queueing pipeline tool
        /// post some messages to its input
        /// receive some processed messages on its output
        /// </summary>
        [TestMethod]
        public void CanPassThroughToOutputChannels()
        {
            QueueingPipelineTool<TaskItemPipelineVariable> toolA = new QueueingPipelineTool<TaskItemPipelineVariable>();

            QueueingPipelineTool<TaskItemPipelineVariable> toolB = new QueueingPipelineTool<TaskItemPipelineVariable>();
        }
    }
}
