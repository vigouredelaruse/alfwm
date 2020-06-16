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

        TCreateResult ILiteDbFlowchartDataSetProvider.Create<TCreateResult, Query, TCreatedEntity>(TCreatedEntity entity, Query createExpression, Func<Query, TCreatedEntity, TCreateResult> createOperation)
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
