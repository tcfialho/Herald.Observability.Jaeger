using Microsoft.Extensions.PlatformAbstractions;

namespace Herald.Observability.Jaeger.Configurations
{
    /// <summary>
    ///     Config for the Jaeger Client, see more https://github.com/jaegertracing/jaeger-client-csharp
    /// </summary>
    public class JaegerOptions
    {
        /// <summary>
        /// The service name.
        /// </summary>
        public string ServiceName { get; set; } = PlatformServices.Default.Application.ApplicationName;

        /// <summary>
        /// The hostname for communicating with agent via UDP.
        /// </summary>
        public int AgentPort { get; set; } = 6831;

        /// <summary>
        /// The port for communicating with agent via UDP.
        /// </summary>
        public string AgentHost { get; set; } = "localhost";

        /// <summary>
        /// The url for the remote sampling conf when using sampler type remote.
        /// </summary>
        public string SamplingEndpoint { get; set; } = $"http://localhost:5778";

        /// <summary>
        /// The traces endpoint, in case the client should connect directly to the collector.
        /// </summary>
        public string Endpoint { get; set; } = "http://localhost:14268/api/traces";

        /// <summary>
        /// Username to send as part of "Basic" authentication to the endpoint.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Password to send as part of "Basic" authentication to the endpoint.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Authentication Token to send as "Bearer" to the endpoint.
        /// </summary>
        public string AuthToken { get; set; }

        public JaegerOptions()
        {

        }
    }
}