using Content.Shared.Alert;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._RD.Weight.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(fieldDeltas: true)]
public sealed partial class RDWeightAlertsComponent : Component
{
    [DataField, AutoNetworkedField]
    public ProtoId<AlertPrototype>? Alert;

    [DataField, AutoNetworkedField]
    public Dictionary<ProtoId<AlertPrototype>, Range> Alerts = new();
}

[DataDefinition, Serializable, NetSerializable]
public sealed partial class Range
{
    [DataField]
    public float Min;

    [DataField]
    public float Max;
}
