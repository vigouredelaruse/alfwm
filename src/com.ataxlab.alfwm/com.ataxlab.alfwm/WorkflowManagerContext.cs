using com.ataxlab.alfwm.deployment.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm
{
    public class WorkflowManagerContext
    {

        private List<Deployment> _deployments;

        public WorkflowManagerContext()
        {
            _deployments = new List<Deployment>();
        }

   
        internal List<Deployment> Deployments { get => _deployments; set => _deployments = value; }
    }
}
