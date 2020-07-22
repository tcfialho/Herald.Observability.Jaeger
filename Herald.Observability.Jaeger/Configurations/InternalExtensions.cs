using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Herald.Observability.Jaeger.Tests")]
namespace Herald.Observability.Jaeger
{
    internal static class InternalExtensions
    {
        public static void CopyTo<T>(this T from, T to) where T : class
        {
            foreach (var toPropInfo in typeof(T).GetProperties())
            {
                var fromValue = toPropInfo.GetValue(from);
                toPropInfo.SetValue(to, fromValue, null);
            }
        }

        public static string ToUpperSnakeCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToUpper();
        }
    }
}
