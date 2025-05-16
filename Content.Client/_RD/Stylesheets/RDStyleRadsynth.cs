using System.Linq;
using Content.Client.Resources;
using Robust.Client.ResourceManagement;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;

namespace Content.Client._RD.Stylesheets;

public sealed class RDStyleRadsynth : RDStyle
{
    public override Stylesheet Stylesheet { get; }

    public RDStyleRadsynth(IResourceCache resCache) : base(resCache)
    {
        var checkBoxTextureChecked = resCache.GetTexture("/Textures/_RD/Interface/Styles/Radsynth/checkbox_checke.png");
        var checkBoxTextureUnchecked = resCache.GetTexture("/Textures/_RD/Interface/Styles/Radsynth/checkbox.png");

        Stylesheet = new Stylesheet(base.Stylesheet.Rules.Concat([
            new (new SelectorElement(typeof(TextureRect), [CheckBox.StyleClassCheckBox], null, null),
                [new StyleProperty(TextureRect.StylePropertyTexture, checkBoxTextureUnchecked)]
            ),
            new (new SelectorElement(typeof(TextureRect), [CheckBox.StyleClassCheckBox, CheckBox.StyleClassCheckBoxChecked], null, null),
                [new StyleProperty(TextureRect.StylePropertyTexture, checkBoxTextureChecked)]
            ),
        ])
        .ToList());
    }
}
