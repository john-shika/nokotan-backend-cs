using NokoWebApiSdk.Cores;

namespace NokoWebApiSdk.Generators.Helper;

public interface INokoWebGeneratorHelperEntryPoint
{
    void OnInitialized(NokoWebApplication app);
}

public class NokoWebGeneratorHelperEntryPoint : INokoWebGeneratorHelperEntryPoint
{
    public virtual void OnInitialized(NokoWebApplication app)
    {
    }
}
