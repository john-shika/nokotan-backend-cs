namespace NokoWebApiSdk.Cores;

public delegate void NokoWebApplicationListenerDelegate(NokoWebApplication application);

public static class NokoWebApplicationListener
{
    public static void Listener(this Action<NokoWebApplication> action, NokoWebApplication application)
    {
        action.Invoke(application);
    }
}