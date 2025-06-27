using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Characteristics;

[Prototype("RDCharacteristicGroup")]
public sealed class RDCharacteristicGroupPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; } = string.Empty;
}
