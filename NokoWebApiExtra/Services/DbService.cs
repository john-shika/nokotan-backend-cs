using NokoWebApiExtra.Extensions.ApiRepository;
using NokoWebApiExtra.Extensions.ApiService;
using NokoWebApiExtra.Models;

namespace NokoWebApiExtra.Services;

[ApiService]
public class DbService : IApiServiceInitialized
{
    public void OnInitialized(IServiceCollection services)
    {
    }
}
