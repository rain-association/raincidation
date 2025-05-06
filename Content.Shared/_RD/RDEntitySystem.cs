using System.Runtime.CompilerServices;
using Robust.Shared.Map.Components;

namespace Content.Shared._RD;

public abstract class RDEntitySystem : EntitySystem
{
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
