using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Extensions.AppService;

namespace NokoWebApi.Services;

[AppService]
public class RepositoryService : AppServiceInitialized
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
