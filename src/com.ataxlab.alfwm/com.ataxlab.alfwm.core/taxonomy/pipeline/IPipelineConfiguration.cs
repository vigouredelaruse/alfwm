using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    /// <summary>
    /// optimistically assuming IPipelineToolConfiguration is a suitable
    /// archetype for IPipelineConfiguration
    /// </summary>
    [Obsolete]
    public interface IPipelineConfiguration : IPipelineToolConfiguration
    {
    }
}
