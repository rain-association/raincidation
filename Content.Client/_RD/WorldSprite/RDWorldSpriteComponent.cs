using Robust.Client.Graphics;

namespace Content.Client._RD.WorldSprite;

[RegisterComponent]
public sealed partial class RDWorldSpriteComponent : Component
{
    [ViewVariables]
    public Dictionary<object, RSI?> CachedTexture = new();

    [DataField]
    public string Sprite = string.Empty;

    [DataField]
    public List<string> Layers = new();
}
