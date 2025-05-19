using Content.Client.GameTicking.Managers;
using Content.Client.Lobby.UI;
using Content.Client.UserInterface.Systems.Chat;
using Content.Client.Voting;
using Content.Shared.CCVar;
using Robust.Client;
using Robust.Client.ResourceManagement;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Configuration;

namespace Content.Client.Lobby;

public sealed class LobbyState : Robust.Client.State.State
{
    [Dependency] private readonly IBaseClient _baseClient = default!;
    [Dependency] private readonly IConfigurationManager _cfg = default!;
    [Dependency] private readonly IEntityManager _entityManager = default!;
    [Dependency] private readonly IResourceCache _resourceCache = default!;
    [Dependency] private readonly IUserInterfaceManager _userInterfaceManager = default!;
    [Dependency] private readonly IVoteManager _voteManager = default!;

    private ClientGameTicker _gameTicker = default!;

    protected override Type? LinkedScreenType { get; } = typeof(LobbyGui);
    public LobbyGui? Lobby;

    protected override void Startup()
    {
        if (_userInterfaceManager.ActiveScreen is null)
            return;

        Lobby = (LobbyGui) _userInterfaceManager.ActiveScreen;

        var chatController = _userInterfaceManager.GetUIController<ChatUIController>();
        _gameTicker = _entityManager.System<ClientGameTicker>();

        chatController.SetMainChat(true);

        _voteManager.SetPopupContainer(Lobby.VoteContainer);
        LayoutContainer.SetAnchorPreset(Lobby, LayoutContainer.LayoutPreset.Wide);

        var lobbyNameCvar = _cfg.GetCVar(CCVars.ServerLobbyName);
        var serverName = _baseClient.GameInfo?.ServerName ?? string.Empty;

        Lobby.ServerName.Text = string.IsNullOrEmpty(lobbyNameCvar)
            ? Loc.GetString("ui-lobby-title", ("serverName", serverName))
            : lobbyNameCvar;

        var width = _cfg.GetCVar(CCVars.ServerLobbyRightPanelWidth);
        Lobby.RightSide.SetWidth = width;

        _gameTicker.LobbyStatusUpdated += LobbyStatusUpdated;
    }

    protected override void Shutdown()
    {
        var chatController = _userInterfaceManager.GetUIController<ChatUIController>();
        chatController.SetMainChat(false);
        _gameTicker.LobbyStatusUpdated -= LobbyStatusUpdated;

        _voteManager.ClearPopupContainer();

        Lobby = null;
    }

    public void SwitchState(LobbyGui.LobbyGuiState state)
    {
        // Yeah I hate this but LobbyState contains all the badness for now.
        Lobby?.SwitchState(state);
    }

    private void LobbyStatusUpdated()
    {
        UpdateLobbyBackground();
    }

    private void UpdateLobbyBackground()
    {
        if (_gameTicker.LobbyBackground != null)
        {
            Lobby!.Background.Texture = _resourceCache.GetResource<TextureResource>(_gameTicker.LobbyBackground );
        }
        else
        {
            Lobby!.Background.Texture = null;
        }
    }
}
