using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Characteristics.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(fieldDeltas: true)]
[Access(typeof(RDSharedCharacteristicSystem), Other = AccessPermissions.None)]
public sealed partial class RDCharacteristicContainerComponent : Component
{
    public const int DefaultValue = 0;

    [ViewVariables, AutoNetworkedField]
    public Dictionary<ProtoId<RDCharacteristicPrototype>, float> Values = new();
}
