using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.uwp.mstests.datasetprovider.litedb.model
{
    public class UpdateExpression
    {
        public UpdateExpression() { }

        public BsonExpression UpdateCriteria { get; set; }
        public string CollectionName { get; internal set; }
    }

    public class UpdateExpression<TUpdatedEntity> : UpdateExpression
        where TUpdatedEntity : class
    {
        public UpdateExpression(TUpdatedEntity updatedEntity)
        {
            this.UpdatedEntity = updatedEntity;
        }

        public TUpdatedEntity UpdatedEntity { get; set; }
    }
}
