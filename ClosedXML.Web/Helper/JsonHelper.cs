using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace ClosedXML.Web.Helper
{
    public static class JsonHelper
    {
        public static string ConvertJson(string json, NamingStrategy strategy, Formatting formatting = Formatting.Indented)
        {
            using (StringReader sr = new StringReader(json))
            using (StringWriter sw = new StringWriter())
            {
                ConvertJson(sr, sw, strategy, formatting);
                return sw.ToString();
            }
        }

        public static void ConvertJson(TextReader textReader, TextWriter textWriter, NamingStrategy strategy, Formatting formatting = Formatting.Indented)
        {
            using (JsonReader reader = new JsonTextReader(textReader))
            using (JsonWriter writer = new JsonTextWriter(textWriter))
            {
                writer.Formatting = formatting;
                if (reader.TokenType == JsonToken.None)
                {
                    reader.Read();
                    ConvertJsonValue(reader, writer, strategy);
                }
            }
        }

        private static void ConvertJsonValue(JsonReader reader, JsonWriter writer, NamingStrategy strategy)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                writer.WriteStartObject();
                while (reader.Read() && reader.TokenType != JsonToken.EndObject)
                {
                    string name = strategy.GetPropertyName((string)reader.Value, false);
                    writer.WritePropertyName(name);
                    reader.Read();
                    ConvertJsonValue(reader, writer, strategy);
                }
                writer.WriteEndObject();
            }
            else if (reader.TokenType == JsonToken.StartArray)
            {
                writer.WriteStartArray();
                while (reader.Read() && reader.TokenType != JsonToken.EndArray)
                {
                    ConvertJsonValue(reader, writer, strategy);
                }
                writer.WriteEndArray();
            }
            else if (reader.TokenType == JsonToken.Integer)
            {
                // convert integer values to string
                writer.WriteValue(Convert.ToString((long)reader.Value));
            }
            else if (reader.TokenType == JsonToken.Float)
            {
                // convert floating point values to string
                writer.WriteValue(Convert.ToString((double)reader.Value, System.Globalization.CultureInfo.InvariantCulture));
            }
            else // string, bool, date, etc.
            {
                writer.WriteValue(reader.Value);
            }
        }
    }
    // This naming strategy converts snake case names to upper camel case (a.k.a. proper case)
    public class ProperCaseFromSnakeCaseNamingStrategy : NamingStrategy
    {
        protected override string ResolvePropertyName(string name)
        {
            StringBuilder sb = new StringBuilder(name.Length);
            for (int i = 0; i < name.Length; i++)
            {
                char c = name[i];

                if (i == 0 || name[i - 1] == '_')
                    c = char.ToUpper(c, CultureInfo.InvariantCulture);

                if (c != '_')
                    sb.Append(c);
            }
            return sb.ToString();
        }
    }


    // For whatever reason, .NET Fiddle blows up (Could not load file or assembly) if I include a class which
    // derives from Newtonsoft.Json.Serialization.NamingStrategy, even though it works perfectly fine
    // in my local environment.  For this reason I am faking out Json.Net's base NamingStrategy class here
    // and wrapping Json.Net's SnakeCaseNamingStrategy inside of my own version of the same.  You would not need
    // to do this in production code.  Just pretend this stuff isn't here, and the references to NamingStategy 
    // in the code above are actually references to Newtonsoft.Json.Serialization.NamingStrategy as originally intended.

    public abstract class NamingStrategy
    {
        public string GetPropertyName(string name, bool isSpecified)
        {
            if (isSpecified) return name;
            return ResolvePropertyName(name);
        }
        protected abstract string ResolvePropertyName(string name);
    }

    public class SnakeCaseNamingStrategy : NamingStrategy
    {
        private static readonly Newtonsoft.Json.Serialization.SnakeCaseNamingStrategy innerStrategy = new Newtonsoft.Json.Serialization.SnakeCaseNamingStrategy();

        protected override string ResolvePropertyName(string name)
        {
            return innerStrategy.GetPropertyName(name, false);
        }
    }
}
