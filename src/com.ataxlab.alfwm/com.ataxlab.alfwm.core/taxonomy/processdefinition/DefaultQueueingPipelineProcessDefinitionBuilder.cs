using AutoMapper;
using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.core.alfwm.utility.extension;
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
            var node = UsePipelineNodeBuilder.BuildPipelineNodeEntity();
            processDefinition.QueueingPipelineNodes.Add(node);

            // reset the builder
            UsePipelineNodeBuilder.Reset();
            return this;
        }

        public DefaultQueueingPipelineProcessDefinitionEntity BuildProcessDefinitionEntitiy(bool isMustResetBuilder)
        {
            var node = UsePipelineNodeBuilder.BuildPipelineNodeEntity();
            processDefinition.QueueingPipelineNodes.Add(node);

            if(isMustResetBuilder)
            {
                // reset the builder
                UsePipelineNodeBuilder.Reset();
            }

            return processDefinition;
        }

        public DefaultQueueingPipelineProcessInstance BuildProcessDefinition(bool isMustResetBuilder)
        {

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DefaultQueueingPipelineProcessDefinitionEntity,
                    DefaultQueueingPipelineProcessInstance>().IgnoreAllNonExisting();
            });

            // only during development, validate your mappings; remove it before release
            configuration.AssertConfigurationIsValid();
            // use DI (http://docs.automapper.org/en/latest/Dependency-injection.html) or create the mapper yourself
            var mapper = configuration.CreateMapper();


            var retVal = new DefaultQueueingPipelineProcessInstance();
            var currentNode = UsePipelineNodeBuilder.BuildPipelineNodeEntity();
            processDefinition.QueueingPipelineNodes.Add(currentNode);
            
            retVal = mapper.Map<DefaultQueueingPipelineProcessInstance>(processDefinition);

            if (isMustResetBuilder)
            {
                // reset the builder
                UsePipelineNodeBuilder.Reset();
            }

            return retVal;
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


            public QueueingPipelineNodeEntity BuildPipelineNodeEntity()
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
                    _parentBuilder.UsePipelineNodeBuilder.tool.PipelineToolDisplayName = displayName;
                    _parentBuilder.UsePipelineNodeBuilder.node.NodeType = QueueingPipelineNodeType.PipelineTool;
                    return _parentBuilder;
                }

                public DefaultQueueingPipelineProcessDefinitionBuilder withPipelineToolId(string id)
                {
                    _parentBuilder.UsePipelineNodeBuilder.tool.PipelineToolId = id;

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
                    _parentBuilder.UsePipelineNodeBuilder.tool.PipelineToolDescription = description;

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
                    _parentBuilder.UsePipelineNodeBuilder.tool.PipelineToolVariables.Add(variables);

                    _parentBuilder.UsePipelineNodeBuilder.node.NodeType = QueueingPipelineNodeType.PipelineTool;
                    return _parentBuilder;
                }
            }

        }



    }

}
