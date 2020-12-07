using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.activity
{
    public class ActivityConfiguration
    {
        /// <summary>
        /// designed with ease of implementation replacement
        /// by the implementer
        /// </summary>
        public ActivityConfiguration()
        { }

        public virtual string DisplayName { get; set; }
        public virtual DateTime DeploymentTime { get; set; }
        public virtual string ConfigurationJson { get; set; }
        public virtual string ConfigurationJsonSchema { get; set; }
    }
}
