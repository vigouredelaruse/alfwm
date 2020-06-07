using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.persistence
{
    
    /// <summary>
    /// deliberately generic specification
    /// hopefully allowing implementers to provide their
    /// own CRUD of whatever type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public interface IPersistenceProvider
    {
        /// <summary>
        /// support persistable entity semantics
        /// </summary>
        string PersistenceProviderId { get; set; }

        string PersistenceProviderName { get; set; }

        string PersistenceProviderDisplayName { get; set; }

        /// <summary>
        /// support dynamic invocation by specifying the class nae
        /// </summary>
        string PersistenceProviderHostClassName { get; set; }

        /// <summary>
        /// support Activator symaitics
        /// </summary>
        string PersistenceProviderAssemblyName { get; set; }

        /// <summary>
        /// managing a context from the configuration left as an exercise to other code
        /// </summary>
        /// <typeparam name="TConfigureResult"></typeparam>
        /// <typeparam name="TProviderConfiguration"></typeparam>
        /// <param name="config"></param>
        /// <param name="configureProviderOperation"></param>
        /// <returns></returns>
        TConfigureResult ConfigureProvider<TConfigureResult, TProviderConfiguration>(TProviderConfiguration config, Func<TConfigureResult, TProviderConfiguration> configureProviderOperation);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCreateResult"></typeparam>
        /// <typeparam name="TCreateExpression"></typeparam>
        /// <typeparam name="TCreatedEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="createExpression"></param>
        /// <param name="createOperation"></param>
        /// <returns></returns>
        TCreateResult Create<TCreateResult, TCreateExpression, TCreatedEntity>(TCreatedEntity entity, TCreateExpression createExpression, Func<TCreateResult, TCreateExpression, TCreatedEntity> createOperation = null);

        /// <summary>
        /// support providing a method implementing the operation
        /// that takes as parameters the entity to be updated
        /// and optional update operation expressions 
        /// and a method for implementing the update
        /// </summary>
        /// <typeparam name="TUpdatedEntity"></typeparam>
        /// <typeparam name="TUpdateExpression"></typeparam>
        /// <typeparam name="TUpdateResult"></typeparam>
        /// <param name="entity"></param>
        /// <param name="updateExpression"></param>
        /// <param name="updateOperation"></param>
        /// <returns></returns>
        TUpdateResult Update<TUpdatedEntity, TUpdateExpression, TUpdateResult>(TUpdatedEntity entity, TUpdateExpression updateExpression, Func<TUpdateResult, TUpdateExpression, TUpdatedEntity> updateOperation = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDeletedEntity"></typeparam>
        /// <typeparam name="TDeleteExpression"></typeparam>
        /// <typeparam name="TDeleteOperationResult"></typeparam>
        /// <param name="entity"></param>
        /// <param name="deleteExpression"></param>
        /// <param name="deleeOperation"></param>
        /// <returns></returns>
        TDeleteOperationResult Delete<TDeletedEntity, TDeleteExpression, TDeleteOperationResult>(TDeletedEntity entity, TDeleteExpression deleteExpression, Func<TDeleteOperationResult, TDeletedEntity, TDeleteExpression> deleeOperation = null);

        /// <summary>
        /// designed to delegate read operations and specification 
        /// of their associated search expressions, the vast variety 
        /// of which cannot be known at library design time
        /// </summary>
        /// <typeparam name="TReadOperationResult"></typeparam>
        /// <typeparam name="TSearchExpression"></typeparam>
        /// <param name="searchExpression"></param>
        /// <param name="readOperation"></param>
        /// <returns></returns>
        TReadOperationResult Read<TReadOperationResult, TSearchExpression>(TSearchExpression searchExpression, Func<TReadOperationResult, TSearchExpression> readOperation);

        /// <summary>
        /// support input caching
        /// supports injection of the mechanism for 
        /// getting a reference to the input cache api
        /// </summary>
        /// <typeparam name="TInputItemCache"></typeparam>
        /// <param name="getInputItemCacheOperation"></param>
        /// <returns></returns>
        TInputItemCache GetInputItemCache<TInputItemCache>(Func<TInputItemCache> getInputItemCacheOperation = null);

        /// <summary>
        /// support injection of mechanism for getting
        /// a reference to output item cache
        /// </summary>
        /// <typeparam name="TOutputItemCache"></typeparam>
        /// <param name="getOutputItemCacheOperation"></param>
        /// <returns></returns>
        TOutputItemCache GetOutputItemCache<TOutputItemCache>(Func<TOutputItemCache> getOutputItemCacheOperation = null);

        /// <summary>
        /// support injection of logic for getting a 
        /// reference to the input queue api, of whatever type
        /// </summary>
        /// <typeparam name="TInputQueue"></typeparam>
        /// <param name="getInputQueueOperation"></param>
        /// <returns></returns>
        TInputQueue GetInputQueue<TInputQueue>(Func<TInputQueue> getInputQueueOperation = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOutputQueue"></typeparam>
        /// <param name="getOutputQueueOperation"></param>
        /// <returns></returns>
        TOutputQueue GetOutputQueue<TOutputQueue>(Func<TOutputQueue> getOutputQueueOperation = null);

        /// <summary>
        /// support injection of logic for setting a queue
        /// of whatever implementation type
        /// </summary>
        /// <typeparam name="TSetInputQueueResult"></typeparam>
        /// <typeparam name="TInputQueue"></typeparam>
        /// <param name="queue"></param>
        /// <param name="setInputQueueOperation"></param>
        /// <returns></returns>
        TSetInputQueueResult SetInputQueue<TSetInputQueueResult, TInputQueue>(TInputQueue queue, Func<TSetInputQueueResult, TInputQueue> setInputQueueOperation = null);


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSetOutputQueueResult"></typeparam>
        /// <typeparam name="TOutputQueue"></typeparam>
        /// <param name="queue"></param>
        /// <param name="setOutputQueueOperation"></param>
        /// <returns></returns>
        TSetOutputQueueResult SetOutputQueue<TSetOutputQueueResult, TOutputQueue>(TOutputQueue queue, Func<TSetOutputQueueResult, TOutputQueue> setOutputQueueOperation = null);

    }
}
