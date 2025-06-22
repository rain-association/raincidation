using Robust.Shared.GameStates;

namespace Content.Shared._RD.Weapons.Range.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(fieldDeltas: true)]
public sealed partial class RDWeaponRangeComponent : Component
{
    [DataField, AutoNetworkedField]
    public float FireRate = 8f;

    [DataField, AutoNetworkedField]
    public int Shots = 5;

    [DataField, AutoNetworkedField]
    public int BurstShots = 5;

    [DataField, AutoNetworkedField]
    public SelectiveFire SelectedMode = SelectiveFire.SemiAuto;

    [DataField, AutoNetworkedField]
    public SelectiveFire AvailableModes = SelectiveFire.SemiAuto;
}

[Flags]
public enum SelectiveFire : byte
{
    Invalid = 0,
    SemiAuto = 1 << 0,
    Burst = 1 << 1,
    FullAuto = 1 << 2,
}
