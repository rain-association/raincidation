using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Shared._RD.Perks.Prototypes;

[Prototype("RDPerkTree")]
public sealed class RDPerkTreePrototype : IPrototype
{
    [IdDataField]
    public string ID { get; } = string.Empty;

    [DataField]
    public LocId Name;

    [DataField]
    public LocId Description;

    [DataField]
    public SpriteSpecifier Icon = new SpriteSpecifier.Rsi(new ResPath("/Textures/_RD/Interface/Perks/default.rsi"), "frame");

    [DataField]
    public SpriteSpecifier FrameIcon = new SpriteSpecifier.Rsi(new ResPath("/Textures/_RD/Interface/Perks/default.rsi"), "frame");

    [DataField]
    public SpriteSpecifier HoveredIcon = new SpriteSpecifier.Rsi(new ResPath("/Textures/_RD/Interface/Perks/default.rsi"), "hovered");

    [DataField]
    public SpriteSpecifier SelectedIcon = new SpriteSpecifier.Rsi(new ResPath("/Textures/_RD/Interface/Perks/default.rsi"), "selected");

    [DataField]
    public SpriteSpecifier LearnedIcon = new SpriteSpecifier.Rsi(new ResPath("/Textures/_RD/Interface/Perks/default.rsi"), "learned");

    [DataField]
    public SpriteSpecifier AvailableIcon = new SpriteSpecifier.Rsi(new ResPath("/Textures/_RD/Interface/Perks/default.rsi"), "available");

    [DataField]
    public Color Color = Color.White;

    [DataField]
    public string Parallax = "AspidParallax";
}
