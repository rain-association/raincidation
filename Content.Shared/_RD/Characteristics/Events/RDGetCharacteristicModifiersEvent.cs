using Content.Shared._RD.Characteristics.Components;
using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Characteristics.Events;

public sealed class RDGetCharacteristicModifiersEvent : EventArgs
{
    public readonly Entity<RDCharacteristicContainerComponent> Container;
    public readonly ProtoId<RDCharacteristicPrototype> Id;

    public float ValueMultiplier = 1;
    public int ValueAdditional;

    public RDGetCharacteristicModifiersEvent(Entity<RDCharacteristicContainerComponent> container, ProtoId<RDCharacteristicPrototype> id)
    {
        Container = container;
        Id = id;
    }
}
