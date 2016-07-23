using Newtonsoft.Json.Linq;
using static Newtonsoft.Json.JsonConvert;

namespace System
{
    public class JsonObject<T> : IEquatable<JsonObject<T>>, IEquatable<JsonObject>, IEquatable<string>
        where T : class
    {
        private string _originalValue { get; set; }

        public JsonObject() { }

        public JsonObject(T instance)
        {
            Object = instance;
            _originalValue = Json;
        }

        public JsonObject(string json)
        {
            Json = json;
            _originalValue = Json;
        }

        public T Object { get; set; }

        public string Json
        {
            get
            {
                if (Object != null)
                    return SerializeObject(Object);
                else
                    return "";
            }
            set
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(value))
                        Object = default(T);
                    else
                        Object = DeserializeObject<T>(value);
                }
                catch
                {
                    Object = null;
                }
            }
        }

        public override string ToString()
        {
            return Json;
        }

        public override bool Equals(object obj)
        {
            try
            {
                dynamic o = obj;
                return Equals((string)o.Json);
            }
            catch
            {
                return base.Equals(obj);
            }
        }

        public bool Equals(JsonObject<T> other)
        {
            return Equals(other.Json);
        }

        public bool Equals(JsonObject other)
        {
            return Equals(other.Json);
        }

        public bool Equals(string other)
        {
            if (!IsSameType(Json, other))
                return false;
            if (IsObject(Json))
            {
                var o1 = JObject.Parse(Json);
                var o2 = JObject.Parse(other);
                return JToken.DeepEquals(o1, o2);
            }
            else
            {
                var a1 = JArray.Parse(Json);
                var a2 = JArray.Parse(other);
                return JToken.DeepEquals(a1, a2);
            }
        }

        public static implicit operator JsonObject<T>(string json)
        {
            return new JsonObject<T>(json);
        }

        public static implicit operator JsonObject<T>(T obj)
        {
            return new JsonObject<T>(obj);
        }

        public static implicit operator JsonObject<T>(JsonObject obj)
        {
            return new JsonObject<T>(obj.Json);
        }

        public static implicit operator JsonObject<T>(JsonObject<object> obj)
        {
            return new JsonObject<T>(obj.Json);
        }

        private static bool IsObject(string json)
        {
            return json.TrimStart()[0] == '{';
        }

        private static bool IsSameType(string json1, string json2)
        {
            if (IsObject(json1) && IsObject(json2) || !IsObject(json1) && !IsObject(json2))
                return true;
            return false;
        }

        public static bool operator== (JsonObject<T> a, JsonObject<T> b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(JsonObject<T> a, JsonObject<T> b)
        {
            return !a.Equals(b);
        }
    }
}
