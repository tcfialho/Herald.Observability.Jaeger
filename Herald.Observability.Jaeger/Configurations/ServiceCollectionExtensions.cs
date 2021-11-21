using Jaeger;
using Jaeger.Samplers;
using Jaeger.Senders;
using Jaeger.Senders.Thrift;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using OpenTracing.Contrib.NetCore.Configuration;
using OpenTracing.Util;

using System;

using static Jaeger.Configuration;

namespace Herald.Observability.Jaeger.Configurations
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJaegerTracing(this IServiceCollection services)
        {
            var jaegerOptions = JaegerOptionsFactory.Create();

            return services.AddJaegerTracing(jaegerOptions);
        }

        public static IServiceCollection AddJaegerTracing(this IServiceCollection services, IConfiguration configuration)
        {
            var jaegerOptions = JaegerOptionsFactory.Create(configuration);

            return services.AddJaegerTracing(jaegerOptions);
        }

        public static IServiceCollection AddJaegerTracing(this IServiceCollection services, JaegerOptions options)
        {
            return services.AddJaegerTracing(configure =>
            {
                options.CopyTo(configure);
            });
        }

        public static IServiceCollection AddJaegerTracing(this IServiceCollection services, Action<JaegerOptions> configure)
        {
            services.Configure(configure);

            var options = new JaegerOptions();

            configure?.Invoke(options);
            services.AddOpenTracing();

            // Add ITracer
            services.AddSingleton(serviceProvider =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

                Configuration.SenderConfiguration.DefaultSenderResolver =
                    new SenderResolver(loggerFactory).RegisterSenderFactory<ThriftSenderFactory>();

                var tracerConfig = GetTracerConfig(options, loggerFactory);

                // Tracer
                var tracer = tracerConfig.GetTracer();

                if (!GlobalTracer.IsRegistered())
                {
                    GlobalTracer.Register(tracer);
                }

                return tracer;
            });

            services.Configure<HttpHandlerDiagnosticOptions>(o =>
            {
                o.IgnorePatterns.Add(x => !x.RequestUri.IsLoopback);
            });

            return services;
        }

        private static Configuration GetTracerConfig(JaegerOptions options, ILoggerFactory loggerFactory)
        {
            // Sender
            var senderConfig = new Configuration.SenderConfiguration(loggerFactory);
            senderConfig.WithAgentHost(options.AgentHost)
                        .WithAgentPort(options.AgentPort)
                        .WithEndpoint(options.Endpoint);

            if (!string.IsNullOrEmpty(options.User))
            {
                senderConfig.WithAuthUsername(options.User)
                            .WithAuthPassword(options.Password);
            }
            if (!string.IsNullOrEmpty(options.AuthToken))
            {
                senderConfig.WithAuthToken(options.AuthToken);
            }

            // Sampler
            var samplerConfig = new Configuration.SamplerConfiguration(loggerFactory)
                        .WithSamplingEndpoint(options.SamplingEndpoint)
                        .WithType(ConstSampler.Type);

            // Reporter
            var reporterConfig = new Configuration.ReporterConfiguration(loggerFactory);
            reporterConfig.WithSender(senderConfig);

            var codecConfiguration = new Configuration.CodecConfiguration(loggerFactory)
                .WithPropagation(Propagation.Jaeger)
                .WithPropagation(Propagation.B3);

            // Configuration
            var tracerConfig = new Configuration(options.ServiceName, loggerFactory)
                    .WithSampler(samplerConfig)
                    .WithReporter(reporterConfig)
                    .WithCodec(codecConfiguration);

            return tracerConfig;
        }
    }
}