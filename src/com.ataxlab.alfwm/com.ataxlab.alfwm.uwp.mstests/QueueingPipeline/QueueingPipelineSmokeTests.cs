using com.ataxlab.alfwm.core.taxonomy.processdefinition;
using com.ataxlab.core.alfwm.utility.extension;
using com.ataxlab.alfwm.core.taxonomy.pipeline.queueing;
using com.ataxlab.alfwm.core.taxonomy.binding.queue.routing;
using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.core.taxonomy.processdefinition;
using com.ataxlab.alfwm.library.uwp.activity.queueing.htmlparser;
using com.ataxlab.alfwm.library.uwp.activity.queueing.httprequest;
using com.ataxlab.alfwm.uwp.mstests.datasetprovider.litedb.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.uwp.mstests.QueueingPipeline
{

    [TestClass]
    public class QueueingPipelineSmokeTests
    {

        [TestInitialize]
        public void Setup()
        {

        }

        [TestMethod]
        public void TestProcessDefinitionBuilder()
        {
            var builder = new DefaultQueueingPipelineProcessDefinitionBuilder();

            var testSerializedProcessDefinition = builder
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolClassName(typeof(HttpRequestQueueingActivity).GetType().AssemblyQualifiedName)
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolDisplayName("test http queueing request activity");

            int i = 0;                             
        }
    }
}
