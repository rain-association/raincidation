using System.Linq;
using System.Numerics;
using Content.Client._RD.Perks.UI;
using Content.Client.Gameplay;
using Content.Client.UserInterface.Controls;
using Content.Client.UserInterface.Systems.MenuBar.Widgets;
using Content.Shared._RD.Input;
using Content.Shared._RD.Perks.Components;
using Content.Shared._RD.Perks.Prototypes;
using JetBrains.Annotations;
using Robust.Client.GameObjects;
using Robust.Client.Player;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controllers;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Input.Binding;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Client._RD.Perks;

[UsedImplicitly]
public sealed class RDPerkUIController : UIController, IOnStateEntered<GameplayState>, IOnStateExited<GameplayState>, IOnSystemChanged<RDPerkSystem>
{
    [UISystemDependency] private readonly SpriteSystem _sprite = default!;
    [UISystemDependency] private readonly RDPerkSystem _perk = default!;
    [Dependency] private readonly IPrototypeManager _prototype = default!;
    [Dependency] private readonly IPlayerManager _player = default!;

    private RDPerkWindow? _window;
    private RDPerkPrototype? _selectedPerk;

    private MenuButton? MenuButton => UIManager.GetActiveUIWidgetOrNull<GameTopMenuBar>()?.RDPerkButton;

    public void OnStateEntered(GameplayState state)
    {
        CreateWindow();
        CommandBinds.Builder
            .Bind(RDKeyFunctions.PerkMenu, InputCmdHandler.FromDelegate(_ => ToggleWindow()))
            .Register<RDPerkUIController>();
    }

    public void OnStateExited(GameplayState state)
    {
        DisposeWindow();
        CommandBinds.Unregister<RDPerkUIController>();
    }

    public void OnSystemLoaded(RDPerkSystem system)
    {
        system.LocalUpdated += OnUpdateState;
        _player.LocalPlayerAttached += OnLocalPlayerDetached;
    }

    public void OnSystemUnloaded(RDPerkSystem system)
    {
        system.LocalUpdated -= OnUpdateState;
        _player.LocalPlayerDetached -= OnLocalPlayerDetached;
    }

    public void LoadButton()
    {
        if (MenuButton is null)
            return;

        MenuButton.OnPressed += ButtonPressed;

        if (_window is null)
            return;

        _window.OnClose += DeactivateButton;
        _window.OnOpen += ActivateButton;
    }

    public void UnloadButton()
    {
        if (MenuButton is null)
            return;

        MenuButton.OnPressed -= ButtonPressed;
    }

    private void OnUpdateState(Entity<RDPerkContainerComponent> entity)
    {
        if (_window is null)
            return;

        _window.GraphControl.UpdateState(entity);

        // Reselect for update state
        SelectPerk(_selectedPerk);

        // If tree not selected, select the first one
        if (_window.GraphControl.Tree is null && entity.Comp.Progress.Count > 0)
        {
            var firstTree = entity.Comp.Progress.First().Key;
            if (_prototype.TryIndex(firstTree, out var indexedTree))
                SelectTree(indexedTree, entity); // Set the first tree from the player's progress
        }

        // Update the experience points for the selected tree
        var playerProgress = entity.Comp.Progress;
        if (_window.GraphControl.Tree is not null && playerProgress.TryGetValue(_window.GraphControl.Tree, out var skillpoint))
            _window.ExpPointLabel.Text = skillpoint.ToString();

        _window.LevelLabel.Text = $"{entity.Comp.SkillsSumExperience}";
        _window.TreeTabsContainer.RemoveAllChildren();

        foreach (var (tree, progress) in entity.Comp.Progress)
        {
            if (!_prototype.TryIndex(tree, out var indexedTree))
                continue;

            var treeButton2 = new RDPerkTreeButtonControl(indexedTree.Color, Loc.GetString(indexedTree.Name));
            treeButton2.ToolTip = Loc.GetString(indexedTree.Description);
            treeButton2.OnPressed += () =>
            {
                SelectTree(indexedTree, entity);
            };

            _window.TreeTabsContainer.AddChild(treeButton2);
        }
    }

    private void SelectPerk(RDPerkPrototype? perk)
    {
        if (_window is null)
            return;

        if (_player.LocalEntity is null)
            return;

        _selectedPerk = perk;

        if (perk is null)
        {
            _window.SkillName.Text = string.Empty;
            _window.SkillDescription.Text = string.Empty;
            _window.SkillView.Texture = null;
            _window.LearnButton.Disabled = true;
            return;
        }

        var description = new FormattedMessage();
        description.TryAddMarkup(_perk.GetDescriptionFull(_player.LocalEntity.Value, perk), out _);

        _window.SkillName.Text = _perk.GetName(perk);
        _window.SkillDescription.SetMessage(description);
        _window.SkillView.Texture = _sprite.Frame0(perk.Icon);
        _window.SkillCost.Text = perk.Cost.ToString();
        _window.LearnButton.Disabled = !_perk.CanLearn(_player.LocalEntity.Value, perk);
    }

    private void SelectTree(RDPerkTreePrototype tree, RDPerkContainerComponent container)
    {
        if (_window == null)
            return;

        _window.GraphControl.Tree = tree;
        _window.ParallaxBackground.ParallaxPrototype = tree.Parallax;
        _window.TreeName.Text = Loc.GetString(tree.Name);

        var playerProgress = container.Progress;
        _window.ExpPointLabel.Text = playerProgress.TryGetValue(tree, out var skillpoint) ? skillpoint.ToString() : "0";
    }

    private void DeactivateButton()
    {
        MenuButton!.Pressed = false;
    }

    private void ActivateButton()
    {
        MenuButton!.Pressed = true;
    }

    private void ButtonPressed(BaseButton.ButtonEventArgs _)
    {
        ToggleWindow();
    }

    private void OnLocalPlayerDetached(EntityUid entityUid)
    {
        CloseWindow();
    }

    private void ToggleWindow()
    {
        if (_window is null)
            return;

        if (_window.IsOpen)
        {
            CloseWindow();
            return;
        }

        OpenWindow();
    }

    private void OpenWindow()
    {
        _perk.RequestUpdate();
        _window?.Open();
    }

    private void CloseWindow()
    {
        _window?.Close();
    }

    private void CreateWindow()
    {
        _window = UIManager.CreateWindow<RDPerkWindow>();
        LayoutContainer.SetAnchorPreset(_window, LayoutContainer.LayoutPreset.CenterTop);

        _window.LearnButton.OnPressed += RequestLearn;
        _window.GraphControl.OnNodeSelected += SelectPerk;
        _window.GraphControl.OnOffsetChanged += offset =>
        {
            _window.ParallaxBackground.Offset = -offset * 0.25f + new Vector2(1000, 1000); // Hardcoding is bad
        };
        _window.ParallaxBackground.ParallaxPrototype = "RDBlack";
    }

    private void RequestLearn(BaseButton.ButtonEventArgs _)
    {
        if (_player.LocalEntity is null || _selectedPerk is null)
            return;

        _perk.RequestLearn(_player.LocalEntity.Value, _selectedPerk);
    }

    private void DisposeWindow()
    {
        if (_window is null)
            return;

        UIManager.RootControl.RemoveChild(_window);
        _window = null;
    }
}
