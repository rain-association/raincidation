using Content.Shared._RD.Exchanger.Data;
using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Exchanger.Prototypes;

[Prototype("RDMerchantCategory")]
public sealed class RDMerchantCategoryPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; } = string.Empty;

    [DataField]
    public List<RDMerchantItemEntry> Entries = new();
}
