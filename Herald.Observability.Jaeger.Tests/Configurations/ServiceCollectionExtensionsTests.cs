
using Herald.Observability.Jaeger.Configurations;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Moq;

using OpenTracing;

using Xunit;

namespace Herald.Observability.Jaeger.Tests.Configurations
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void ShouldAddJaegerTracing()
        {
            //Arrange
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging()
                             .AddJaegerTracing();

            //Act
            var tracer = serviceCollection.BuildServiceProvider().GetService<ITracer>();

            //Assert
            Assert.NotNull(tracer);
        }

        [Fact]
        public void ShouldAddJaegerTracingWithIConfiguration()
        {
            //Arrange
            var serviceCollection = new ServiceCollection();
            var mockConfiguration = new Mock<IConfiguration>();

            serviceCollection.AddLogging()
                             .AddJaegerTracing(mockConfiguration.Object);

            //Act
            var tracer = serviceCollection.BuildServiceProvider().GetService<ITracer>();

            //Assert
            Assert.NotNull(tracer);
        }

        [Fact]
        public void ShouldAddJaegerTracingWithJaegerOptions()
        {
            //Arrange
            var serviceCollection = new ServiceCollection();
            var jaegerOptions = new JaegerOptions();

            serviceCollection.AddLogging()
                             .AddJaegerTracing(jaegerOptions);

            //Act
            var tracer = serviceCollection.BuildServiceProvider().GetService<ITracer>();

            //Assert
            Assert.NotNull(tracer);
        }

        [Fact]
        public void ShouldAddJaegerTracingWithAction()
        {
            //Arrange
            var serviceCollection = new ServiceCollection();
            var jaegerOptions = new JaegerOptions();

            serviceCollection.AddLogging()
                             .AddJaegerTracing(configure =>
                             {
                                 configure.ServiceName = jaegerOptions.ServiceName;
                             });

            //Act
            var tracer = serviceCollection.BuildServiceProvider().GetService<ITracer>();

            //Assert
            Assert.NotNull(tracer);
        }

        [Fact]
        public void ShouldAddJaegerTracingWithUsernameAndPassord()
        {
            //Arrange
            var serviceCollection = new ServiceCollection();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.SetupGet(x => x["JAEGER_USER"]).Returns("user");
            mockConfiguration.SetupGet(x => x["JAEGER_PASSWORD"]).Returns("password");

            serviceCollection.AddLogging()
                             .AddJaegerTracing(mockConfiguration.Object);

            //Act
            var tracer = serviceCollection.BuildServiceProvider().GetService<ITracer>();

            //Assert
            Assert.NotNull(tracer);
        }

        [Fact]
        public void ShouldAddJaegerTracingWithAuthToken()
        {
            //Arrange
            var serviceCollection = new ServiceCollection();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.SetupGet(x => x["JAEGER_AUTH_TOKEN"]).Returns("ey...");

            serviceCollection.AddLogging()
                             .AddJaegerTracing(mockConfiguration.Object);

            //Act
            var tracer = serviceCollection.BuildServiceProvider().GetService<ITracer>();

            //Assert
            Assert.NotNull(tracer);
        }
    }
}
