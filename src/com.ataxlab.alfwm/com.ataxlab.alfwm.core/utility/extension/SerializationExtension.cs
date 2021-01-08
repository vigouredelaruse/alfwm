using System;
using System.Collections.Generic;
using System.IO;
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
    }
}
