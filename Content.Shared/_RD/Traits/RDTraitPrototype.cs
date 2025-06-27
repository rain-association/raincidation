using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Traits;

[Prototype("RDTrait")]
public sealed class RDTraitPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; } = string.Empty;

    [DataField]
    public LocId Name = string.Empty;

    [DataField]
    public LocId Description = string.Empty;

    [DataField]
    public int Cost;

    [DataField]
    public List<RDTraitEffect> Effects = new();
}
