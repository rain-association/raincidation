using Content.Shared._RD.Exchanger.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
using Robust.Shared.Utility;

namespace Content.Shared._RD.Exchanger.Data;

[DataDefinition, Serializable, NetSerializable]
public sealed partial class RDMerchantItemEntry
{
    [DataField]
    public EntProtoId Id;

    [DataField]
    public LocId? Name;

    [DataField]
    public LocId? Description;

    [DataField]
    public LocId? Tooltip;

    [DataField]
    public SpriteSpecifier? Icon;

    [DataField]
    public int Amount = 1;

    [DataField]
    public int? Count;

    [DataField]
    public ProtoId<RDCurrencyPrototype> Currency;

    [DataField]
    public float? Cost;
}
