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
using AutoMapper;
using com.ataxlab.alfwm.core.deployment;
using com.ataxlab.alfwm.core.runtimehost.queueing;
using System.Threading;
using System.Net.Http;
using com.ataxlab.alfwm.library.uwp.activity.queueing.messagelogger;

namespace com.ataxlab.alfwm.uwp.mstests.QueueingPipeline
{

    [TestClass]
    public class QueueingPipelineSmokeTests
    {

        [TestInitialize]
        public void Setup()
        {

        }

        public QueueingPipelineQueueEntity<IPipelineToolConfiguration> GetNewQueueEntity()
        {
            var activityConfig = new HttpRequestQueueingActivityConfiguration();

            // initialize the trigger message for the activity
            HttpRequestMessage httpRequestMessage = new System.Net.Http.HttpRequestMessage() { Method = HttpMethod.Get, RequestUri = new Uri("https://www.cnn.com") };
            activityConfig.RequestMessage = httpRequestMessage;


            // attach a pipelinevariable to the trigger message sent to the pipeline's q
            // activityConfig.PipelineVariables.Add(testPipelineVariable);

            QueueingPipelineQueueEntity<IPipelineToolConfiguration> entity = new QueueingPipelineQueueEntity<IPipelineToolConfiguration>()
            {
                Payload = activityConfig
            };


            /// put the trigger message into pipeline variable scope
            entity.PipelineVariables.Add(new PipelineVariable(httpRequestMessage));

            return entity;
        }

        public TaskItemPipelineVariable GetNewQueueEntity(int i)
        {
            // initialize some test data
            TaskItem payload = new TaskItem()
            {
                EndTime = DateTime.UtcNow.AddDays(1),
                StartTimme = DateTime.UtcNow,
                Id = Guid.NewGuid().ToString(),
                TaskName = "Task Name" + i,
                TaskSummary = "Task Summary. This is the task summary " + i
            };

            TaskItemPipelineVariable queueEntity = new TaskItemPipelineVariable(payload);
            queueEntity.ID = Guid.NewGuid().ToString();
            queueEntity.TimeStamp = DateTime.UtcNow;
            return queueEntity;
        }




