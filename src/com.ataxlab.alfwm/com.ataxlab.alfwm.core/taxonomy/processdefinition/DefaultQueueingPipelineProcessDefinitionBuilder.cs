using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.processdefinition
{
    public class DefaultQueueingPipelineProcessDefinitionBuilder
    {
        private DefaultQueueingPipelineProcessDefinitionEntity processDefinition;
        public QueueingPipelineNodeBuilder UsePipelineNodeBuilder;

        public DefaultQueueingPipelineProcessDefinitionBuilder()
        {
            processDefinition = new DefaultQueueingPipelineProcessDefinitionEntity();

            UsePipelineNodeBuilder = new QueueingPipelineNodeBuilder(this);
        }


        public DefaultQueueingPipelineProcessDefinitionBuilder NextPipelineToolNode()
        {
            var node = UsePipelineNodeBuilder.Build();
            processDefinition.QueueingPipelineNodes.Add(node);

            // reset the builder
            UsePipelineNodeBuilder.Reset();
            return this;
        }

        public DefaultQueueingPipelineProcessDefinitionEntity Build(bool isMustResetBuilder)
        {
            var node = UsePipelineNodeBuilder.Build();
            processDefinition.QueueingPipelineNodes.Add(node);

            if(isMustResetBuilder)
            {
                // reset the builder
                UsePipelineNodeBuilder.Reset();
            }

            return processDefinition;
        }

        public class QueueingPipelineNodeBuilder
        {
            private DefaultQueueingPipelineProcessDefinitionBuilder _parentBuilder;

            private QueueingPipelineNodeEntity node;
            public readonly QueueingPipelineToolBuilder ToBuildPipelineTool;
            private QueueingPipelineToolEntity tool;
            private DefaultQueueingChannelPipelineToolGatewayContextEntity gateway;

            public QueueingPipelineNodeBuilder(DefaultQueueingPipelineProcessDefinitionBuilder parentBuilder)
            {
                _parentBuilder = parentBuilder;
                node = new QueueingPipelineNodeEntity();
                tool = new QueueingPipelineToolEntity();
                ToBuildPipelineTool = new QueueingPipelineToolBuilder(parentBuilder);
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
                    _parentBuilder.UsePipelineNodeBuilder.tool.DisplayName = displayName;
                    _parentBuilder.UsePipelineNodeBuilder.node.NodeType = QueueingPipelineNodeType.PipelineTool;
                    return _parentBuilder;
                }

                public DefaultQueueingPipelineProcessDefinitionBuilder withPipelineToolId(string id)
                {
                    _parentBuilder.UsePipelineNodeBuilder.tool.Id = id;

                    _parentBuilder.UsePipelineNodeBuilder.node.NodeType = QueueingPipelineNodeType.PipelineTool;
                    return _parentBuilder;
                }


                public DefaultQueueingPipelineProcessDefinitionBuilder withPipelineToolClassName(string classname)
                {
                    _parentBuilder.UsePipelineNodeBuilder.node.ClassName = classname;
                    _parentBuilder.UsePipelineNodeBuilder.tool.QueueingPipelineToolClassName = classname;

                    _parentBuilder.UsePipelineNodeBuilder.node.NodeType = QueueingPipelineNodeType.PipelineTool;
                    return _parentBuilder;
                }

                public DefaultQueueingPipelineProcessDefinitionBuilder withPipelineToolDescription(string description)
                {
                    _parentBuilder.UsePipelineNodeBuilder.tool.Description = description;

                    _parentBuilder.UsePipelineNodeBuilder.node.NodeType = QueueingPipelineNodeType.PipelineTool;
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
                    _parentBuilder.UsePipelineNodeBuilder.node.InstanceId = id;

                    _parentBuilder.UsePipelineNodeBuilder.node.NodeType = QueueingPipelineNodeType.PipelineTool;
                    return _parentBuilder;
                }

                public DefaultQueueingPipelineProcessDefinitionBuilder withPipelineToolPipelineVariable(PipelineVariable variables)
                {
                    _parentBuilder.UsePipelineNodeBuilder.tool.PipelineVariables.Add(variables);

                    _parentBuilder.UsePipelineNodeBuilder.node.NodeType = QueueingPipelineNodeType.PipelineTool;
                    return _parentBuilder;
                }
            }

        }



    }

}
