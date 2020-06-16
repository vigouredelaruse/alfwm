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

        public string PersistenceProviderId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PersistenceProviderName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PersistenceProviderDisplayName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PersistenceProviderHostClassName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PersistenceProviderAssemblyName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public LiteDbFlowchartDataSetProviderConfiguration ProviderConfiguration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ConnectionString TestConnectionString { get; private set; }
        public string TestCollectionName { get; private set; }
        public string IndexExpression { get; private set; }
        public string TestIndexName { get; private set; }
        public LiteDbFlowchartDataSetProviderConfiguration TestedProviderConfiguration { get; private set; }

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

        LiteDbFlowchartDataSetProviderConfigResult ConfigureProviderOperation(LiteDbFlowchartDataSetProviderConfiguration config)
        {
            int i = 0;
            return this.testedClass.ConfigureProvider(config);
        }

        [TestMethod]
        public void TestConfigureProvideOperation()
        {
            Exception e = null;


            Func<LiteDbFlowchartDataSetProviderConfiguration, LiteDbFlowchartDataSetProviderConfigResult> configProvider =
                new Func<LiteDbFlowchartDataSetProviderConfiguration, LiteDbFlowchartDataSetProviderConfigResult>(ConfigureProviderOperation);

            try
            {
                var result = testedClass.ConfigureProvider(this.ProviderConfiguration,
                   configProvider
                    );
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
