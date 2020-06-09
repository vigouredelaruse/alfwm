using com.ataxlab.alfwm.core.persistence;
using com.ataxlab.alfwm.core.taxonomy.processdefinition.flowchart.grammar.verbs;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.persistence.litedb.processdefinition.flowchart.grammar.verbs
{

    /// <summary>
    /// a litedb implementation of a persistence provider
    /// </summary>
    public class LiteDbFlowchartDataSetProvider : IPersistenceProvider<LiteDbFlowchartDataSetProviderConfiguration, LiteDbFlowchartDataSetProviderConfigResult>, ILiteDbFlowchartDataSetProvider
    {
        public string PersistenceProviderId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PersistenceProviderName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PersistenceProviderDisplayName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PersistenceProviderHostClassName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PersistenceProviderAssemblyName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public LiteDbFlowchartDataSetProviderConfiguration ProviderConfiguration { get; set; }

        /// <summary>
        /// this merely sets the config property on the class
        /// use an overload of this method to supply your own operation
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public virtual LiteDbFlowchartDataSetProviderConfigResult ConfigureProvider(LiteDbFlowchartDataSetProviderConfiguration config)
        {

            this.ProviderConfiguration = config;
            LiteDbFlowchartDataSetProviderConfigResult ret = default(LiteDbFlowchartDataSetProviderConfigResult); // new LiteDbFlowchartDataSetProviderConfigResult() { };     
            return ret;
        }

        public virtual LiteDbFlowchartDataSetProviderConfigResult ConfigureProvider(LiteDbFlowchartDataSetProviderConfiguration config, Func<LiteDbFlowchartDataSetProviderConfiguration, LiteDbFlowchartDataSetProviderConfigResult> configureProviderOperation)
        {
            return configureProviderOperation(this.ProviderConfiguration);
        }

        public TCreateResult Create<TCreateResult, TCreateExpression, TCreatedEntity>(TCreatedEntity entity, TCreateExpression createExpression, Func<TCreateExpression, TCreatedEntity, TCreateResult> createOperation = null)
        {
            throw new NotImplementedException();
        }

        public TDeleteOperationResult Delete<TDeletedEntity, TDeleteExpression, TDeleteOperationResult>(TDeletedEntity entity, TDeleteExpression deleteExpression, Func<TDeletedEntity, TDeleteExpression, TDeleteOperationResult> deleeOperation = null)
        {
            throw new NotImplementedException();
        }

        public TInputItemCache GetInputItemCache<TInputItemCache>(Func<TInputItemCache> getInputItemCacheOperation = null)
        {
            throw new NotImplementedException();
        }

        public TInputQueue GetInputQueue<TInputQueue>(Func<TInputQueue> getInputQueueOperation = null)
        {
            throw new NotImplementedException();
        }

        public TOutputItemCache GetOutputItemCache<TOutputItemCache>(Func<TOutputItemCache> getOutputItemCacheOperation = null)
        {
            throw new NotImplementedException();
        }

        public TOutputQueue GetOutputQueue<TOutputQueue>(Func<TOutputQueue> getOutputQueueOperation = null)
        {
            throw new NotImplementedException();
        }

        public TReadOperationResult Read<TReadOperationResult, TSearchExpression>(TSearchExpression searchExpression, Func<TSearchExpression, TReadOperationResult> readOperation)
        {
            throw new NotImplementedException();
        }

        public TSetInputQueueResult SetInputQueue<TSetInputQueueResult, TInputQueue>(TInputQueue queue, Func<TInputQueue, TSetInputQueueResult> setInputQueueOperation = null)
        {
            throw new NotImplementedException();
        }

        public TSetOutputQueueResult SetOutputQueue<TSetOutputQueueResult, TOutputQueue>(TOutputQueue queue, Func<TOutputQueue, TSetOutputQueueResult> setOutputQueueOperation = null)
        {
            throw new NotImplementedException();
        }

        public TUpdateResult Update<TUpdatedEntity, TUpdateExpression, TUpdateResult>(TUpdatedEntity entity, TUpdateExpression updateExpression, Func<TUpdateExpression, TUpdatedEntity, TUpdateResult> updateOperation = null)
        {
            throw new NotImplementedException();
        }
    }
}
