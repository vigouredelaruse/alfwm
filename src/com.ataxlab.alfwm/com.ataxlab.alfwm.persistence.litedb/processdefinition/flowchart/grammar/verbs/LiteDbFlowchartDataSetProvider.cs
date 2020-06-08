using com.ataxlab.alfwm.core.taxonomy.processdefinition.flowchart.grammar.verbs;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.persistence.litedb.processdefinition.flowchart.grammar.verbs
{

    /// <summary>
    /// a litedb implementation of a persistence provider
    /// </summary>
    public class LiteDbFlowchartDataSetProvider : FlowchartDataSetProvider<LiteDbFlowchartDataSetProviderConfiguration>, ILiteDbFlowchartDataSetProvider
    {
        public override string PersistenceProviderId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string PersistenceProviderName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string PersistenceProviderDisplayName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string PersistenceProviderHostClassName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string PersistenceProviderAssemblyName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override LiteDbFlowchartDataSetProviderConfiguration ProviderConfiguration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override TConfigureResult ConfigureProvider<TConfigureResult>(LiteDbFlowchartDataSetProviderConfiguration config, Func<LiteDbFlowchartDataSetProviderConfiguration, TConfigureResult> configureProviderOperation)
        {
            throw new NotImplementedException();
        }

        public override TCreateResult Create<TCreateResult, TCreateExpression, TCreatedEntity>(TCreatedEntity entity, TCreateExpression createExpression, Func<TCreateExpression, TCreatedEntity, TCreateResult> createOperation = null)
        {
            throw new NotImplementedException();
        }

        public override TDeleteOperationResult Delete<TDeletedEntity, TDeleteExpression, TDeleteOperationResult>(TDeletedEntity entity, TDeleteExpression deleteExpression, Func<TDeletedEntity, TDeleteExpression, TDeleteOperationResult> deleeOperation = null)
        {
            throw new NotImplementedException();
        }

        public override TInputItemCache GetInputItemCache<TInputItemCache>(Func<TInputItemCache> getInputItemCacheOperation = null)
        {
            throw new NotImplementedException();
        }

        public override TInputQueue GetInputQueue<TInputQueue>(Func<TInputQueue> getInputQueueOperation = null)
        {
            throw new NotImplementedException();
        }

        public override TOutputItemCache GetOutputItemCache<TOutputItemCache>(Func<TOutputItemCache> getOutputItemCacheOperation = null)
        {
            throw new NotImplementedException();
        }

        public override TOutputQueue GetOutputQueue<TOutputQueue>(Func<TOutputQueue> getOutputQueueOperation = null)
        {
            throw new NotImplementedException();
        }

        public override TReadOperationResult Read<TReadOperationResult, TSearchExpression>(TSearchExpression searchExpression, Func<TSearchExpression, TReadOperationResult> readOperation)
        {
            throw new NotImplementedException();
        }

        public override TSetInputQueueResult SetInputQueue<TSetInputQueueResult, TInputQueue>(TInputQueue queue, Func<TInputQueue, TSetInputQueueResult> setInputQueueOperation = null)
        {
            throw new NotImplementedException();
        }

        public override TSetOutputQueueResult SetOutputQueue<TSetOutputQueueResult, TOutputQueue>(TOutputQueue queue, Func<TOutputQueue, TSetOutputQueueResult> setOutputQueueOperation = null)
        {
            throw new NotImplementedException();
        }

        public override TUpdateResult Update<TUpdatedEntity, TUpdateExpression, TUpdateResult>(TUpdatedEntity entity, TUpdateExpression updateExpression, Func<TUpdateExpression, TUpdatedEntity, TUpdateResult> updateOperation = null)
        {
            throw new NotImplementedException();
        }
    }
}
