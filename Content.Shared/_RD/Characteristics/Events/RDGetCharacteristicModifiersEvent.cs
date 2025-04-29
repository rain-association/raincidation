using Content.Shared._RD.Characteristics.Components;
using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Characteristics.Events;

[ByRefEvent]
public ref struct RDGetCharacteristicModifiersEvent
{
    public readonly Entity<RDCharacteristicContainerComponent> Container;
    public readonly ProtoId<RDCharacteristicPrototype> Id;

    public float ValueMultiplier = 1;
    public float ValueAdditional;

    public RDGetCharacteristicModifiersEvent(Entity<RDCharacteristicContainerComponent> container, ProtoId<RDCharacteristicPrototype> id)
    {
        Container = container;
        Id = id;
    }
}
