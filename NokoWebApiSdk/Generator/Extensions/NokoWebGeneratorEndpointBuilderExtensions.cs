using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Generator.Helper;

namespace NokoWebApiSdk.Generator.Extensions;

public static class NokoWebGeneratorEndpointBuilderExtensions
{
    public static void EntryPoint<TEntryPoint>(this NokoWebApplication nokoWebApplication) 
        where TEntryPoint : class, INokoWebGeneratorHelperEntryPoint, new()
    {
        var entryPoint = new TEntryPoint();
        var action = (NokoWebApplication application) =>
        { 
            entryPoint.OnInitialized(application);
        };
        
        nokoWebApplication.Listen(action.Listener);
    }
}