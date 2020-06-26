namespace com.ataxlab.alfwm.deployment.model
{
    public interface IDeployment<TProcessDefinition, TDeploymentStatus>
        where TProcessDefinition : class
        where TDeploymentStatus : class
    {
        TProcessDefinition ProcessDefinition { get; set; }
        TDeploymentStatus DeploymentStatus { get; set; }
        string DeploymentId { get; set; }
        string InstanceId { get; set; }
    }
}