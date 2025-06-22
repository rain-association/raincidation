using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Stiffness.Prototypes;

[Prototype("RDStiffnessSlot")]
public sealed class RDStiffnessSlotPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; } = string.Empty;
}
