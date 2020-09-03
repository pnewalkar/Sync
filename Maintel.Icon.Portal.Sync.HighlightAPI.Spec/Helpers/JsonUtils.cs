using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Maintel.Icon.Portal.Sync.HighlightAPI.Spec.Helpers
{
    public static class JsonUtils {
        public static bool TryParseJson<T>(string data)
        {
            bool success = true;
            var settings = new JsonSerializerSettings
            {
                Error = (sender, args) => { success = false; args.ErrorContext.Handled = true; },
                MissingMemberHandling = MissingMemberHandling.Error
            };
            object result = new {};
            result = JsonConvert.DeserializeObject<T>(data, settings);
            return success;
        }

        public static object GetObject<T>(string data) {
            var settings = new JsonSerializerSettings
            {
                Error = (sender, args) => { args.ErrorContext.Handled = true; },
                MissingMemberHandling = MissingMemberHandling.Error
            };
            object result = new {};
            result = JsonConvert.DeserializeObject<T>(data, settings);

            return result;
        }
        
        public static List<object> GetObjectArray<T>(string data) {
            var result = new List<object>();
            var settings = new JsonSerializerSettings
            {
                Error = (sender, args) => { args.ErrorContext.Handled = true; },
                MissingMemberHandling = MissingMemberHandling.Error
            };
            var rtn = JsonConvert.DeserializeObject<List<T>>(data, settings);
            result = rtn.Cast<object>().ToList();
            
            return result;
        }
    }
}