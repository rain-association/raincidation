using System.Linq;
using Content.Client._RD.Stylesheets;
using Content.Client.Resources;
using Robust.Client.Graphics;
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
        var buttonTexture = resCache.GetTexture("/Textures/_RD/Interface/Styles/Radsynth/button.png");
        var buttonRoundedTexture = resCache.GetTexture("/Textures/_RD/Interface/Styles/Radsynth/button_rounded.png");

        var roundedButton = new StyleBoxTexture
        {
            Texture = buttonRoundedTexture,
        };
        roundedButton.SetPatchMargin(StyleBox.Margin.All, 5);

        var button = new StyleBoxTexture
        {
            Texture = buttonTexture,
        };
        button.SetPatchMargin(StyleBox.Margin.All, 10);

        Stylesheet = new Stylesheet(base.Stylesheet.Rules.Concat([
            #region Window title
            new StyleRule(
                new SelectorElement(typeof(Label), [DefaultWindow.StyleClassWindowTitle], null, null),
                new[]
                {
                    new StyleProperty(Label.StylePropertyFontColor, NanoGold),
                    new StyleProperty(Label.StylePropertyFont, fontCozetteVector),
                }),
            #endregion

            #region Checkbox
            Element<TextureRect>()
                .Class(CheckBox.StyleClassCheckBox)
                .Prop(TextureRect.StylePropertyTexture, checkBoxTextureUnchecked),

            Element<TextureRect>()
                .Class(CheckBox.StyleClassCheckBox)
                .Class(CheckBox.StyleClassCheckBoxChecked)
                .Prop(TextureRect.StylePropertyTexture, checkBoxTextureChecked),
            #endregion

            Element<Button>()
                .Prop(ContainerButton.StylePropertyStyleBox, button),

            Element<Button>()
                .Class(StyleClassChatChannelSelectorButton)
                .Prop(ContainerButton.StylePropertyStyleBox, roundedButton),

            Element<TextureButton>()
                .Class("CrossButtonRed")
                .Prop(TextureButton.StylePropertyTexture, resCache.GetTexture("/Textures/_RD/Interface/Styles/Radsynth/cross.png"))
                .Prop(Control.StylePropertyModulateSelf, Color.White),

            Element<PanelContainer>()
                .Class(ClassAngleRect)
                .Prop(PanelContainer.StylePropertyPanel, button)
                .Prop(Control.StylePropertyModulateSelf, Color.FromHex("#FFFFFF")),

            Element<PanelContainer>()
                .Class("BackgroundOpenRight")
                .Prop(PanelContainer.StylePropertyPanel, button)
                .Prop(Control.StylePropertyModulateSelf, Color.FromHex("#25252A")),

            Element<PanelContainer>()
                .Class("BackgroundOpenLeft")
                .Prop(PanelContainer.StylePropertyPanel, button)
                .Prop(Control.StylePropertyModulateSelf, Color.FromHex("#25252A")),
        ])
        .ToList());
    }
}
