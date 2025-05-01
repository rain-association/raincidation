using Robust.Client.Graphics;
using Robust.Shared.Player;

namespace Content.Client._RD.ZLevels;

public sealed class ZLevelSystem : EntitySystem
{
    [Dependency] private readonly IOverlayManager _overlay = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ZLevelViewerComponent, ComponentInit>(OnPlayerAttached);
        SubscribeLocalEvent<ZLevelViewerComponent, ComponentShutdown>(OnPlayerDetached);
    }

    private void OnPlayerAttached(Entity<ZLevelViewerComponent> entity, ref ComponentInit args)
    {
        var overlay = new ZLevelOverlay();

        IoCManager.InjectDependencies(overlay);
        overlay.PostInject();

        _overlay.AddOverlay(overlay);
    }

    private void OnPlayerDetached(Entity<ZLevelViewerComponent> entity, ref ComponentShutdown args)
    {
        _overlay.RemoveOverlay<ZLevelOverlay>();
    }
}
