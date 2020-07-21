# Herald.Observability.Jaeger

![Status](https://github.com/tcfialho/Herald.Observability.Jaeger/workflows/Herald.Observability.Jaeger/badge.svg) ![Coverage](https://codecov.io/gh/tcfialho/Herald.Observability.Jaeger/branch/master/graph/badge.svg) ![NuGet](https://buildstats.info/nuget/Herald.Observability.Jaeger)

## Overview
 - Support add trace to the [Jaeger](https://github.com/jaegertracing/jaeger-client-csharp) automatically.

## Installation
 - Package Manager
    ```
    Install-Package Herald.Observability.Jaeger
    ```
 - .NET CLI
    ```
    dotnet add package Herald.Observability.Jaeger
    ```

See more information in [Nuget](https://www.nuget.org/packages/Herald.Observability.Jaeger/).

## Usage
 - Configure service and middleware in Startup.cs
    ```c#
    using Herald.Observability.Jaeger

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //...
            services.AddJaegerTracing();
            //...
        }
    }
    ```

## Credits

Author [**Thiago Fialho**](https://br.linkedin.com/in/thiago-fialho-139ab116)

## License

Herald.Observability.Jaeger is licensed under the [MIT License](LICENSE).