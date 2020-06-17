using com.ataxlab.alfwm.core.persistence;
using com.ataxlab.alfwm.core.taxonomy.processdefinition.flowchart.grammar.verbs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.IO;
using LiteDB;

namespace com.ataxlab.alfwm.persistence.litedb.processdefinition.flowchart.grammar.verbs
{


    /// <summary>
    /// a litedb implementation of a persistence provider
    /// </summary>
    public class LiteDbFlowchartDataSetProvider : IPersistenceProvider<LiteDbFlowchartDataSetProviderConfiguration, LiteDbFlowchartDataSetProviderConfigResult>, ILiteDbFlowchartDataSetProvider
    {


        public LiteDbFlowchartDataSetProvider()
        {

        }

        public string PersistenceProviderId { get; set; }
        public string PersistenceProviderName { get; set; }
        public string PersistenceProviderDisplayName { get; set; }
        public string PersistenceProviderHostClassName { get; set; }
        public string PersistenceProviderAssemblyName { get; set; }
        public LiteDbFlowchartDataSetProviderConfiguration ProviderConfiguration { get; set; }
        public FileInfo DatabaseFilePath { get; private set; }

        /// <summary>
        /// this merely sets the config property on the class
        /// and initializes some basic LiteDb properties
        /// 
        /// use an overload of this method to supply your own operation
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public virtual LiteDbFlowchartDataSetProviderConfigResult ConfigureProvider(LiteDbFlowchartDataSetProviderConfiguration config)
        {
            LiteDbFlowchartDataSetProviderConfigResult ret = new LiteDbFlowchartDataSetProviderConfigResult();
            this.ProviderConfiguration = config;


            try
            {
                this.DatabaseFilePath = new FileInfo(config.ConnectionString.Filename);

                if (config.IsMustEnsureExists)
                {
                    if (!this.DatabaseFilePath.Exists)
                    {
                        // initialize the database
                        using (var db = new LiteDatabase(config.ConnectionString))
                        {
                            if (!db.CollectionExists(config.CollectionName))
                            {
                                var validCollection = db.GetCollection(config.CollectionName);
                                if (config.IndexExpression != null && config.IndexName != String.Empty)
                                {
                                    // initialize index with supplied 
                                    // index expression and index name
                                    validCollection.EnsureIndex(config.IndexName, config.IndexExpression);
                                }
                            }



                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new LiteDbFlowchartDataSetProviderConfigurationException(ex.Message);
            }

           
            return ret;
        }


        public virtual LiteDbFlowchartDataSetProviderConfigResult ConfigureProvider(LiteDbFlowchartDataSetProviderConfiguration config, Func<LiteDbFlowchartDataSetProviderConfiguration, LiteDbFlowchartDataSetProviderConfigResult> configureProviderOperation)
        {
            if (configureProviderOperation == null)
            {
                throw new LiteDbFlowchartDataSetProviderException("null provider operation exception");               
            }

            return configureProviderOperation(config);
        }

        /// <summary>
        /// an insert operation to rule them all is a challenge to implement
        /// not to be attempted here
        /// 
        /// clients of this provider will supply their own types
        /// and their own operation to perform the insert
        /// operation, it's associated logging etc
        /// 
        /// see the unit tests for this method for an implementation
        /// </summary>
        /// <typeparam name="TCreateResult"></typeparam>
        /// <typeparam name="Query"></typeparam>
        /// <typeparam name="TCreatedEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="createExpression"></param>
        /// <param name="createOperation"></param>
        /// <returns></returns>
        public TCreateResult Create<TCreateResult, Query, TCreatedEntity>(TCreatedEntity entity, Query createExpression, Func<Query, TCreatedEntity, TCreateResult> createOperation)
        {
            TCreateResult ret = default(TCreateResult); ;

            if(createOperation == null)
            {

                /// here because the required delegate has not been provided
                throw new LiteDbFlowchartDataSetProviderException("invalid method invocation. you must hydrate the createOperation delegate");
            }
            else
            {
                try
                {
                    /// here because the required delegate has been provided. invoke it
                    ret = createOperation(createExpression, entity);
                }
                catch(Exception ex)
                {
                    throw new LiteDbFlowchartDataSetProviderException(ex.Message);
                }
            }

            return ret;
        }

        /// <summary>
        /// use of a generic delegate allows us to pass type 
        /// information through to generic API methods
        /// </summary>
        /// <typeparam name="TCreateResult"></typeparam>
        /// <typeparam name="TCreateExpression"></typeparam>
        /// <typeparam name="TCreatedEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="createExpression"></param>
        /// <param name="createOperation"></param>
        /// <returns></returns>
        public TCreateResult Create<TCreateResult, TCreateExpression, TCreatedEntity>(TCreatedEntity entity, TCreateExpression createExpression, EntityCreateOperation<TCreateExpression, TCreatedEntity, TCreateResult> createOperation = null)
            where TCreateResult : class
            where TCreateExpression : class
            where TCreatedEntity : class
        {
            TCreateResult ret = default(TCreateResult); ;

            if (createOperation == null)
            {

                /// here because the required delegate has not been provided
                throw new LiteDbFlowchartDataSetProviderException("invalid method invocation. you must hydrate the createOperation delegate");
            }
            else
            {
                try
                {
                    /// here because the required delegate has been provided. invoke it
                    ret = createOperation(createExpression, entity);
                }
                catch (Exception ex)
                {
                    throw new LiteDbFlowchartDataSetProviderException(ex.Message);
                }
            }

            return ret;
        }

        public TDeleteOperationResult Delete<TDeletedEntity, TDeleteExpression, TDeleteOperationResult>(TDeletedEntity entity, TDeleteExpression deleteExpression, Func<TDeletedEntity, TDeleteExpression, TDeleteOperationResult> deleteOperation = null)
        {
            TDeleteOperationResult ret = default(TDeleteOperationResult);

            if (deleteOperation == null)
            {
                /// here because the required delegate has not been provided
                throw new LiteDbFlowchartDataSetProviderException("invalid method invocation. you must hydrate the createOperation delegate");
            }
            else
            {

                try
                {
                    /// here because the required delegate has been provided. invoke it
                    ret = deleteOperation(entity, deleteExpression);
                }
                catch (Exception e)
                {
                    throw new LiteDbFlowchartDataSetProviderException(e.Message);
                }
            }

            return ret;
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
            TReadOperationResult ret = default(TReadOperationResult);

            if(readOperation == null)
            {
                throw new LiteDbFlowchartDataSetProviderException("invalid method invocation. you must hydrate the createOperation delegate");
            }
            else
            {
                try
                {
                    ret = readOperation(searchExpression);
                }
                catch(Exception e)
                {
                    throw new LiteDbFlowchartDataSetProviderException(e.Message);
                }
            }

            return ret;
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

            TUpdateResult ret = default(TUpdateResult);

            if (updateOperation == null)
            {
                /// here because the required delegate has not been provided
                throw new LiteDbFlowchartDataSetProviderException("invalid method invocation. you must hydrate the createOperation delegate");

            }
            else
            {
                try
                {
                    ret = updateOperation(updateExpression, entity);
                }
                catch(Exception e)
                {
                    throw new LiteDbFlowchartDataSetProviderException(e.Message);
                }
            }


            return ret;
        }
    }
}
