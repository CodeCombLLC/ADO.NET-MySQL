using static Newtonsoft.Json.JsonConvert;

namespace System
{
    public class JsonObject<T>
    {
        public JsonObject() { }

        public JsonObject(T instance)
        {
            Object = instance;
        }

        public JsonObject(string json)
        {
            Json = json;
        }

        public T Object { get; set; }

        public string Json
        {
            get
            {
                return SerializeObject(Object);
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    Object = default(T);
                else
                    Object = DeserializeObject<T>(value);
            }
        }

        public override string ToString()
        {
            return Json;
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
