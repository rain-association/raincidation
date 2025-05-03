using Content.Shared._RD.Weight.Systems;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization;

namespace Content.Shared._RD.Weight.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(fieldDeltas: true)]
[Access(typeof(RDWeightSpeedModifierSystem), Other = AccessPermissions.None)]
public sealed partial class RDWeightSpeedModifierComponent : Component
{
    [ViewVariables, AutoNetworkedField]
    public float Value = 1;

    [DataField, ViewVariables, AutoNetworkedField]
    public RDWeightSpeedModifierCurve Curve = new RDWeightSpeedModifierLinearCurve();
}

[ImplicitDataDefinitionForInheritors, Serializable, NetSerializable]
public abstract partial class RDWeightSpeedModifierCurve
{
    public abstract float Calculate(float total);
}

[Serializable, NetSerializable]
public sealed partial class RDWeightSpeedModifierLinearCurve : RDWeightSpeedModifierCurve
{
    [DataField]
    public float Min;

    [DataField]
    public float Max;

    public override float Calculate(float total)
    {
        return Math.Clamp(1 - (total - Min) / (Max - Min), 0f, 1f);
    }
}
