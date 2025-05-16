using Content.Client._RD.Stylesheets.Manager;

namespace Content.Client._RD;

public sealed class RDEntryPoint
{
    [Dependency] private readonly IRDStylesheetCache _stylesheetCache = default!;

    public void PostInitBefore()
    {
        // ...
    }

    public void PostInitAfter()
    {
        _stylesheetCache.Initialize();
    }
}
