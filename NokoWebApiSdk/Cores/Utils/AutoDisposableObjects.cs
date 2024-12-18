namespace NokoWebApiSdk.Cores.Utils;

public class AutoDisposableObjects(params object?[] objects) : IDisposable
{
    public void Dispose()
    {
        foreach (var obj in objects.Reverse())
        {
            if (obj is not IDisposable disposable) continue;
            disposable.Dispose();
        }
        
        // Suppress Finalize By Garbage Collector
        GC.SuppressFinalize(this);
    }
}
