using System.Runtime.CompilerServices;
using Robust.Shared.Map.Components;
using Robust.Shared.Player;
using Robust.Shared.Prototypes;

namespace Content.Shared._RD;

public abstract class RDEntitySystem : EntitySystem
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void AddComps(EntityUid targetUid, ComponentRegistry registry)
    {
        EntityManager.AddComponents(targetUid, registry);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void RemComps(EntityUid targetUid, ComponentRegistry registry)
    {
        EntityManager.RemoveComponents(targetUid, registry);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected ICommonSession? GetSession(EntityUid entityUid)
    {
        return CompOrNull<ActorComponent>(entityUid)?.PlayerSession;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool IsMap(EntityUid entityUid)
    {
        return HasComp<MapComponent>(entityUid) || HasComp<MapGridComponent>(entityUid);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool OnMap(EntityUid entityUid)
    {
        var parent = Transform(entityUid).ParentUid;
        return HasComp<MapComponent>(parent) || HasComp<MapGridComponent>(parent);
    }
}
