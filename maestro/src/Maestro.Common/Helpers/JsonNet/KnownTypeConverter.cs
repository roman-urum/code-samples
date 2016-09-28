using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Maestro.Common.Helpers.JsonNet
{
    /// <summary>
    /// Use KnownType Attribute to match a divierd class based on the class given to the serilaizer
    /// Selected class will be the first class to match all properties in the json object.
    /// </summary>
    public class KnownTypeConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return Attribute.GetCustomAttributes(objectType).Any(v => v is KnownTypeAttribute);
        }

        /// <summary>
        /// Gets a value indicating whether this instance can write.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can write; otherwise, <c>false</c>.
        /// </value>
        public override bool CanWrite
        {
            get { return false; }
        }

        /// <summary>
        /// Reads the json.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns></returns>
        /// <exception cref="System.Runtime.Serialization.SerializationException"></exception>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            Attribute[] attrs = Attribute.GetCustomAttributes(objectType);  // Reflection. 

            // Сheck known types for a match
            foreach (var attr in attrs.OfType<KnownTypeAttribute>())
            {
                object target = Activator.CreateInstance(attr.Type);

                JObject jTest;

                using (var writer = new StringWriter())
                {
                    using (var jsonWriter = new JsonTextWriter(writer))
                    {
                        serializer.Serialize(jsonWriter, target);
                        string json = writer.ToString();
                        jTest = JObject.Parse(json);
                    }
                }

                var jO = this.GetKeys(jObject).Select(k => k.Key).ToList();
                var jT = this.GetKeys(jTest).Select(k => k.Key).ToList();

                if (jO.Count == jT.Count && jO.Intersect(jT).Count() == jO.Count)
                {
                    serializer.Populate(jObject.CreateReader(), target);

                    return target;
                }
            }

            throw new SerializationException(string.Format("Could not convert base class {0}", objectType));
        }

        /// <summary>
        /// Writes the json.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The serializer.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException("KnownTypeConverter should only be used while deserializing.");
        }

        /// <summary>
        /// Gets the keys.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        private IEnumerable<KeyValuePair<string, JToken>> GetKeys(JObject obj)
        {
            var list = new List<KeyValuePair<string, JToken>>();

            foreach (var t in obj)
            {
                list.Add(t);
            }

            return list;
        }
    }
}