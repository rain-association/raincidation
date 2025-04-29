using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Characteristics;

[Prototype("RDCharacteristic")]
public sealed class RDCharacteristicPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; } = string.Empty;

    [DataField]
    public LocId Name = string.Empty;

    [DataField]
    public LocId Description = string.Empty;

    [DataField]
    public bool Visible = true;
}
