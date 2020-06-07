using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public interface IPipelineProgressUpdatedStatus : IPipelineToolStatus
    {
        IPipelineBinding OutputBinding { get; set; }
    }
}
