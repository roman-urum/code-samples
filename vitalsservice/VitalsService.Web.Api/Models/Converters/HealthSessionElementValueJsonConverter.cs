﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Models.HealthSessions;

namespace VitalsService.Web.Api.Models.Converters
{
    /// <summary>
    /// Converter to select required type of element value during serialization.
    /// </summary>
    public class HealthSessionElementValueJsonConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            HealthSessionElementValueType? type = this.ReadType(jsonObject);

            HealthSessionElementValueDto model;

            switch (type)
            {
                case HealthSessionElementValueType.SelectionAnswer:
                    model = new SelectionAnswerDto();
                    break;
                case HealthSessionElementValueType.ScaleAnswer:
                    model = new ScaleAnswerDto();
                    break;
                case HealthSessionElementValueType.OpenEndedAnswer:
                    model = new FreeFormAnswerDto();
                    break;
                case HealthSessionElementValueType.MeasurementAnswer:
                    model = new MeasurementValueRequestDto();
                    break;
                case HealthSessionElementValueType.StethoscopeAnswer:
                    model = new AssessmentValueRequestDto();
                    break;
                default:
                    model = new HealthSessionElementValueDto();
                    break;
            }

            serializer.Populate(jsonObject.CreateReader(), model);

            return model;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON.
        /// </summary>
        /// <value>
        /// <c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON; otherwise, <c>false</c>.
        /// </value>
        public override bool CanWrite
        {
            get { return false; }
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (HealthSessionElementValueDto);
        }

        /// <summary>
        /// Reads type from json object with health element value.
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        private HealthSessionElementValueType? ReadType(JObject jsonObject)
        {
            JToken jsonToken = jsonObject.GetValue("Type", StringComparison.InvariantCultureIgnoreCase);

            if (jsonToken == null)
            {
                return null;
            }

            HealthSessionElementValueType type;
            var typeString = jsonToken.Value<string>();

            if (Enum.TryParse(typeString, out type))
            {
                return type;
            }

            return null;
        }
    }
}