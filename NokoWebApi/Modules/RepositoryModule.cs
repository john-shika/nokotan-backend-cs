using Microsoft.EntityFrameworkCore;
using NokoWebApi.Repositories;
using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Extensions.ApiModule;

namespace NokoWebApi.Modules;

[ApiModule]
public class RepositoryModule : IApiModuleInitialized
{
    public void OnInitialized(IServiceCollection services)
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
