using com.ataxlab.alfwm.core.persistence;
using com.ataxlab.alfwm.utility.extension;
using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.persistence.litedb.processdefinition.flowchart.grammar.verbs;
using com.ataxlab.alfwm.uwp.mstests.datasetprovider.litedb.model;
using LiteDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        TestPipelineVariable TestEntity;

        public void InitializeTestEntity()
        {
            // initialize the payload of the pipeline variable
            var testTask = new TaskItem()
            {
                Id = Guid.NewGuid().ToString(),
                StartTimme = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddHours(1),
                TaskName = "Task Name",
                TaskSummary = "Task Summary."
            };

            /// initialize the test entity with it's 
            /// payload
            TestEntity  = new TestPipelineVariable(testTask);

            TestEntity.ID = Guid.NewGuid().ToString();
            TestEntity.Key = "TestVariableKey";
            TestEntity.TimeStamp = DateTime.UtcNow;
            TestEntity.CreateDate = DateTime.UtcNow;
            TestEntity.Description = "Test Entity Description";
            TestEntity.DisplayName = "Test Entity Display Name";
            
        }

        [TestInitialize]
        public void Setup()
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

            // implicit test of provider builtin config operation 
            // used ere to configure a provider uniformly for these tests
            testedClass.ConfigureProvider(TestedProviderConfiguration);

            /// initialize a test entity
            InitializeTestEntity();

        }

        #region interface properties
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

        #endregion interface properties

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


        [TestMethod]
        public void TestCreate()
        {
            Exception e = null;

            // initialize a create expression for
            // the delegate
            var createExpression = new CreateExpression<TestPipelineVariable>();
            createExpression.CollectionName = this.TestCollectionName;
            createExpression.NewEntity = this.TestEntity;

            // initialize the func
            Func<CreateExpression<TestPipelineVariable>, TestPipelineVariable, CreateResult> createOperation =
                new Func<CreateExpression<TestPipelineVariable>, TestPipelineVariable, CreateResult>(CreateOperation);

            try
            {
                var result = testedClass.Create(TestEntity, createExpression, createOperation);

                Assert.IsNotNull(result, "create operation failed. null result");
            }
            catch(Exception ex)
            {
                e = ex;
            }


            Assert.IsNull(e, "test failed, exception");
        }

        public CreateResult CreateOperation(CreateExpression<TestPipelineVariable> createExpression, TestPipelineVariable entity)
        {
            CreateResult ret = new CreateResult();
            try
            {
                // conveniently our delegate has access to the enclosing class
                // should that be a viewmodel or a service or a test harness
                using (var db = new LiteDatabase(this.TestConnectionString.Filename))
                {
                    var col = db.GetCollection<TestPipelineVariable>(createExpression.CollectionName);
                    col.Insert(entity);
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            return ret;
        }

        [TestMethod]
        public void TestCreateWithCreateOperationDelegate()
        {
            Exception e = null;

            var clonedEntity = TestEntity.Clone() as TestPipelineVariable;
            clonedEntity.ID = Guid.NewGuid().ToString();

            // initialize a create expression for
            // the delegate
            var createExpression = new CreateExpression<TestPipelineVariable>();
            createExpression.CollectionName = this.TestCollectionName;
            createExpression.NewEntity = this.TestEntity;

            // initialize the delegate
            EntityCreateOperation<CreateExpression<TestPipelineVariable>, TestPipelineVariable, CreateResult> testOperation =
                new EntityCreateOperation<CreateExpression<TestPipelineVariable>, TestPipelineVariable, CreateResult>
                (CreateWithOperationDelegate);

            try
            {

                // test the method
                var result = testedClass.Create(TestEntity, createExpression, testOperation);

                Assert.IsNotNull(result, "test failed with null result");
            }
            catch(Exception ex)
            {
                e = ex;
            }

            Assert.IsNull(e, "failed test with exception " + e?.Message);
        }

        /// <summary>
        /// delegate implementation for Create operation
        /// </summary>
        /// <param name="createExpression"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private CreateResult CreateWithOperationDelegate(CreateExpression<TestPipelineVariable> createExpression, TestPipelineVariable entity)
        {
            CreateResult ret = new CreateResult();
            try
            {
                // conveniently our delegate has access to the enclosing class
                // should that be a viewmodel or a service or a test harness
                using (var db = new LiteDatabase(this.TestConnectionString))
                {
                    var col = db.GetCollection<TestPipelineVariable>(createExpression.CollectionName);
                    col.Insert(entity);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return ret;
        }

        public TCreateResult Create<TCreateResult, TCreateExpression, TCreatedEntity>(TCreatedEntity entity, TCreateExpression createExpression, EntityCreateOperation<TCreateExpression, TCreatedEntity, TCreateResult> createOperation = null)
            where TCreateResult : class
            where TCreateExpression : class
            where TCreatedEntity : class
        {
            throw new NotImplementedException();
        }

        public TCreateResult Create<TCreateResult, TCreateExpression, TCreatedEntity>(TCreatedEntity entity, TCreateExpression createExpression, Func<TCreateExpression, TCreatedEntity, TCreateResult> createOperation = null)
        {
            //Func<TCreateExpression, TCreatedEntity, TCreateResult> targetOperation = 
            //    new Func<TCreateExpression, TCreatedEntity, TCreateResult>()

            TCreateResult ret = default(TCreateResult);

            return ret;
        }

        /// <summary>
        /// test a thinly sliced selection of default search expression supported by this provider
        /// ideally a provider permits runtime speicification of query parameters and predicates
        /// which the LiteDb BsonExpression accomplishes
        /// </summary>
        [TestMethod]
        public void TestEntityReadOperationWithBsonExpressionQuery()
        {
            Exception e = null;

            // define the sql
            var searchExpression = Query.Contains("Description", "Test");

            // define the func
            EntityReadOperation<BsonExpression, List<TestPipelineVariable>> testSqlReadOperation =
                new EntityReadOperation<BsonExpression, List<TestPipelineVariable>>(EntityReadOperationWithSqlQuery);

            // execute the query logic in the caller supplied delegate
            try
            {
                var result = testSqlReadOperation(searchExpression);

                Assert.IsNotNull(result, "failed test with null result");

                Assert.IsTrue(result.Count > 1, "failed test. you probably forgot to insert a record before running this test");
            }
            catch(Exception ee)
            {
                e = ee;
            }

            Assert.IsNull(e, "Failed test with exception " + e?.Message);
        }

        /// <summary>
        /// implementation of user supplied delegate for BsonExpression queryExpression
        /// BsonExpression is sufficiently expressive to support a couple of nice pipeline provider requirements
        ///  viz: 1) fairly type-safe expression of search parameters at runtime as opposed to linq where lamdas are specified at compile time
        /// currently todo - ensure a record is inserted prior to calling this test
        /// </summary>
        /// <param name="queryExpression"></param>
        /// <returns></returns>
        private List<TestPipelineVariable> EntityReadOperationWithSqlQuery(BsonExpression queryExpression)
        {
            List<TestPipelineVariable> ret = new List<TestPipelineVariable>();

            using (var db = new LiteDatabase(this.TestConnectionString))
            {
                // var result = db.GetCollection<TestPipelineVariable>(this.TestCollectionName).FindById(this.TestEntity.ID);
                // ret = result;
                
                var result = db.GetCollection<TestPipelineVariable>(this.TestCollectionName).Find(queryExpression);
                foreach(var item in result)
                {
                    ret.Add(item);
                }

            }

            return ret;
        }

        public TReadOperationResult Read<TReadOperationResult, TSearchExpression>(TSearchExpression searchExpression, EntityReadOperation<TSearchExpression, TReadOperationResult> readOperation)
            where TReadOperationResult : class
            where TSearchExpression : class
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
