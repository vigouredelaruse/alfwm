namespace com.ataxlab.alfwm.core.deployment.model
{
    public interface IDeployment<TProcessDefinition>
    {
        TProcessDefinition ProcessDefinition { get; set; }
        string DeploymentId { get; set; }
        string InstanceId { get; set; }
    }
}