using NokoWebApiExtra.Extensions.ApiService;
using Serilog;
using Serilog.Sinks.OpenTelemetry;

namespace NokoWebApi.Services;

[ApiService]
public class LogService : IApiServiceInitialized
{
    public void OnInitialized(IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.OpenTelemetry(x =>
            {
                x.Endpoint = "http://localhost:5341/ingest/otlp/v1/logs";
                x.Protocol = OtlpProtocol.HttpProtobuf;
                x.Headers = new Dictionary<string, string>
                {
                    ["X-Seq-ApiKey"] = "WBPq4wjBhGll1QlL9m6r"
                };
                x.ResourceAttributes = new Dictionary<string, object>
                {
                    ["service.name"] = "Noko.WebApi"
                };
            })
            .CreateLogger();

        services.AddSerilog();
    }
}