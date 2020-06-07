using System;
using System.Runtime.Serialization;

namespace com.ataxlab.alfwm.scheduler.windowsthreadpool
{
    [Serializable]
    internal class WindowsThreadpoolSchedulerException : Exception
    {
        public WindowsThreadpoolSchedulerException()
        {
        }

        public WindowsThreadpoolSchedulerException(string message) : base(message)
        {
        }

        public WindowsThreadpoolSchedulerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WindowsThreadpoolSchedulerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}