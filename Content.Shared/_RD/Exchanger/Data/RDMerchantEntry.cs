using Content.Shared._RD.Exchanger.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._RD.Exchanger.Data;

[DataDefinition, Serializable, NetSerializable]
public sealed partial class RDMerchantEntry
{
    [DataField]
    public ProtoId<RDMerchantPrototype> Id;

    [DataField]
    public Dictionary<ProtoId<RDCurrencyPrototype>, int?> Currencies = new();

    [DataField]
    public List<ProtoId<RDMerchantCategoryPrototype>> Categories = new();
}
