using System;

namespace com.ataxlab.alfwm.persistence.litedb.processdefinition.flowchart.grammar.verbs
{

    public delegate LiteDbFlowchartDataSetProviderConfigResult ConfigureProviderOperation(LiteDbFlowchartDataSetProviderConfiguration config);

    /// <summary>
    /// delegate signature callers need to implement to supply their own Create operation against the provider
    /// there is no default implementation of CRUD operations against this provider
    /// </summary>
    /// <typeparam name="TCreateExpression"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TCreateOperationResult"></typeparam>
    /// <param name="createExpression"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public delegate TCreateOperationResult EntityCreateOperation<TCreateExpression, TEntity, TCreateOperationResult>(TCreateExpression createExpression, TEntity entity)
        where TCreateOperationResult : class
        where TCreateExpression : class
        where TEntity : class;

    /// <summary>
    /// delegate signature callers need to implement to supply their own Read operation against the provider
    /// there is no default implementation of CRUD operations against this provider
    /// </summary>
    /// <typeparam name="TReadExpression"></typeparam>
    /// <typeparam name="TReadOperationResult"></typeparam>
    /// <param name="queryExpression"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public delegate TReadOperationResult EntityReadOperation<TReadExpression, TReadOperationResult>(TReadExpression queryExpression)
    where TReadOperationResult : class
    where TReadExpression : class;

    public interface ILiteDbFlowchartDataSetProvider
    {


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
        TCreateResult Create<TCreateResult, TCreateExpression, TCreatedEntity>(TCreatedEntity entity, TCreateExpression createExpression, Func<TCreateExpression, TCreatedEntity, TCreateResult> createOperation = null);

        TCreateResult Create<TCreateResult, TCreateExpression, TCreatedEntity>(TCreatedEntity entity, TCreateExpression createExpression, EntityCreateOperation<TCreateExpression, TCreatedEntity, TCreateResult> createOperation = null)
                            where TCreateResult : class
                            where TCreateExpression : class
                            where TCreatedEntity : class;

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
        TUpdateResult Update<TUpdatedEntity, TUpdateExpression, TUpdateResult>(TUpdatedEntity entity, TUpdateExpression updateExpression, Func<TUpdateExpression, TUpdatedEntity, TUpdateResult> updateOperation = null);

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
        TDeleteOperationResult Delete<TDeletedEntity, TDeleteExpression, TDeleteOperationResult>(TDeletedEntity entity, TDeleteExpression deleteExpression, Func<TDeletedEntity, TDeleteExpression, TDeleteOperationResult> deleeOperation = null);

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
        TReadOperationResult Read<TReadOperationResult, TSearchExpression>(TSearchExpression searchExpression, EntityReadOperation<TSearchExpression, TReadOperationResult> readOperation)
                    where TReadOperationResult : class
                    where TSearchExpression : class;

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
        TSetInputQueueResult SetInputQueue<TSetInputQueueResult, TInputQueue>(TInputQueue queue, Func<TInputQueue, TSetInputQueueResult> setInputQueueOperation = null);


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSetOutputQueueResult"></typeparam>
        /// <typeparam name="TOutputQueue"></typeparam>
        /// <param name="queue"></param>
        /// <param name="setOutputQueueOperation"></param>
        /// <returns></returns>
        TSetOutputQueueResult SetOutputQueue<TSetOutputQueueResult, TOutputQueue>(TOutputQueue queue, Func<TOutputQueue, TSetOutputQueueResult> setOutputQueueOperation = null);

    }
}