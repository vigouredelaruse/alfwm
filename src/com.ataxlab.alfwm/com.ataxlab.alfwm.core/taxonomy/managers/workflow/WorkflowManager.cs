using com.ataxlab.alfwm.core.scheduler;
using com.ataxlab.alfwm.core.taxonomy.workflow;
using com.ataxlab.alfwm.core.deployment.model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.core.taxonomy.managers.workflow
{
    /// <summary>
    /// 
    /// </summary>

    public class WorkflowManager : IWorkflowManager
    {
        private IScheduler _scheduler;
        private WorkflowManagerContext _workflowManagerContext;

        public WorkflowManager()
        {
            _workflowManagerContext = new WorkflowManagerContext();
        }

        public WorkflowManager(IScheduler scheduler)
        {
            Scheduler = scheduler;
            _workflowManagerContext = new WorkflowManagerContext();
        }



        public IScheduler Scheduler { get => _scheduler; set => _scheduler = value; }
        public WorkflowManagerContext WorkflowManagerContext { get => _workflowManagerContext; set => _workflowManagerContext = value; }

        public Task<DeploymentStatus> DeleteWorkflowByWorkflowId(string workflowId)
        {
            throw new NotImplementedException();
        }

        public Task<DeploymentStatus> DeployWorkflow(WorkflowConfiguration workflow)
        {
            throw new NotImplementedException();
        }

        public Task<WorkflowStatus> GetWorkflowStatusByWorkflowId(string workflowId)
        {
            throw new NotImplementedException();
        }

        public Task ShutDown()
        {
            throw new NotImplementedException();
        }

        public Task Start()
        {
            throw new NotImplementedException();
        }

        public Task<DeploymentStatus> SuspendWorkflowByWorkflowId(string workflowId)
        {
            throw new NotImplementedException();
        }
    }
}
