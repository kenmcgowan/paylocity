﻿using Newtonsoft.Json;
using System;

namespace Paylocity.Benefits.Registration.Api.Models
{
    /// <summary>
    /// Provides the means to convert decimal fields to strings when converting to JSON. Does
    /// not support converting strings back to decimal during deserialization.
    /// </summary>
    public class DecimalToStringJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(string.Format("{0:F2}", value));
        }
    }
}
