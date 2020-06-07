﻿using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.persistence
{
    /// <summary>
    /// honestly this is similar to a marker interface
    /// but supplied as an abstract class
    /// 
    /// this is a prime target for refactoring
    /// 
    /// for instance the need needs to be proved
    /// that CRUD can be generalized in this way for a random situation
    /// </summary>
    public abstract class PersistenceProvider : IPersistenceProvider
    {
        public PersistenceProvider()
        { }

        public abstract string PersistenceProviderId { get; set; }
        public abstract string PersistenceProviderName { get; set; }
        public abstract string PersistenceProviderDisplayName { get; set; }
        public abstract string PersistenceProviderHostClassName { get; set; }
        public abstract string PersistenceProviderAssemblyName { get; set; }

        public abstract TConfigureResult ConfigureProvider<TConfigureResult, TProviderConfiguration>(TProviderConfiguration config, Func<TConfigureResult, TProviderConfiguration> configureProviderOperation);
        public abstract TCreateResult Create<TCreateResult, TCreateExpression, TCreatedEntity>(TCreatedEntity entity, TCreateExpression createExpression, Func<TCreateResult, TCreateExpression, TCreatedEntity> createOperation = null);
        public abstract TDeleteOperationResult Delete<TDeletedEntity, TDeleteExpression, TDeleteOperationResult>(TDeletedEntity entity, TDeleteExpression deleteExpression, Func<TDeleteOperationResult, TDeletedEntity, TDeleteExpression> deleeOperation = null);
        public abstract TInputItemCache GetInputItemCache<TInputItemCache>(Func<TInputItemCache> getInputItemCacheOperation = null);
        public abstract TInputQueue GetInputQueue<TInputQueue>(Func<TInputQueue> getInputQueueOperation = null);
        public abstract TOutputItemCache GetOutputItemCache<TOutputItemCache>(Func<TOutputItemCache> getOutputItemCacheOperation = null);
        public abstract TOutputQueue GetOutputQueue<TOutputQueue>(Func<TOutputQueue> getOutputQueueOperation = null);
        public abstract TReadOperationResult Read<TReadOperationResult, TSearchExpression>(TSearchExpression searchExpression, Func<TReadOperationResult, TSearchExpression> readOperation);
        public abstract TSetInputQueueResult SetInputQueue<TSetInputQueueResult, TInputQueue>(TInputQueue queue, Func<TSetInputQueueResult, TInputQueue> setInputQueueOperation = null);
        public abstract TSetOutputQueueResult SetOutputQueue<TSetOutputQueueResult, TOutputQueue>(TOutputQueue queue, Func<TSetOutputQueueResult, TOutputQueue> setOutputQueueOperation = null);
        public abstract TUpdateResult Update<TUpdatedEntity, TUpdateExpression, TUpdateResult>(TUpdatedEntity entity, TUpdateExpression updateExpression, Func<TUpdateResult, TUpdateExpression, TUpdatedEntity> updateOperation = null);
    }
}
                                                                  