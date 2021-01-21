using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace com.ataxlab.core.alfwm.utility.extension
{
    /// <summary>
    /// as per https://stackoverflow.com/questions/2434534/serialize-an-object-to-string
    /// </summary>
    public static class SerializationExtension
    {
        public static string SerializeObject<T>(this T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        /// <summary>
        /// as per https://stackoverflow.com/questions/2347642/deserialize-from-string-instead-textreader
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toDeSerialize"></param>
        /// <returns></returns>
        public static T DeSerializeObject<T>(this string toDeSerialize)
        {
            return (T)toDeSerialize.XmlDeserializeFromString(typeof(T));
        }

        public static object XmlDeserializeFromString(this string objectData, Type type)
        {
            var serializer = new XmlSerializer(type);
            object result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }


        /// <summary>
        /// automapper ignore all nonmapped fields 
        /// as per
        /// Mapper.CreateMap<SourceType, DestinationType>()
        ///        .IgnoreAllNonExisting();
        /// https://stackoverflow.com/questions/954480/automapper-ignore-the-rest
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>
            (this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(flags);

            foreach (var property in destinationProperties)
            {
                if (sourceType.GetProperty(property.Name, flags) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;
        }
    }
}
