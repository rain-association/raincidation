using System.Linq;
using Content.Client._RD.Stylesheets;
using Content.Client.Resources;
using Robust.Client.ResourceManagement;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;
using static Robust.Client.UserInterface.StylesheetHelpers;

namespace Content.Client._RD._Content.Stylesheets;

public sealed class RDStyleRadsynth : RDStyle
{
    public override Stylesheet Stylesheet { get; }

    public RDStyleRadsynth(IResourceCache resCache) : base(resCache)
    {
        var fontCozetteVector = resCache.GetFont("/Fonts/_RD/Cozette/vector.otf", size: 18);

        var checkBoxTextureChecked = resCache.GetTexture("/Textures/_RD/Interface/Styles/Radsynth/checkbox_checked.png");
        var checkBoxTextureUnchecked = resCache.GetTexture("/Textures/_RD/Interface/Styles/Radsynth/checkbox.png");

        Stylesheet = new Stylesheet(base.Stylesheet.Rules.Concat([
            Element()
                .Class("monospace")
                .Prop("font", fontCozetteVector),

            #region Window title.
            new StyleRule(
                new SelectorElement(typeof(Label), [DefaultWindow.StyleClassWindowTitle], null, null),
                new[]
                {
                    new StyleProperty(Label.StylePropertyFontColor, NanoGold),
                    new StyleProperty(Label.StylePropertyFont, fontCozetteVector),
                }),
            #endregion

            #region Checkbox
            new (new SelectorElement(typeof(TextureRect), [CheckBox.StyleClassCheckBox], null, null),
                [new StyleProperty(TextureRect.StylePropertyTexture, checkBoxTextureUnchecked)]
            ),
            new (new SelectorElement(typeof(TextureRect), [CheckBox.StyleClassCheckBox, CheckBox.StyleClassCheckBoxChecked], null, null),
                [new StyleProperty(TextureRect.StylePropertyTexture, checkBoxTextureChecked)]
            ),
            #endregion

            Element<RichTextLabel>()
                .Prop(Label.StylePropertyFont, fontCozetteVector),

        ])
        .ToList());
    }
}
