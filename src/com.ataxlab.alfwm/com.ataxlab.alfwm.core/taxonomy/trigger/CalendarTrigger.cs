using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.trigger
{
    /// <summary>
    /// designed with maximum implementer control im mind
    /// </summary>
    public abstract class CalendarTrigger : ITrigger
    {
        public abstract ConfigurationResult Configure<ConfigurationParameter, ConfigurationResult>(ConfigurationParameter parameter);
    }
}
