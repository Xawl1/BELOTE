using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace DEMO2_ASP.Extensions
{
    public static class SessionExtensions
    {
        // Пази обект във сесията като JSON
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // Връща обект от сесията, ако съществува, или null
        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return string.IsNullOrEmpty(value) ? default : JsonSerializer.Deserialize<T>(value);
        }       

    }
}