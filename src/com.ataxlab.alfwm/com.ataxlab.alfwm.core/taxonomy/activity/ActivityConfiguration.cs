using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.activity
{
    public abstract class ActivityConfiguration : IPipelineToolConfiguration
    {
        /// <summary>
        /// designed with ease of implementation replacement
        /// by the implementer
        /// </summary>
        public ActivityConfiguration()
        { }

        public virtual string DisplayName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public virtual DateTime DeploymentTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public virtual string ConfigurationJson { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public virtual string ConfigurationJsonSchema { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
