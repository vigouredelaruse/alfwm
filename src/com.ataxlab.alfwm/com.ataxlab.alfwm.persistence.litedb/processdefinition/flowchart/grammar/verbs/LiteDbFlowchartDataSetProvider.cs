using com.ataxlab.alfwm.core.taxonomy.processdefinition.flowchart.grammar.verbs;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.persistence.litedb.processdefinition.flowchart.grammar.verbs
{

    public class LiteDbFlowchartDataSetProvider : FlowchartDataSetProvider
    {
        public override string PersistenceProviderId { get; set; }
        public override string PersistenceProviderName { get; set; }
        public override string PersistenceProviderDisplayName { get; set; }
        public override string PersistenceProviderHostClassName { get; set; }
        public override string PersistenceProviderAssemblyName { get; set; }

        public override TConfigureResult ConfigureProvider<TConfigureResult, TProviderConfiguration>(TProviderConfiguration config, Func<TConfigureResult, TProviderConfiguration> configureProviderOperation)
        {
            throw new NotImplementedException();
        }

        public override TCreateResult Create<TCreateResult, TCreateExpression, TCreatedEntity>(TCreatedEntity entity, TCreateExpression createExpression, Func<TCreateResult, TCreateExpression, TCreatedEntity> createOperation = null)
        {
            throw new NotImplementedException();
        }

        public override TDeleteOperationResult Delete<TDeletedEntity, TDeleteExpression, TDeleteOperationResult>(TDeletedEntity entity, TDeleteExpression deleteExpression, Func<TDeleteOperationResult, TDeletedEntity, TDeleteExpression> deleeOperation = null)
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

        public override TReadOperationResult Read<TReadOperationResult, TSearchExpression>(TSearchExpression searchExpression, Func<TReadOperationResult, TSearchExpression> readOperation)
        {
            throw new NotImplementedException();
        }

        public override TSetInputQueueResult SetInputQueue<TSetInputQueueResult, TInputQueue>(TInputQueue queue, Func<TSetInputQueueResult, TInputQueue> setInputQueueOperation = null)
        {
            throw new NotImplementedException();
        }

        public override TSetOutputQueueResult SetOutputQueue<TSetOutputQueueResult, TOutputQueue>(TOutputQueue queue, Func<TSetOutputQueueResult, TOutputQueue> setOutputQueueOperation = null)
        {
            throw new NotImplementedException();
        }

        public override TUpdateResult Update<TUpdatedEntity, TUpdateExpression, TUpdateResult>(TUpdatedEntity entity, TUpdateExpression updateExpression, Func<TUpdateResult, TUpdateExpression, TUpdatedEntity> updateOperation = null)
        {
            throw new NotImplementedException();
        }
    }
}
