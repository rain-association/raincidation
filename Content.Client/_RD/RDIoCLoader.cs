using Content.Client._RD.Stylesheets.Manager;

namespace Content.Client._RD;

public static class RDIoCLoader
{
    public static void RegisterBefore(IDependencyCollection collection)
    {
        collection.Register<RDEntryPoint>();
    }

    public static void RegisterAfter(IDependencyCollection collection)
    {
        collection.Register<IRDStylesheetCache, RDStylesheetCache>();
    }
}
