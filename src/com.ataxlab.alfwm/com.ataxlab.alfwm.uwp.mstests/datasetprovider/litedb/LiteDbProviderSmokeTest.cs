using com.ataxlab.alfwm.core.persistence;
using com.ataxlab.alfwm.persistence.litedb.processdefinition.flowchart.grammar.verbs;
using LiteDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Protection.PlayReady;

namespace com.ataxlab.alfwm.uwp.mstests.datasetprovider.litedb
{
    [TestClass]
    public class LiteDbProviderSmokeTest : IPersistenceProvider<LiteDbFlowchartDataSetProviderConfiguration, LiteDbFlowchartDataSetProviderConfigResult>, ILiteDbFlowchartDataSetProvider
    {
        LiteDbFlowchartDataSetProvider testedClass = new LiteDbFlowchartDataSetProvider();

        [TestInitialize]
        public void PlayReadyDecryptorSetup()
        {
            //Expression<Func<bool>> indexExpr = 
            //var config =
            //    new LiteDbFlowchartDataSetProviderConfiguration(;
            //testedClass.ConfigureProvider()
            //// testClass.
            ///

            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;

            var filePath = storageFolder.Path;
            var dbFileName = filePath + "\\test.litedb";

            TestConnectionString = new ConnectionString(dbFileName)
            {
                Password = "password",
            };


            TestCollectionName = "testcollection";
            IndexExpression = "$.JsonValue";  /// https://www.litedb.org/docs/expressions/
            TestIndexName = "TestIndex";

            TestedProviderConfiguration =
                new LiteDbFlowchartDataSetProviderConfiguration(TestConnectionString,
                IndexExpression, TestCollectionName, TestIndexName, true);

           

        }

        public string PersistenceProviderId { get; set; }
        public string PersistenceProviderName { get; set; }
        public string PersistenceProviderDisplayName { get; set; }
        public string PersistenceProviderHostClassName { get; set; }
        public string PersistenceProviderAssemblyName { get; set; }
        public LiteDbFlowchartDataSetProviderConfiguration ProviderConfiguration { get; set; }
        public ConnectionString TestConnectionString { get; set; }
        public string TestCollectionName { get; set; }
        public string IndexExpression { get; set; }
        public string TestIndexName { get; set; }
        public LiteDbFlowchartDataSetProviderConfiguration TestedProviderConfiguration { get; set; }

        [TestMethod]
        public void TestConfigureProvider()
        {
            Exception e = null;
            try
            {
                var results = testedClass.ConfigureProvider(this.TestedProviderConfiguration);
            }
            catch(Exception ee)
            {
                e = ee;
            }

            Assert.IsNull(e, "failed with exception " + e?.Message);
        }

        public LiteDbFlowchartDataSetProviderConfigResult ConfigureProvider(LiteDbFlowchartDataSetProviderConfiguration config, Func<LiteDbFlowchartDataSetProviderConfiguration, LiteDbFlowchartDataSetProviderConfigResult> configureProviderOperation)
        {
            throw new NotImplementedException();
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

        /// <summary>
        /// required method implementation for user supplied 
        /// provider configure operation
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        LiteDbFlowchartDataSetProviderConfigResult ConfigureProviderOperation(LiteDbFlowchartDataSetProviderConfiguration config)
        {
            int i = 0;

            // this operation can do something else
            // besides or instead of calling
            // the provider's builtin configure method
            // 
            // this user supplied operation should perform operations
            // that match those inferred by the moethod signature
            // of the builtin provider configuration method
            //
            // at least the operation should operate against
            // the populated properties of the configuration
            return this.testedClass.ConfigureProvider(config);
        }

        /// <summary>
        /// test configuring the provider 
        /// by supplying configuration and
        /// a delegate
        /// </summary>
        [TestMethod]
        public void TestConfigureProvideOperation()
        {
            Exception e = null;


            // initialize the func parameter with the required user supplied method signature implementation
            Func<LiteDbFlowchartDataSetProviderConfiguration, LiteDbFlowchartDataSetProviderConfigResult> configProviderOperation =
                new Func<LiteDbFlowchartDataSetProviderConfiguration, LiteDbFlowchartDataSetProviderConfigResult>(ConfigureProviderOperation);

            try
            {
                // call the provider's configure method
                // with your initialized configuration and func
                var result = testedClass.ConfigureProvider(TestedProviderConfiguration,
                   configProviderOperation
                    );

                // examine the result 
                Assert.IsNotNull(result);
            }
            catch(Exception ee)
            {
                e = ee;
            }

            Assert.IsNull(e, "exception configuring provider " + e?.Message);
        }

        public LiteDbFlowchartDataSetProviderConfigResult ConfigureProvider(LiteDbFlowchartDataSetProviderConfiguration config)
        {
            throw new NotImplementedException();
        }
    }
}
