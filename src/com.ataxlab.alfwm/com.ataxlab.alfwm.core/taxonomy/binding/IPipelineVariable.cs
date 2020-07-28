using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.binding
{
    /// <summary>
    /// designed for convenience key/value collection lookup
    /// 
    /// takes a dependency on JSON.NET for better or worse
    /// </summary>
    public interface IPipelineVariable
    {
        /// <summary>
        /// support lookup by composite key ID.Key
        /// </summary>
        string ID { get; set; }

        /// <summary>
        /// variable lookup key
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// dynamic typing is solved by 
        /// json and json/schema
        /// </summary>
        string JsonValue { get; set; }

        /// <summary>
        /// support strongly typed casting 
        /// </summary>
        object Payload { get; set; }

        /// <summary>
        /// clients will use the schema to infer the type of the value
        /// which is expressed as a json value
        /// </summary>
        JSchema JsonValueSchema { get; set; }
        DateTime TimeStamp { get; set; }
        DateTime CreateDate { get; set; }
        string DisplayName { get; set; }
        string Description { get; set; }
        object ParentItem { get; set; }
        ICollection<object> Items { get; set; }
    }

    /// <summary>
    /// support generic semantics for the payload
    /// </summary>
    /// <typeparam name="TPayload"></typeparam>
    public interface IPipelineVariable<TPayload>
        where TPayload : class
    {
        /// support lookup by composite key ID.Key
        /// </summary>
        string ID { get; set; }

        /// <summary>
        /// variable lookup key
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// dynamic typing is solved by 
        /// json and json/schema
        /// </summary>
        string JsonValue { get; set; }

        /// <summary>
        /// support strongly typed casting 
        /// </summary>
        TPayload Payload { get; set; }

        /// <summary>
        /// clients will use the schema to infer the type of the value
        /// which is expressed as a json value
        /// </summary>
        JSchema JsonValueSchema { get; set; }
        DateTime TimeStamp { get; set; }
        DateTime CreateDate { get; set; }
        string DisplayName { get; set; }
        string Description { get; set; }
    }

    /// <summary>
    /// support generic semantics and a hierarchal tuple
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPipelineVariable<T, TItems, TParent>  where T : class where TItems : class where TParent : class
    {
        /// <summary>
        /// support lookup by composite key ID.Key
        /// </summary>
        string ID { get; set; }

        /// <summary>
        /// variable lookup key
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// dynamic typing is solved by 
        /// json and json/schema
        /// </summary>
        string JsonValue { get; set; }

        /// <summary>
        /// support strongly typed casting 
        /// </summary>
        T Payload { get; set; }

        /// <summary>
        /// clients will use the schema to infer the type of the value
        /// which is expressed as a json value
        /// </summary>
        JSchema JsonValueSchema { get; set; }
        DateTime TimeStamp { get; set; }
        DateTime CreateDate { get; set; }
        string DisplayName { get; set; }
        string Description { get; set; }

        TParent ParentItem { get; set; }

        TItems Items { get; set; }

    }
}
