using Content.Shared._RD.Characteristics.Systems;
using Content.Shared._RD.Utilities.Random;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Characteristics.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(fieldDeltas: true)]
[Access(typeof(RDSharedCharacteristicSystem), Other = AccessPermissions.None)]
public sealed partial class RDCharacteristicContainerComponent : Component
{
    public const int DefaultValue = 0;

    [ViewVariables, AutoNetworkedField]
    public Dictionary<ProtoId<RDCharacteristicPrototype>, int> Values = new();

    [ViewVariables, AutoNetworkedField]
    public Dictionary<ProtoId<RDCharacteristicPrototype>, int> ModifiedValues = new();

    [ViewVariables]
    public RDXoshiro256 Random;
}
