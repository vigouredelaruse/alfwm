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
        public LiteDbFlowchartDataSetProviderConfiguration(ConnectionString connectionString, LiteDB.BsonExpression indexExpression, string collectionName = "", string indexName = "",  bool isMustEnsureEsists = false)
        {
            try
            {
                this.DatabaseFilePath = new FileInfo(connectionString.Filename);

                if(isMustEnsureEsists)
                {
                    if(!this.DatabaseFilePath.Exists)
                    {
                        // initialize the database
                        using (var db = new LiteDatabase(connectionString))
                        {
                            if (!db.CollectionExists(collectionName))
                            {
                                var validCollection = db.GetCollection(collectionName);
                                if (indexExpression != null && indexName != String.Empty)
                                {
                                    // initialize index with supplied 
                                    // index expression and index name
                                    validCollection.EnsureIndex(indexName, indexExpression);
                                }
                            }



                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw new LiteDbFlowchartDataSetProviderConfigurationException(ex.Message);
            }
        }

        public FileInfo DatabaseFilePath { get; set; }
    }
}
