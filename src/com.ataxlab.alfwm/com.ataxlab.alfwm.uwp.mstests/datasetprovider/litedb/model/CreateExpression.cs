﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.uwp.mstests.datasetprovider.litedb.model
{
    public class CreateExpression<TEntity> where TEntity : class, new()
    {
        public CreateExpression() { }

        public string CollectionName { get; set; }

        public TEntity NewEntity { get; set; }
    }
}
