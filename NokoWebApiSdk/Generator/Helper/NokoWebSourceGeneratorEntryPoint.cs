using NokoWebApiSdk.Cores;

namespace NokoWebApiSdk.Generator.Helper;

public interface INokoWebSourceGeneratorEntryPoint
{
    void OnInitialized(NokoWebApplication app);
}

public class NokoWebSourceGeneratorEntryPoint : INokoWebSourceGeneratorEntryPoint
{
    public virtual void OnInitialized(NokoWebApplication app)
    {
    }
}
