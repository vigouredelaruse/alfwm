using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.runtimehost
{

    /// <summary>
    /// specifies the base interface for runtimehosts
    /// </summary>
    public interface IRuntimeHostContext
    {
        DateTime HostStartedAt { get; set; }
        /// <summary>
        /// support a way to send pipelines extant
        /// in other runtimehosts
        /// </summary>
        string RuntimeHostAddress { get; set; }
    }

    /// <summary>
    /// specify the base interface for 
    /// implementations that need a runtimehost
    /// as opposed to a scheduler, that exists in
    /// a different level of abstraction within 
    /// the model used here
    /// </summary>
    public interface IRuntimeHost
    {
        String RuntimeHostId { get; set; }

        String RuntimeHostDisplayName { get; set; }

        IRuntimeHostContext Context { get; set; }
        string RuntimeHostInstanceId { get; }
    }


}
