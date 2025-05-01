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

    [DataDefinition, Serializable, NetSerializable]
    public partial struct Modifier
    {
        [DataField]
        public int Additional;

        [DataField]
        public float Multiplier = 1;

        public Modifier(int additional, float multiplier)
        {
            Additional = additional;
            Multiplier = multiplier;
        }
    }
}
