using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Jenkins.Model
{
    public class BuildsActionsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(BuildActions);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var actions = new BuildActions();

            var array = (JArray)JContainer.ReadFrom(reader);

            foreach (JProperty item in array.Where(x => x.HasValues && x.First != null).Select(x => x.First))
            {
                actions[item.Name] = serializer.Deserialize(item.Value.CreateReader());
            }

            return actions;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
