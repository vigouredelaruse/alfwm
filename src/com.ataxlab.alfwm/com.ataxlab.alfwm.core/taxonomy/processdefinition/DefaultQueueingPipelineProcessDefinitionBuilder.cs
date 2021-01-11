using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.processdefinition
{
    public class DefaultQueueingPipelineProcessDefinitionBuilder
    {
        private DefaultQueueingPipelineProcessDefinitionEntity processDefinition;
        public QueueingPipelineNodeBuilder pipelineNodeBuilder;

        public DefaultQueueingPipelineProcessDefinitionBuilder()
        {
            processDefinition = new DefaultQueueingPipelineProcessDefinitionEntity();

            pipelineNodeBuilder = new QueueingPipelineNodeBuilder(this);
        }


        public DefaultQueueingPipelineProcessDefinitionBuilder NextPipelineToolNode()
        {
            var node = pipelineNodeBuilder.Build();
            processDefinition.QueueingPipelineNodes.Add(node);

            // reset the builder
            pipelineNodeBuilder.Reset();
            return this;
        }

        public DefaultQueueingPipelineProcessDefinitionEntity Build(bool isMustResetBuilder)
        {
            var node = pipelineNodeBuilder.Build();
            processDefinition.QueueingPipelineNodes.Add(node);

            if(isMustResetBuilder)
            {
                // reset the builder
                pipelineNodeBuilder.Reset();
            }

            return processDefinition;
        }

        public class QueueingPipelineNodeBuilder
        {
            private DefaultQueueingPipelineProcessDefinitionBuilder _parentBuilder;

            private QueueingPipelineNodeEntity node;
            public readonly QueueingPipelineToolBuilder buildPipelineTool;
            private QueueingPipelineToolEntity tool;
            private DefaultQueueingChannelPipelineToolGatewayContextEntity gateway;

            public QueueingPipelineNodeBuilder(DefaultQueueingPipelineProcessDefinitionBuilder parentBuilder)
            {
                _parentBuilder = parentBuilder;
                node = new QueueingPipelineNodeEntity();
                tool = new QueueingPipelineToolEntity();
                buildPipelineTool = new QueueingPipelineToolBuilder(parentBuilder);
            }




            public DefaultQueueingPipelineProcessDefinitionBuilder withToolChainSlotNumber(int slot)
            {
                node.ToolChainSlotNumber = slot;
                return _parentBuilder;
            }


            public QueueingPipelineNodeEntity Build()
            {
                // add the nested classes
                node.QueueingPipelineTool = tool;
                return node;
            }

            internal void Reset()
            {
                node = new QueueingPipelineNodeEntity();
                tool = new QueueingPipelineToolEntity();
            }

            public class QueueingPipelineToolBuilder
            {
                DefaultQueueingPipelineProcessDefinitionBuilder _parentBuilder;

                public QueueingPipelineToolBuilder(DefaultQueueingPipelineProcessDefinitionBuilder parentBuilder)
                {
                    _parentBuilder = parentBuilder;


                }

                public DefaultQueueingPipelineProcessDefinitionBuilder withPipelineToolDisplayName(string displayName)
                {
                    _parentBuilder.pipelineNodeBuilder.tool.DisplayName = displayName;
                    _parentBuilder.pipelineNodeBuilder.node.NodeType = QueueingPipelineNodeType.PipelineTool;
                    return _parentBuilder;
                }

                public DefaultQueueingPipelineProcessDefinitionBuilder withPipelineToolId(string id)
                {
                    _parentBuilder.pipelineNodeBuilder.tool.Id = id;

                    _parentBuilder.pipelineNodeBuilder.node.NodeType = QueueingPipelineNodeType.PipelineTool;
                    return _parentBuilder;
                }


                public DefaultQueueingPipelineProcessDefinitionBuilder withPipelineToolClassName(string classname)
                {
                    _parentBuilder.pipelineNodeBuilder.node.ClassName = classname;
                    _parentBuilder.pipelineNodeBuilder.tool.QueueingPipelineToolClassName = classname;

                    _parentBuilder.pipelineNodeBuilder.node.NodeType = QueueingPipelineNodeType.PipelineTool;
                    return _parentBuilder;
                }

                public DefaultQueueingPipelineProcessDefinitionBuilder withPipelineToolDescription(string description)
                {
                    _parentBuilder.pipelineNodeBuilder.tool.Description = description;

                    _parentBuilder.pipelineNodeBuilder.node.NodeType = QueueingPipelineNodeType.PipelineTool;
                    return _parentBuilder;
                }

                /// <summary>
                /// the tool should create new instance id's 
                /// the id might be thought of as less ephemeral
                /// useful for primary keys for example
                /// </summary>
                /// <param name="id"></param>
                /// <returns></returns>
                private DefaultQueueingPipelineProcessDefinitionBuilder withPipelineToolInstanceId(string id)
                {
                    _parentBuilder.pipelineNodeBuilder.node.InstanceId = id;

                    _parentBuilder.pipelineNodeBuilder.node.NodeType = QueueingPipelineNodeType.PipelineTool;
                    return _parentBuilder;
                }

                public DefaultQueueingPipelineProcessDefinitionBuilder withPipelineToolPipelineVariable(PipelineVariable variables)
                {
                    _parentBuilder.pipelineNodeBuilder.tool.PipelineVariables.Add(variables);

                    _parentBuilder.pipelineNodeBuilder.node.NodeType = QueueingPipelineNodeType.PipelineTool;
                    return _parentBuilder;
                }
            }

        }



    }

}
