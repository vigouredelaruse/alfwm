using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    /// <summary>
    /// specify a pipeline tool that supports
    /// pub/sub pipeline queue consumption semantics
    /// 
    /// the intent is to supply input and output bindings
    /// that have queueing behaviour
    /// 
    /// input bindings pool the queue until new data
    /// arrives whereupon the binding signals 
    /// the tool and pauses the polling timer
    /// 
    /// the tool performs the equivalent of ICommand.Execute()
    /// dequeueing as much data as it likes from the binding
    /// before restarting the polling timer
    /// </summary>
    /// <typeparam name="TLatchingInputBinding"></typeparam>
    /// <typeparam name="TOutputBinding"></typeparam>
    public interface IQueueingPipelineTool<TLatchingInputBinding, TOutputBinding, TQueueEntity> : IPipelineTool 
        where TLatchingInputBinding : class
        where TOutputBinding : class
        where TQueueEntity : class, new()
    {
        /// <summary>
        /// latching input binding that latches signalling
        /// after signalling new data. 
        /// 
        /// callers can add data to the queue
        /// but the binding does not signal new arrival
        /// until the latch is reset
        /// 
        /// consumer is free to dequeue as many
        /// events as it likes before renabling the 
        /// polling timer
        /// </summary>
        TLatchingInputBinding InputBinding { get; set; }

        /// <summary>
        /// signalled when the queue input binding
        /// detects data arrived at the queue
        /// 
        /// equivalent to ICommand.Execute()
        /// </summary>
        /// <param name="timestamp"></param>
        void OnQueueHasData(object sender, TQueueEntity availableData);

        /// <summary>
        /// clients of the queue pipeline tool can listen to this event
        /// for notificaton of new arrivals on the quueue
        /// 
        /// the result is could be reflected on the queuing output binding collection of the tool, for instance
        /// </summary>
        event Func<TQueueEntity, TQueueEntity> QueueHasAvailableDataEvent;


        /// <summary>
        /// support fanout scenarious
        /// where a tool produces data on multiple outputs
        /// </summary>
        List<TOutputBinding> QueueingOutputBindingCollection { get; set; }
    }
}
