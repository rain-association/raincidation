using Content.Client._RD.Perks;
using Content.Client.UserInterface.Systems.Actions;
using Content.Client.UserInterface.Systems.Admin;
using Content.Client.UserInterface.Systems.Bwoink;
using Content.Client.UserInterface.Systems.Character;
using Content.Client.UserInterface.Systems.Crafting;
using Content.Client.UserInterface.Systems.Emotes;
using Content.Client.UserInterface.Systems.EscapeMenu;
using Content.Client.UserInterface.Systems.Gameplay;
using Content.Client.UserInterface.Systems.Guidebook;
using Content.Client.UserInterface.Systems.MenuBar.Widgets;
using Content.Client.UserInterface.Systems.Sandbox;
using Robust.Client.UserInterface.Controllers;

namespace Content.Client.UserInterface.Systems.MenuBar;

public sealed class GameTopMenuBarUIController : UIController
{
    [Dependency] private readonly EscapeUIController _escape = default!;
    [Dependency] private readonly AdminUIController _admin = default!;
    [Dependency] private readonly AHelpUIController _ahelp = default!;
    [Dependency] private readonly ActionUIController _action = default!;
    [Dependency] private readonly SandboxUIController _sandbox = default!;
    [Dependency] private readonly GuidebookUIController _guidebook = default!;
    [Dependency] private readonly RDPerkUIController _perk = default!; // raincidation

    private GameTopMenuBar? GameTopMenuBar => UIManager.GetActiveUIWidgetOrNull<GameTopMenuBar>();

    public override void Initialize()
    {
        base.Initialize();

        var gameplayStateLoad = UIManager.GetUIController<GameplayStateLoadController>();
        gameplayStateLoad.OnScreenLoad += LoadButtons;
        gameplayStateLoad.OnScreenUnload += UnloadButtons;
    }

    public void UnloadButtons()
    {
        _escape.UnloadButton();
        _guidebook.UnloadButton();
        _admin.UnloadButton();
        _ahelp.UnloadButton();
        _action.UnloadButton();
        _sandbox.UnloadButton();
        _perk.UnloadButton();
    }

    public void LoadButtons()
    {
        _escape.LoadButton();
        _guidebook.LoadButton();
        _admin.LoadButton();
        _ahelp.LoadButton();
        _action.LoadButton();
        _sandbox.LoadButton();
        _perk.LoadButton();
    }
}
