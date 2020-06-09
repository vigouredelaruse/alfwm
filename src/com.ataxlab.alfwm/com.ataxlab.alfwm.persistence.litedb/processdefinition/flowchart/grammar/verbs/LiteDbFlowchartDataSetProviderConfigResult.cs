using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.persistence.litedb.processdefinition.flowchart.grammar.verbs
{
    public enum LiteDbFlowchartDataSetProviderConfigResultType
    {
        Exception_Invalid_Connection_String,
        Success
    }

    public class LiteDbFlowchartDataSetProviderConfigResult : ILiteDbFlowchartDataSetProviderConfigResult
    {
        public LiteDbFlowchartDataSetProviderConfigResult()
        {

        }

        /// <summary>
        /// hopefully empty
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// hopefully Success
        /// </summary>
        public LiteDbFlowchartDataSetProviderConfigResultType ConfigurationResult { get; set; }
    }
}
