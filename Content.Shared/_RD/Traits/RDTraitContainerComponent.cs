using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Traits;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(fieldDeltas: true)]
public sealed partial class RDTraitContainerComponent : Component
{
    [AutoNetworkedField]
    public List<ProtoId<RDTraitPrototype>> Values = new();
}
