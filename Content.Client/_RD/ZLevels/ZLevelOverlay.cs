using System.Numerics;
using Robust.Client.Graphics;
using Robust.Client.Player;
using Robust.Shared.Enums;

namespace Content.Client._RD.ZLevels;

public sealed class ZLevelOverlay : Overlay, IPostInjectInit
{
    [Dependency] private readonly IClyde _clyde = default!;
    [Dependency] private readonly IEyeManager _eye = default!;
    [Dependency] private readonly IEntityManager _entity = default!;
    [Dependency] private readonly IPlayerManager _player = default!;

    public override OverlaySpace Space => OverlaySpace.ScreenSpace;

    private IClydeViewport? _viewport;

    public void PostInject()
    {
        _viewport = _clyde.CreateViewport(_clyde.ScreenSize);
        _viewport.AutomaticRender = true;
    }

    protected override void Draw(in OverlayDrawArgs args)
    {
        if (_player.LocalEntity is not {} entityUid || _viewport is null)
            return;

        var eye = _entity.GetComponent<EyeComponent>(entityUid);
        var transform = _entity.GetComponent<TransformComponent>(entityUid);

        if (_viewport.Eye != eye.Eye)
            _viewport.Eye = eye.Eye;

        args.ScreenHandle.DrawTexture(_viewport.RenderTarget.Texture, Vector2.Zero, Color.Red);
    }
}
