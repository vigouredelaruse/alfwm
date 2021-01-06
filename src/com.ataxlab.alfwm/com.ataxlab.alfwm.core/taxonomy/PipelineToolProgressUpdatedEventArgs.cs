using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.ObjectModel;

namespace com.ataxlab.alfwm.core.taxonomy
{
    public class PipelineToolProgressUpdatedEventArgs : EventArgs
    {
        public PipelineToolProgressUpdatedEventArgs()
        {
            TimeStamp = DateTime.UtcNow;
        }
        public IPipelineToolStatus Status { get; set; }
        public ObservableCollection<IPipelineVariable> OutputVariables { get; set; }
        public string InstanceId { get; set; }
        public DateTime TimeStamp { get; private set; }
    }
}