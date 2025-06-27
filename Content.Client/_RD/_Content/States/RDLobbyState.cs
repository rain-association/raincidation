using Content.Client._RD._Content.UI.Lobby;
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

namespace Content.Client._RD._Content.States;

public sealed class RDLobbyState : Robust.Client.State.State
{
    [Dependency] private readonly IBaseClient _baseClient = default!;
    [Dependency] private readonly IConfigurationManager _configuration = default!;
    [Dependency] private readonly IEntityManager _entity = default!;
    [Dependency] private readonly IVoteManager _vote = default!;
    [Dependency] private readonly IUserInterfaceManager _userInterface = default!;

    public RDLobbyUI? Lobby;

    protected override Type LinkedScreenType { get; } = typeof(RDLobbyUI);

    private ChatUIController _chatController = default!;
    private ClientGameTicker _gameTicker = default!;

    protected override void Startup()
    {
        if (_userInterface.ActiveScreen is null)
            return;

        Lobby = (RDLobbyUI) _userInterface.ActiveScreen;

        _chatController = _userInterface.GetUIController<ChatUIController>();
        _gameTicker = _entity.System<ClientGameTicker>();

        _chatController.SetMainChat(true);

        _vote.SetPopupContainer(Lobby.VoteContainer);
        LayoutContainer.SetAnchorPreset(Lobby, LayoutContainer.LayoutPreset.Wide);

        var lobbyNameCvar = _configuration.GetCVar(CCVars.ServerLobbyName);
        var serverName = _baseClient.GameInfo?.ServerName ?? string.Empty;

        Lobby.ServerName.Text = string.IsNullOrEmpty(lobbyNameCvar)
            ? Loc.GetString("ui-lobby-title", ("serverName", serverName))
            : lobbyNameCvar;

        var width = _configuration.GetCVar(CCVars.ServerLobbyRightPanelWidth);
        Lobby.RightSide.SetWidth = width;

        _gameTicker.LobbyStatusUpdated += LobbyStatusUpdated;
    }

    protected override void Shutdown()
    {
        var chatController = _userInterface.GetUIController<ChatUIController>();
        chatController.SetMainChat(false);
        _gameTicker.LobbyStatusUpdated -= LobbyStatusUpdated;

        _vote.ClearPopupContainer();

        Lobby = null;
    }

    public void SwitchState(LobbyGui.LobbyGuiState state)
    {
        // Yeah I hate this but LobbyState contains all the badness for now.
        //Lobby?.SwitchState(state);
    }

    private void LobbyStatusUpdated()
    {
        UpdateLobbyBackground();
    }

    private void UpdateLobbyBackground()
    {
        if (_gameTicker.LobbyBackground != null)
        {
            //Lobby!.Background.Texture = _resourceCache.GetResource<TextureResource>(_gameTicker.LobbyBackground );
        }
        else
        {
            Lobby!.Background.Texture = null;
        }
    }
}
