using Content.Shared._RD.Characteristics.Components;
using Content.Shared._RD.Characteristics.Events;
using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Characteristics;

public abstract class RDSharedCharacteristicSystem : EntitySystem
{
    public void Set(Entity<RDCharacteristicContainerComponent?> entity,
        ProtoId<RDCharacteristicPrototype> id,
        float value)
    {
        if (!Resolve(entity, ref entity.Comp, logMissing: false))
            return;

        entity.Comp.Values[id] = value;
        DirtyField(entity.Owner, entity.Comp, nameof(RDCharacteristicContainerComponent.Values));
    }

    public float Get(Entity<RDCharacteristicContainerComponent?> entity,
        ProtoId<RDCharacteristicPrototype> id)
    {
        if (!Resolve(entity, ref entity.Comp, logMissing: false))
            return RDCharacteristicContainerComponent.DefaultValue;

        if (!entity.Comp.Values.TryGetValue(id, out var value))
            return RDCharacteristicContainerComponent.DefaultValue;

        var ev = new RDGetCharacteristicModifiersEvent((entity, entity.Comp), id);
        return (value + ev.ValueAdditional) * ev.ValueMultiplier;
    }
}
