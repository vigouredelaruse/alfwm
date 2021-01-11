using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.processdefinition
{
    public class DefaultQueueingPipelineProcessDefinitionBuilder
    {
        private DefaultQueueingPipelineProcessDefinitionEntity processDefinition;

        public DefaultQueueingPipelineProcessDefinitionBuilder()
        {
            processDefinition = new DefaultQueueingPipelineProcessDefinitionEntity();
        }
    }

    public class QueueingPipelineNodeBuilder
    {
        private QueueingPipelineNodeEntity node;
        public readonly QueueingPipelineToolBuilder buildPipelineTool;
        private QueueingPipelineToolEntity tool;

        public QueueingPipelineNodeBuilder()
        {
            node = new QueueingPipelineNodeEntity();
            tool = new QueueingPipelineToolEntity();
            buildPipelineTool = new QueueingPipelineToolBuilder(this);
        }




        public QueueingPipelineNodeBuilder withToolChainSlotNumber(int slot)
        {
            node.ToolChainSlotNumber = slot;
            return this;
        }


        public QueueingPipelineNodeEntity Build()
        {
            // add the nested classes
            node.QueueingPipelineTool = tool;
            return node;
        }


        public class QueueingPipelineToolBuilder
        {
            QueueingPipelineNodeBuilder _parentBuilder;

            public QueueingPipelineToolBuilder(QueueingPipelineNodeBuilder parentBuilder)
            {
                _parentBuilder = parentBuilder;

            }

            public QueueingPipelineNodeBuilder withPipelineToolDisplayName(string displayName)
            {
                _parentBuilder.tool.DisplayName = displayName;

                return _parentBuilder;
            }

            public QueueingPipelineNodeBuilder withPipelineToolId(string id)
            {
                _parentBuilder.tool.Id = id;
                return _parentBuilder;
            }


            public QueueingPipelineNodeBuilder withPipelineToolClassName(string classname)
            {
                _parentBuilder.node.ClassName = classname;
                _parentBuilder.tool.QueueingPipelineToolClassName = classname;
                return _parentBuilder;
            }


            public QueueingPipelineNodeBuilder withPipelineToolInstanceId(string id)
            {
                _parentBuilder.node.InstanceId = id;
                return _parentBuilder;
            }

        }

    }


}
