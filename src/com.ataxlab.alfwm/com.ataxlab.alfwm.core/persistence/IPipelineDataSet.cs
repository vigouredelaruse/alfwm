using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.ataxlab.alfwm.core.persistence
{
    /// <summary>
    /// an abstraction of entitis that are made available to a pipeline
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPipelineDataSet<T> where T : new()
    {
        IQueryable<T> Items { get; set; }
    }
}
