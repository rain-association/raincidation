using Content.Client._RD._Content.Stylesheets;
using Content.Client.Stylesheets;
using Robust.Client.ResourceManagement;
using Robust.Client.UserInterface;

namespace Content.Client._RD.Stylesheets.Manager;

public sealed class RDStylesheetCache : IRDStylesheetCache
{
    [Dependency] private readonly IUserInterfaceManager _userInterfaceManager = default!;
    [Dependency] private readonly IResourceCache _resourceCache = default!;

    public Stylesheet Radsynth { get; private set; } = default!;

    public void Initialize()
    {
        Radsynth = new RDStyleRadsynth(_resourceCache).Stylesheet;

        _userInterfaceManager.Stylesheet = Radsynth;
    }
}
