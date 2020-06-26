using com.ataxlab.alfwm.core.scheduler;
using com.ataxlab.alfwm.core.taxonomy.workflow;
using com.ataxlab.alfwm.deployment.model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm
{
    /// <summary>
    /// the goal is for the workflow manager to abstract away the details 
    /// associated with what happens when you start it, shut it down and
    /// deploy workflow configurations
    /// 
    /// its overriding purpose is to provide you with a reference you 
    /// will likely not allow to go out of scope, for there be background threads
    /// 
    /// obviously the Activities need to communicate back to the application
    /// but how this occurs is not 'visible' on the Workflow Manager's external 
    /// interace
    /// </summary>
    [Obsolete]
    public interface IWorkflowManager
    {
        IScheduler Scheduler { get; set; }

        [Obsolete]
        WorkflowManagerContext WorkflowManagerContext { get; set; }

        Task Start();
        Task ShutDown();

        /// <summary>
        /// the expectation is that the client will maintain (external to this library)
        /// a repository of deployed workflow configurations, 
        /// keep their states in sync with what's reported 
        /// by the workflow manager, and of course, correlate
        /// the different kinds of workflow configuration with the required 
        /// user interfaces if any
        /// </summary>
        /// <param name="workflow"></param>
        /// <returns></returns>
        [Obsolete]
        Task<DeploymentStatus> DeployWorkflow(WorkflowConfiguration workflow);

        [Obsolete]
        Task<DeploymentStatus> SuspendWorkflowByWorkflowId(string workflowId);

        [Obsolete]
        Task<DeploymentStatus> DeleteWorkflowByWorkflowId(string workflowId);

        [Obsolete]
        Task<WorkflowStatus> GetWorkflowStatusByWorkflowId(string workflowId);
    }
}
