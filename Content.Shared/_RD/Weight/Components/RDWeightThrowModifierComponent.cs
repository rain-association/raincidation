using Content.Shared._RD.Weight.Curves;
using Content.Shared._RD.Weight.Systems;
using Robust.Shared.GameStates;

namespace Content.Shared._RD.Weight.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(fieldDeltas: true)]
[Access(typeof(RDWeightThrowModifierSystem))]
public sealed partial class RDWeightThrowModifierComponent : Component
{
    [DataField, ViewVariables, AutoNetworkedField]
    public RDWeightModifierCurve Curve = new RDWeightModifierLinearCurve();
}
