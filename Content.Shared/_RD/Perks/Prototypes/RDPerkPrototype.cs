using System.Numerics;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Shared._RD.Perks.Prototypes;

[Prototype("RDPerk")]
public sealed class RDPerkPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; } = string.Empty;

    [DataField]
    public ProtoId<RDPerkTreePrototype> Tree;

    [DataField]
    public LocId Name;

    [DataField]
    public LocId Description;

    [DataField]
    public SpriteSpecifier Icon = new SpriteSpecifier.Rsi(new ResPath("/Textures/_RD/Interface/Perks/test.rsi"), "icon");

    [DataField]
    public int Cost;

    [DataField("uiPosition")]
    public Vector2 UIPosition;

    [DataField]
    public List<RDPerkEffect> Effects = new();

    [DataField]
    public List<RDPerkRestriction> Restrictions = new();
}
