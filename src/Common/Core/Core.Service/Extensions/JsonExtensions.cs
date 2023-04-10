using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Core.Service.Extensions
{
    public static class JsonExtensions
    {

        public static JsonSerializerSettings AsDefault(this JsonSerializerSettings settings)
        {
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver
            {
                NamingStrategy = { ProcessDictionaryKeys = false }
            };

            settings.Converters.Add(new StringEnumConverter()); // serialize enums as strings

            settings.NullValueHandling = NullValueHandling.Ignore; // do not serialize null values
            settings.DefaultValueHandling =
                            DefaultValueHandling
                                            .Include; // always include default values (especially needed when sending enums to the outside world, e.g. in webhooks)
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc; // always deal with dates in UTC manner
            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            settings.DateParseHandling = DateParseHandling.DateTime;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.Formatting = Formatting.Indented;

            return settings;
        }


    }
}
