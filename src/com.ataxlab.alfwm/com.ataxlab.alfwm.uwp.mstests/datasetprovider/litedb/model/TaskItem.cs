using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.uwp.mstests.datasetprovider.litedb.model
{
    /// <summary>
    /// sample POCO for our provider test
    /// </summary>
    public class TaskItem : ICloneable
    {
        public TaskItem() 
        {
            this.AssignedResources = new List<Contact>();
        }

        public string Id { get; set; }

        public string TaskName { get; set; }

        public string TaskSummary { get; set; }

        public List<Contact> AssignedResources { get; set; }

        public DateTime StartTimme { get; set; }

        public DateTime EndTime { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
