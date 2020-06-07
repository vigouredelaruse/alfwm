
using Newtonsoft.Json.Linq;

namespace com.ataxlab.alfwm.core.taxonomy.binding
{
    public interface IPipelineToolBinding
    {
        string PipelineToolBindingDisplayName { get; set; }
        string PipelineToolBindingKey { get; set; }
        PipelineVariableDictionary PipelineToolBindingValue { get; set; }

    }

    /// <summary>
    /// implementation needs to arrange for 
    /// parsing the JObject graph 
    /// extracting the properties and their values
    /// and population of 
    /// 
    /// IPipelineToolBinding.Value &&
    /// IPipelineToolBinding.Key
    /// </summary>
    /// <typeparam name="TDTO"></typeparam>
    public interface IPipelineToolBinding<TDTO> : IPipelineToolBinding where TDTO : JObject
    {

    }

}