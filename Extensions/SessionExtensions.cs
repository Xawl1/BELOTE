using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace DEMO2_ASP.Extensions
{
    public static class SessionExtensions
    {
        // Stores an object in session as JSON
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // Retrieves an object from session
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}