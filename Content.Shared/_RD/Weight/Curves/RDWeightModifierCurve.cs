using Robust.Shared.Serialization;

namespace Content.Shared._RD.Weight.Curves;

[ImplicitDataDefinitionForInheritors, Serializable, NetSerializable]
public abstract partial class RDWeightModifierCurve
{
    public abstract float Calculate(float total);
}
