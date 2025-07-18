﻿using Content.Shared._RD.Weight.Curves;
using Content.Shared._RD.Weight.Systems;
using Robust.Shared.GameStates;

namespace Content.Shared._RD.Weight.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(fieldDeltas: true)]
[Access(typeof(RDWeightSpeedModifierSystem), Other = AccessPermissions.None)]
public sealed partial class RDWeightSpeedModifierComponent : Component
{
    [ViewVariables, AutoNetworkedField]
    public float Value = 1;

    [DataField, ViewVariables, AutoNetworkedField]
    public RDWeightModifierCurve Curve = new RDWeightModifierLinearCurve();
}
