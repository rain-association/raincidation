using Content.Shared._RD.Stiffness.Prototypes;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Stiffness.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(fieldDeltas: true)]
public sealed partial class RDStiffnessComponent : Component
{
    [DataField]
    public ProtoId<RDStiffnessSlotPrototype> Slot;

    [DataField]
    public EntProtoId AppliedEntityId;
}
