using Robust.Shared.GameStates;

namespace Content.Shared._RD.Weight.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(fieldDeltas: true)]
public sealed partial class RDWeightExamineCoponent : Component
{
    [DataField, AutoNetworkedField]
    public LocId? Current;

    [DataField, AutoNetworkedField]
    public Dictionary<LocId, Range> Examines = new();
}
