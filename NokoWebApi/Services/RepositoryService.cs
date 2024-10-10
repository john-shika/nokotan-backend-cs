using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Extensions.ApiService;

namespace NokoWebApi.Services;

[ApiService]
public class RepositoryService : ApiServiceInitialized
{
    public override void OnInitialized(IServiceCollection services, IConfiguration configuration)
    {
        // services.AddDbContext<UserRepository>((options) =>
        // {
        //     options.UseSqlite("Data Source=Migrations/dev.db");
        // });
        // services.AddDbContext<SessionRepository>((options) =>
        // {
        //     options.UseSqlite("Data Source=Migrations/dev.db");
        // });
    }
}
