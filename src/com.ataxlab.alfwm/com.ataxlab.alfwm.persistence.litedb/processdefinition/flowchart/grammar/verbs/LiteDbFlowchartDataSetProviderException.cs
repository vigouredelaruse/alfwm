using System;
using System.Runtime.Serialization;

namespace com.ataxlab.alfwm.persistence.litedb.processdefinition.flowchart.grammar.verbs
{
    [Serializable]
    internal class LiteDbFlowchartDataSetProviderException : Exception
    {
        public LiteDbFlowchartDataSetProviderException()
        {
        }

        public LiteDbFlowchartDataSetProviderException(string message) : base(message)
        {
        }

        public LiteDbFlowchartDataSetProviderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LiteDbFlowchartDataSetProviderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}