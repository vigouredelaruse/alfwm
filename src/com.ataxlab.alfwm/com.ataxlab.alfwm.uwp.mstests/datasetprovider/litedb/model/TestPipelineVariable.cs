using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.uwp.mstests.datasetprovider.litedb.model
{

    public class TestPipelineVariable : PipelineVariable<TaskItem, List<TaskItem>, TestPipelineVariable>, ICloneable
    {
        public TestPipelineVariable()
        {

        }

        /// <summary>
        /// test entity
        /// concrete implementation of IPipelineVariable<>
        /// </summary>
        /// <param name="payload"></param>
        public TestPipelineVariable(TaskItem payload) : base(payload)
        {
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

}