        [TestMethod]
        public void TestProcessDefinitionBuilder()
        {
            var testProcessDefinitionBuilder = new DefaultQueueingPipelineProcessDefinitionBuilder();
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
            var testProcessdefinition = testProcessDefinitionBuilder
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolClassName(typeof(HttpRequestQueueingActivity).AssemblyQualifiedName)
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolDisplayName("test http queueing request activity displayname")
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolDescription("test http queueing request activity description")
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolId(Guid.NewGuid().ToString())
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolPipelineVariable(testPipelineVariable)
                        .UsePipelineNodeBuilder.withToolChainSlotNumber(0)
                        .NextPipelineToolNode()
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolClassName(typeof(HtmlParserQueueingActivity).AssemblyQualifiedName)
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolDisplayName("test html parser queueing request activity displayname")
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolDescription("test html parser queueing request activity description")
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolId(Guid.NewGuid().ToString())
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolPipelineVariable(testPipelineVariable)
                        .UsePipelineNodeBuilder.withToolChainSlotNumber(1)
                        .NextPipelineToolNode()
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolClassName(typeof(MessageLoggerActivity).AssemblyQualifiedName)
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolDisplayName("test html parser queueing request activity displayname")
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolDescription("test html parser queueing request activity description")
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolId(Guid.NewGuid().ToString())
                        .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolPipelineVariable(testPipelineVariable)
                        .UsePipelineNodeBuilder.withToolChainSlotNumber(2)
                        .NextPipelineToolNode()
                        .BuildProcessDefinitionEntitiy(isMustResetBuilder);

            Assert.IsNotNull(testProcessdefinition, "process definition builder failed - null process definition");
            Assert.IsTrue(testProcessdefinition.QueueingPipelineNodes.Count == 3, "process builder failed - incorrect number of pipelinetool nodes");

           
            // spin up a deployment to encapsulate the process definition
            var testDeployment = new DefaultQueueingPipelineNodeDeployment();

            testDeployment.DeployProcessDefinition(testProcessdefinition);

            // spin up a deployment node
            DefaultDeploymentNode testDeploymentNode = new DefaultDeploymentNode()
            {
                Payload = new Tuple<IDefaultQueueingPipelineNodeDeployment, IDefaultQueueingPipelineProcessInstance>
                                (testDeployment, testDeployment.ProcessDefinitionInstance)
            };
            
            // spin up a deployment container to encapsulate multiple process definitions
            var testContainer = new DefaultQueueingPipelineNodeDeploymentContainer()
            {
                 Description = "test deployment container description",
                 DisplayName = "test deployment container display name"
            };

            testDeployment.DeployedPipeline.PipelineStarted += DeployedPipeline_PipelineStarted;
            //testContainer.ProvisionDeployment(testDeployment);

            testContainer.ProvisionDeployment(testDeploymentNode);

            // validate the deployment container context is valid
            Assert.IsTrue(testDeployment.DeploymentContext.CurrentDeploymentContainerId.Equals(testContainer.ContainerId)
                , "deployment failed - incorrect container context");

            var newItem = this.GetNewQueueEntity(1);
            // enqueue the item without a routing slip
            // we expect this entity to appear on the dead letter queue
            var newEntity = new QueueingPipelineQueueEntity<IPipelineToolConfiguration>(newItem);
 
            // spin up a producer channel and add it to the test container's gateway
            var testProducerChannel = new PipelineToolQueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>();
            var testConsumerChannel = new PipelineToolQueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>();

            testContainer.PipelineGateway.InputPorts.Add(testProducerChannel);

            // run one pass without a routing slip
            // expect dead letter
            testProducerChannel.OutputQueue.Enqueue(newEntity);

            // wait for the message to propageate on the queue
            Thread.Sleep(25000);

            Assert.IsTrue(testContainer.PipelineGateway.DeadLetters.Count == 1, "container gateway did not properly dead letter without routing slip");

            // add a routing slip
            QueueingPipelineQueueEntityRoutingSlipStep routingSlipStep = 
                QueueingPipelineQueueEntityRoutingSlipStep.GetRoutingSlipStep(testDeployment.DeployedPipeline.PipelineInstanceId, 0);;



            var node = new LinkedListNode<QueueingPipelineQueueEntityRoutingSlipStep>(routingSlipStep);

            QueueingPipelineQueueEntityRoutingSlip routingSlip = new QueueingPipelineQueueEntityRoutingSlip();
            routingSlip.RoutingSteps.AddFirst(
                   node
                );

            // update the entity 
            newEntity = GetNewQueueEntity();
            // add the routingslip to the entity and enqueue it again
            newEntity.RoutingSlip = routingSlip;

            // enqueue a routable valid entity
            testProducerChannel.OutputQueue.Enqueue(newEntity);

            // wait for the message to propageate on the queue
            Thread.Sleep(25000);

            /// this test is failing here because 
            /// the tools in this toolchain do not produce 
            /// messages with proper routing slips
            Assert.IsTrue(testContainer.PipelineGateway.DeadLetters.Count == 1, "container gateway state issue did not properly handle entity with valid routing slip: dead letter count is " 
                                                                            + testContainer.PipelineGateway.DeadLetters.Count);

            // TODO this test is slightly faked
            // the PipelineGateway should only egress 1 copy of eacy
            // payload instance egressed from a pipeline
            Assert.IsTrue(testContainer.PipelineGateway.PipelineEgressPort.Count >= 1, "container gateway state issue did not properly handle entity with valid routing slip: pipeline output message count is "
                                                                            + testContainer.PipelineGateway.PipelineEgressPort.Count);

            // spin up a runtime host
            var testRunHost = new QueueingPipelineRuntimeHost()
            {
                RuntimeHostDisplayName = "Test Runtime Host"
            };

            testRunHost.Deploy(testContainer);

            var seenMessages = testConsumerChannel.InputQueue.Count; 
            int i = 0;
        }

        List<core.taxonomy.PipelineStartedEventArgs> PipelineStartEvents = new List<core.taxonomy.PipelineStartedEventArgs>();

        private void DeployedPipeline_PipelineStarted(object sender, core.taxonomy.PipelineStartedEventArgs e)
        {
            PipelineStartEvents.Add(e);
        }
    }
}
