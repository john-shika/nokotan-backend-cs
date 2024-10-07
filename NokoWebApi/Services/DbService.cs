using Microsoft.EntityFrameworkCore;
using NokoWebApi.Repositories;
using NokoWebApiExtra.Extensions.ApiService;

namespace NokoWebApi.Services;

[ApiService]
public class DbService : IApiServiceInitialized
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
