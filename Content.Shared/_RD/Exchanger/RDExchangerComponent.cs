using Content.Shared._RD.Exchanger.Data;
using Robust.Shared.GameStates;

namespace Content.Shared._RD.Exchanger;

[RegisterComponent, NetworkedComponent]
public sealed partial class RDExchangerComponent : Component
{
    [DataField]
    public RDMerchants Merchants = new();
}
