using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.ataxlab.alfwm.core.persistence
{
    public interface IPipelineToolDataSet<T> where T : new()
    {
        IQueryable<T> Items { get; set; }
    }
}
