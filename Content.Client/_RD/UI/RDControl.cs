using System.Numerics;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;

namespace Content.Client._RD.UI;

public abstract class RDControl : Control
{
    public Color? BackgroundColor;

    public void SetAnchorPreset(LayoutContainer.LayoutPreset preset)
    {
        LayoutContainer.SetAnchorPreset(this, preset);
    }

    protected override void Draw(DrawingHandleScreen handle)
    {
        base.Draw(handle);

        if (BackgroundColor is not null)
            handle.DrawRect(new UIBox2(Vector2.Zero, Size), BackgroundColor.Value);
    }
}
