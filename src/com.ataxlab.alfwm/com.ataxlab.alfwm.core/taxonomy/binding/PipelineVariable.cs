using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace com.ataxlab.alfwm.core.taxonomy.binding
{

    /// <summary>
    /// support a weakly typed hierarchal tuple 
    /// the tuple payload is independent from
    /// it's parent type
    /// and it's item collection type
    /// </summary>
    [XmlType("PipelineVariableEntity")]
    public class PipelineVariable : IPipelineVariable
    {
        public PipelineVariable()
        {

        }


        public PipelineVariable(object payload)
        {
            this.Payload = payload;

            // doing this may cause problems serializing containing objects to xml
            // TODO look into wire format handling for pipelinevariables,
            // possibly by moving wireformat handling out of this class
            this.JsonValue = JsonConvert.SerializeObject(payload, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            // TODO - justify the schema 
            // given the inclusion of type information in the render JSON
            this.JsonValueSchema = JSchema.Parse(JsonValue);

        }


        [XmlAttribute]
        public string ID { get;  set; }

        [XmlAttribute]
        public string Key { get;  set; }

        [XmlElement]
 
        public string JsonValue { get; set; }

        [XmlIgnoreAttribute]
        public JSchema JsonValueSchema { get; set; }

        [XmlAttribute]
        public DateTime CreateDate { get; set; }

        [XmlAttribute]
        public DateTime TimeStamp { get; set; }

        [XmlAttribute]
        public string DisplayName { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        [XmlIgnore]
        public virtual object ParentItem { get; set; }

        [XmlIgnore]
        public virtual ICollection<object> Items { get; set; }


        [XmlIgnore]
        public virtual object Payload { get; set; }

    }

    /// <summary>
    /// support a tuple with generic semantics
    /// </summary>
    /// <typeparam name="TPayload"></typeparam>
    /// <typeparam name="TPayloadCollection"></typeparam>
    public class PipelineVariable<TPayload> : IPipelineVariable<TPayload>
        where TPayload : class
    {
        public PipelineVariable()
        {
            this.ID = Guid.NewGuid().ToString();
        }

        public PipelineVariable(TPayload payload, bool isGenerateJSONValue = false, bool isGenerateJSONSchema = false)
        {
            this.ID = Guid.NewGuid().ToString();
            this.Payload = payload;
            this.JsonValue = JsonConvert.SerializeObject(payload);

            this.JsonValueSchema = JSchema.Parse(JsonValue);
        }

 
        public string ID { get; set; }
        public string Key { get; set; }
        public string JsonValue { get; set; }
        public virtual TPayload Payload { get; set; }
        public JSchema JsonValueSchema { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime CreateDate { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// support a hierarchal weakly typed tuple type system
    /// with independent types for payload
    /// parent item
    /// and items collections
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TItems"></typeparam>
    /// <typeparam name="TParent"></typeparam>
    public class PipelineVariable<T, TItems, TParent> : IPipelineVariable<T, TItems, TParent> 
        where T : class where TItems : class where TParent : class
    {
        /// <summary>
        /// calling this constructor definitely leaves
        /// the PipelineVariable unitialized
        /// </summary>
        public PipelineVariable()
        {

        }

        /// <summary>
        /// initialize the json type system 
        /// with the supplied payload
        /// </summary>
        /// <param name="payload"></param>
        public PipelineVariable(T payload)
        {
            Payload = payload;

            this.Payload = payload;

            this.JsonValue = JsonConvert.SerializeObject(payload);

            this.JsonValueSchema = JSchema.Parse(JsonValue);

        }

        private T _payload;
       
        /// <summary>
        /// side effect of setter is 
        /// generation of JsonValue and JSonValueSchema
        /// </summary>
        public T Payload 
        { 
            get
            {
                return _payload;
            }
            set
            {
                _payload = value;

                if (value != null)
                {
                    // initialize the json type system with the supplied value
                    try
                    {
                        this.JsonValue = JsonConvert.SerializeObject(value);

                        this.JsonValueSchema = JSchema.Parse(this.JsonValue);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public string ID { get; set; }
        public string Key { get; set; }
        public string JsonValue { get; set; }
        public JSchema JsonValueSchema { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime CreateDate { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public TParent ParentItem { get; set; }

        /// <summary>
        /// use List<object> for heterogenous collections
        /// </summary>
        public TItems Items { get; set; }
    }
}

