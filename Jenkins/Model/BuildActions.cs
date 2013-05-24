using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Jenkins.Model
{
    [JsonConverter(typeof(BuildsActionsConverter))]
    public class BuildActions : Dictionary<string, dynamic>
    {
        public T GetAction<T>(string key)
        {
            return ((JToken)this[key]).ToObject<T>();
        }
    }
}
