using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.deployment.model
{
    public class Deployment
    {
        public DeploymentConfiguration DeploymentConfiguration { get; set; }

        public DeploymentStatus DeploymentStatus { get; set; }
    }
}
