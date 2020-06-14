using System;
using System.Runtime.Serialization;

namespace com.ataxlab.alfwm.persistence.litedb.processdefinition.flowchart.grammar.verbs
{
    [Serializable]
    internal class LiteDbFlowchartDataSetProviderConfigurationException : Exception
    {
        public LiteDbFlowchartDataSetProviderConfigurationException()
        {
        }

        public LiteDbFlowchartDataSetProviderConfigurationException(string message) : base(message)
        {
        }

        public LiteDbFlowchartDataSetProviderConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LiteDbFlowchartDataSetProviderConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}