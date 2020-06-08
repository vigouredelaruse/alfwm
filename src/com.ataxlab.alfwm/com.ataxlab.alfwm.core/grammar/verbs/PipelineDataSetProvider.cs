
using com.ataxlab.alfwm.core.persistence;
using com.ataxlab.alfwm.core.taxonomy.binding;
using Newtonsoft.Json.Linq;
using System;

namespace com.ataxlab.alfwm.core.grammar.verbs
{
    /// <summary>
    /// contract for Pipeline Dataset Providers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //public abstract class PipelineDataSetProvider<U> : IPipelineDataSetProvider where U : IPersistenceProvider
    //{
    //    public abstract string PersistenceProviderId { get; set; }
    //    public abstract string PersistenceProviderName { get; set; }
    //    public abstract string PersistenceProviderDisplayName { get; set; }
    //    public abstract string PersistenceProviderHostClassName { get; set; }
    //    public abstract string PersistenceProviderAssemblyName { get; set; }
    //    public abstract string PipelineBindingDisplayName { get; set; }
    //    public abstract string PipelineBindingKey { get; set; }
    //    public abstract PipelineVariableDictionary PipelineBindingValue { get; set; }

    //    public abstract TConfigureResult ConfigureProvider<TConfigureResult, TProviderConfiguration>(TProviderConfiguration config, Func<TProviderConfiguration, TConfigureResult> configureProviderOperation)
    //        where TConfigureResult : class, new()
    //        where TProviderConfiguration : class, new();
    //    public abstract TCreateResult Create<TCreateResult, TCreateExpression, TCreatedEntity>(TCreatedEntity entity, TCreateExpression createExpression, Func<TCreateExpression, TCreatedEntity, TCreateResult> createOperation = null);
    //    public abstract TDeleteOperationResult Delete<TDeletedEntity, TDeleteExpression, TDeleteOperationResult>(TDeletedEntity entity, TDeleteExpression deleteExpression, Func<TDeletedEntity, TDeleteExpression, TDeleteOperationResult> deleeOperation = null);
    //    public abstract TInputItemCache GetInputItemCache<TInputItemCache>(Func<TInputItemCache> getInputItemCacheOperation = null);
    //    public abstract TInputQueue GetInputQueue<TInputQueue>(Func<TInputQueue> getInputQueueOperation = null);
    //    public abstract TOutputItemCache GetOutputItemCache<TOutputItemCache>(Func<TOutputItemCache> getOutputItemCacheOperation = null);
    //    public abstract TOutputQueue GetOutputQueue<TOutputQueue>(Func<TOutputQueue> getOutputQueueOperation = null);
    //    public abstract TReadOperationResult Read<TReadOperationResult, TSearchExpression>(TSearchExpression searchExpression, Func<TSearchExpression, TReadOperationResult> readOperation);
    //    public abstract TSetInputQueueResult SetInputQueue<TSetInputQueueResult, TInputQueue>(TInputQueue queue, Func<TInputQueue, TSetInputQueueResult> setInputQueueOperation = null);
    //    public abstract TSetOutputQueueResult SetOutputQueue<TSetOutputQueueResult, TOutputQueue>(TOutputQueue queue, Func<TOutputQueue, TSetOutputQueueResult> setOutputQueueOperation = null);
    //    public abstract TUpdateResult Update<TUpdatedEntity, TUpdateExpression, TUpdateResult>(TUpdatedEntity entity, TUpdateExpression updateExpression, Func<TUpdateExpression, TUpdatedEntity, TUpdateResult> updateOperation = null);
    //}
}
