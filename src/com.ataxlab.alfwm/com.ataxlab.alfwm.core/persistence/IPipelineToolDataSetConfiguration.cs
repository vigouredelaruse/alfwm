﻿using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.persistence
{
    /// <summary>
    /// Pipeline Tools Need Access To Data 
    /// This Provides A Typed Way To Get One
    /// </summary>
    public interface IPipelineToolDataSetConfiguration
    {

        string DisplayName { get; set; }

        DateTime DeploymentTime { get; set; }

        string ConfigurationJson { get; set; }

        /// <summary>
        /// the expectation is that the implemenation layer of the workflow service
        /// is decoupled from the clients of public interface of the workflow service
        /// 
        /// the implementation layer can identify the specifics that can be easily 
        /// changed with a schema update
        /// </summary>
        string ConfigurationJsonSchema { get; set; }
    }
}
