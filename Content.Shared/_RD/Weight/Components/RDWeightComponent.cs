using Content.Shared._RD.Weight.Systems;
using Robust.Shared.GameStates;

namespace Content.Shared._RD.Weight.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(fieldDeltas: true)]
[Access(typeof(RDWeightSystem), Other = AccessPermissions.None)]
public sealed partial class RDWeightComponent : Component
{
    public const float DefaultWeight = 0;

    [DataField, AutoNetworkedField]
    public float Value;

    [ViewVariables, AutoNetworkedField]
    public float Inside;
}
