using System.Diagnostics.CodeAnalysis;
using Robust.Client.Graphics;

namespace Content.Client._RD.WorldSprite;

[RegisterComponent]
public sealed partial class RDWorldSpriteComponent : Component
{
    [ViewVariables]
    public readonly Dictionary<object, RSI?> CachedTexture = new();

    [DataField]
    public string Sprite = string.Empty;

    [DataField]
    [SuppressMessage("ReSharper", "UseCollectionExpression")]
    public List<string> Layers = new()
    {
        "$main",
    };
}
