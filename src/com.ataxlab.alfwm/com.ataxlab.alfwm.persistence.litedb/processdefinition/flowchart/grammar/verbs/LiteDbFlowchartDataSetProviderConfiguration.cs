using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace com.ataxlab.alfwm.persistence.litedb.processdefinition.flowchart.grammar.verbs
{
    public class LiteDbFlowchartDataSetProviderConfiguration
    {
        public LiteDbFlowchartDataSetProviderConfiguration() { }

        /// <summary>
        /// throws exception on invalid file path
        /// </summary>
        /// <param name="connectionString"></param>
        public LiteDbFlowchartDataSetProviderConfiguration( ConnectionString connectionString, 
                                                            LiteDB.BsonExpression indexExpression, 
                                                            string collectionName = "", 
                                                            string indexName = "",  
                                                            bool isMustEnsureEsists = false)
        {
            this.ConnectionString = connectionString;

            this.CollectionName = collectionName;

            this.IndexName = indexName;

            this.IndexExpression = indexExpression;

            this.IsMustEnsureExists = isMustEnsureEsists;

        }

        public FileInfo DatabaseFilePath { get; set; }
        public ConnectionString ConnectionString { get; private set; }
        public string CollectionName { get; private set; }
        public string IndexName { get; private set; }
        public BsonExpression IndexExpression { get; private set; }
        public bool IsMustEnsureExists { get; private set; }
    }
}
