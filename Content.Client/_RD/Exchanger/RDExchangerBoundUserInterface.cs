using JetBrains.Annotations;
using Robust.Client.UserInterface;

namespace Content.Client._RD.Exchanger;

[UsedImplicitly]
public sealed class RDExchangerBoundUserInterface : BoundUserInterface
{
    private RDExchangerWindow? _window;

    public RDExchangerBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();

        _window = this.CreateWindow<RDExchangerWindow>();
    }
}
