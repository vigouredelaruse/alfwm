namespace com.ataxlab.alfwm.core.deployment.model
{
    public interface IDeployment<TProcessInstance>
    {
        TProcessInstance ProcessDefinitionInstance { get; set; }
        string DeploymentId { get; set; }
        string InstanceId { get; set; }
    }
}