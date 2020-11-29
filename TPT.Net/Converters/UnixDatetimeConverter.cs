using Newtonsoft.Json;
using System;

namespace TPT
{
    internal class UnixTimestampConverter : JsonConverter<DateTime>
    {
        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            long time;

            if (reader.Value.GetType() == typeof(string))
            {
                time = Convert.ToInt64(reader.Value);
            }
            else if (reader.Value.GetType() == typeof(int))
            {
                time = (long)reader.Value;
            }
            else if (reader.Value.GetType() == typeof(long))
            {
                time = (long)reader.Value;
            }
            else
            {
                throw new JsonReaderException($"Could not parse {reader.Value} typeof {reader.Value.GetType()}");
            }

            return Extensions.FromUnixtime(time);
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanWrite is false. The type will skip the converter.");
        }

        public override bool CanWrite => false;
    }
}