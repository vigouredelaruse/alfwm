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
using com.ataxlab.alfwm.uwp.mstests.QueueingPipelineTool;
using com.ataxlab.alfwm.core.taxonomy.deployment.queueing;

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
            var testPipelineVariable = new PipelineVariable(new TestDTO())
            {
                CreateDate = DateTime.UtcNow,
                Description = "variable description",
                DisplayName = "display name",
                Key = "test1",
                TimeStamp = DateTime.UtcNow,
                ID = Guid.NewGuid().ToString()
            };

            bool isMustResetBuilder = true;

            // exercise the process definition builder
            var testSerializedProcessDefinition = builder
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolClassName(typeof(HttpRequestQueueingActivity).GetType().AssemblyQualifiedName)
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolDisplayName("test http queueing request activity")
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolId(Guid.NewGuid().ToString())
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolPipelineVariable(testPipelineVariable)
                        .UsePipelineNodeBuilder.withToolChainSlotNumber(0)
                        .NextPipelineToolNode()
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolClassName(typeof(HtmlParserQueueingActivity).GetType().AssemblyQualifiedName)
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolDisplayName("test html parser queueing request activity")
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolId(Guid.NewGuid().ToString())
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolPipelineVariable(testPipelineVariable)
                        .UsePipelineNodeBuilder.withToolChainSlotNumber(1)
                        .NextPipelineToolNode()
                        .BuildProcessDefinitionEntitiy(isMustResetBuilder).ToXml();

            var incarnateProcessDefinition = testSerializedProcessDefinition.DeSerializeObject<DefaultQueueingPipelineProcessDefinitionEntity>();

            var testDeployment = new DefaultQueueingPipelineNodeDeploymentContainerBuilder();
            int i = 0;                             
        }
    }
}
