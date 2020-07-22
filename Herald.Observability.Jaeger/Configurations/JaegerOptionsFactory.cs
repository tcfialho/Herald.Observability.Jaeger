using Microsoft.Extensions.Configuration;

using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Herald.Observability.Jaeger.Tests")]
namespace Herald.Observability.Jaeger.Configurations
{
    internal static class JaegerOptionsFactory
    {
        public static JaegerOptions Create(IConfiguration configuration)
        {
            var jaegerOptions = new JaegerOptions();

            foreach (var propertyInfo in jaegerOptions.GetType().GetProperties())
            {
                var configValue = configuration[$"JAEGER_{propertyInfo.Name.ToUpperSnakeCase()}"];
                var defaultValue = propertyInfo.GetValue(jaegerOptions);

                propertyInfo.SetValue(jaegerOptions, configValue ?? defaultValue, null);
            }

            return jaegerOptions;
        }

        public static JaegerOptions Create()
        {
            var jaegerOptions = new JaegerOptions();

            foreach (var propertyInfo in jaegerOptions.GetType().GetProperties())
            {

                var configValue = Environment.GetEnvironmentVariable($"JAEGER_{propertyInfo.Name.ToUpperSnakeCase()}");
                var defaultValue = propertyInfo.GetValue(jaegerOptions);

                propertyInfo.SetValue(jaegerOptions, configValue ?? defaultValue, null);
            }

            return jaegerOptions;
        }
    }
}
