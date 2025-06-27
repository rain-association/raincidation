using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Shared._RD.Exchanger.Prototypes;

[Prototype("RDCurrency")]
public sealed class RDCurrencyPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; } = string.Empty;

    [DataField]
    public LocId Name;

    [DataField]
    public LocId Description;

    [DataField]
    public LocId Abbreviation;

    [DataField]
    public SpriteSpecifier? Icon;
}
