﻿using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.trigger
{
    public abstract class CronTrigger : ITrigger
    {
        public abstract ConfigurationResult Configure<ConfigurationParameter, ConfigurationResult>(ConfigurationParameter parameter);

    }
}
