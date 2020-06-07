using System;
using System.Runtime.Serialization;

namespace com.ataxlab.alfwm.scheduler.uwp.threadpool
{
    [Serializable]
    internal class ActivityStartedException : Exception
    {
        public ActivityStartedException()
        {
        }

        public ActivityStartedException(string message) : base(message)
        {
        }

        public ActivityStartedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ActivityStartedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}