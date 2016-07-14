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

        public bool Equals(JsonObject<T> other)
        {
            if (this.Json == other.Json)
                return true;
            return false;
        }

        public bool Equals(JsonObject other)
        {
            if (this.Json == other.Json)
                return true;
            return false;
        }

        public bool Equals(string other)
        {
            return Json == other;
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
    }
}
