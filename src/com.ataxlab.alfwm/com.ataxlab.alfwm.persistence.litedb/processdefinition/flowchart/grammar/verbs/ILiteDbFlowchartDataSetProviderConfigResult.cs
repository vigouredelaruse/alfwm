using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.persistence.litedb.processdefinition.flowchart.grammar.verbs
{
    public interface ILiteDbFlowchartDataSetProviderConfigResult
    {
        string ExceptionMessage { get; set; }
        LiteDbFlowchartDataSetProviderConfigResultType ConfigurationResult { get; set; }
    }
}
