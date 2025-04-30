using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._RD.Characteristics.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(fieldDeltas: true)]
public sealed partial class RDCharacteristicClothingModifierComponent : Component
{
    [DataField, ViewVariables, AutoNetworkedField]
    public Dictionary<ProtoId<RDCharacteristicPrototype>, Modifier> Values = new();

    [ViewVariables(VVAccess.ReadOnly), AutoNetworkedField]
    public bool Enabled;

    [Serializable, NetSerializable]
    public record Modifier(int Additional, int Multiplier);
}
