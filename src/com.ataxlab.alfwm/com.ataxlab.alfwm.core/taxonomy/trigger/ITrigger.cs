using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.trigger
{
    public interface ITrigger
    {
        V Configure<T,V>(T parameter);
    }
}
