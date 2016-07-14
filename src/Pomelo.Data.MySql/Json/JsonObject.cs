using static Newtonsoft.Json.JsonConvert;

namespace System
{
    public class JsonObject : JsonObject<object>
    {
        public JsonObject(string json)
            : base(json)
        {
        }
    }
}
