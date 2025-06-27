using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Characteristics;

[Prototype("RDCharacteristic")]
public sealed class RDCharacteristicPrototype : IPrototype
{
    public const int Min = 0;

    [IdDataField]
    public string ID { get; } = string.Empty;

    [DataField]
    public LocId Name = string.Empty;

    [DataField]
    public LocId Description = string.Empty;

    [DataField]
    public ProtoId<RDCharacteristicGroupPrototype> Group;

    [DataField]
    public string Icon = string.Empty;

    [DataField]
    public int Max = 30;

    [DataField]
    public int Medium = 15;

    [DataField]
    public bool Visible = true;
}
