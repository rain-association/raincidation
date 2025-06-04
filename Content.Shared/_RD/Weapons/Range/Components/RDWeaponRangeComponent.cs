using Robust.Shared.GameStates;

namespace Content.Shared._RD.Weapons.Range.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(fieldDeltas: true)]
public sealed partial class RDWeaponRangeComponent : Component
{
    [DataField, AutoNetworkedField]
    public float F;
}
