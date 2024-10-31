using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Generator.Helper;

namespace NokoWebApiSdk.Generator.Extensions;

public static class NokoWebSourceGeneratorEndpointBuilderExtensions
{
    public static void EntryPoint<TEntryPoint>(this NokoWebApplication nokoWebApplication) 
        where TEntryPoint : class, INokoWebSourceGeneratorEntryPoint, new()
    {
        var entryPoint = new TEntryPoint();
        var action = (NokoWebApplication application) =>
        { 
            entryPoint.OnInitialized(application);
        };
        
        nokoWebApplication.Listen(action.Listener);
    }
}