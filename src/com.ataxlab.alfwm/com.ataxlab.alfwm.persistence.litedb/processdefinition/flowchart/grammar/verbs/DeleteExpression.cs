﻿using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.persistence.litedb.processdefinition.flowchart.grammar.verbs
{
    public class DeleteExpression<TEntity> where  TEntity : class
    {


        public DeleteExpression() { }


        public DeleteExpression(TEntity entity) 
        {
            TargetEntityType = entity;
        }


        public TEntity TargetEntityType { get; set; }

        public string CollectionName { get; set; }

        public BsonExpression DeleteCriteria {get; set;}
    }
}
