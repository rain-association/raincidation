using Content.Shared._RD.Stiffness.Prototypes;
using Robust.Shared.Containers;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Stiffness.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(fieldDeltas: true)]
public sealed partial class RDStiffnessContainerComponent : Component
{
    [ViewVariables(VVAccess.ReadOnly)]
    public Container Container = default!;

    [DataField, AutoNetworkedField]
    public string ContainerId = "rd_stiffness";

    [DataField, AutoNetworkedField]
    public List<RDStiffnessSlot> Slots = new();
}

[DataDefinition]
public sealed partial class RDStiffnessSlot
{
    [DataField]
    public ProtoId<RDStiffnessSlotPrototype> Id;
}
