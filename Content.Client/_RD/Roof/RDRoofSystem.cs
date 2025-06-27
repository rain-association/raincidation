using System.Linq;
using Content.Shared._RD;
using Content.Shared._RD.Roof;
using Robust.Client.GameObjects;
using Robust.Client.Player;
using Robust.Shared.Map.Components;

namespace Content.Client._RD.Roof;

public sealed class RDRoofSystem : RDEntitySystem
{
    [Dependency] private readonly IPlayerManager _player = default!;
    [Dependency] private readonly SharedMapSystem _map = default!;

    private EntityQuery<RDRoofComponent> _roofQuery;

    private bool _visible = true;

    public override void Initialize()
    {
        base.Initialize();

        _roofQuery = GetEntityQuery<RDRoofComponent>();
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        if (_player.LocalEntity is not {} playerUid)
            return;

        var transform = Transform(playerUid);
        if (transform.GridUid is not {} gridUid)
            return;

        if (!TryComp<MapGridComponent>(gridUid, out var gridComponent))
            return;

        var anchored = _map.GetAnchoredEntities(gridUid, gridComponent, transform.Coordinates);
        var under = anchored.Any(ent => _roofQuery.HasComp(ent));

        _visible = under switch
        {
            true when _visible => false,
            false when !_visible => true,
            _ => _visible,
        };

        UpdateVisibilityAll();
    }

    public void UpdateVisibilityAll()
    {
        var query = EntityQueryEnumerator<RDRoofComponent, SpriteComponent>();
        while (query.MoveNext(out var uid, out var roofComponent, out var spriteComponent))
        {
            UpdateVisibility((uid, roofComponent, spriteComponent));
        }
    }

    private void UpdateVisibility(Entity<RDRoofComponent?, SpriteComponent?> entity)
    {
        if (!Resolve(entity, ref entity.Comp1, ref entity.Comp2, logMissing: false))
            return;

        entity.Comp2.Visible = _visible;
    }
}
