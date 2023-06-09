using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Codecool.CodecoolShop.Helpers
{
    public static class SessionHelper
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            session.SetString(key, serializedValue);
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var serializedValue = session.GetString(key);
            if (serializedValue == null)
                return default(T);

            var deserializedValue = JsonConvert.DeserializeObject<T>(serializedValue);
            return deserializedValue;
        }
    }
}