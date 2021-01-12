using System;
using System.Runtime.Serialization;

namespace com.ataxlab.alfwm.core.taxonomy.binding.queue
{
    [Serializable]
    internal class InvalidQueueingPipelineToolGatewayStateException : Exception
    {
        private Exception e;

        public InvalidQueueingPipelineToolGatewayStateException()
        {
        }

        public InvalidQueueingPipelineToolGatewayStateException(Exception e)
        {
            this.e = e;
        }

        public InvalidQueueingPipelineToolGatewayStateException(string message) : base(message)
        {
        }

        public InvalidQueueingPipelineToolGatewayStateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidQueueingPipelineToolGatewayStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}