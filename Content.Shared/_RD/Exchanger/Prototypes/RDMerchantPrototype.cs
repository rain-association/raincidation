using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Exchanger.Prototypes;

[Prototype("RDMerchant")]
public sealed class RDMerchantPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; } = string.Empty;

    [DataField]
    public LocId Name;

    [DataField]
    public LocId Description;
}
