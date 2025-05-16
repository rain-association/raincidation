using Robust.Client.UserInterface;

namespace Content.Client._RD.Stylesheets.Manager;

public interface IRDStylesheetCache
{
    Stylesheet Radsynth { get; }
    void Initialize();
}

