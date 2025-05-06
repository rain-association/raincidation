using Content.Shared._RD.Perks.Prototypes;
using Content.Shared._RD.Perks.Systems;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Perks.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(fieldDeltas: true)]
[Access(typeof(RDSharedPerkSystem), Other = AccessPermissions.Read)]
public sealed partial class RDPerkContainerComponent : Component
{
    [DataField, AutoNetworkedField]
    public HashSet<ProtoId<RDPerkPrototype>> Values = new();

    [DataField, AutoNetworkedField]
    public Dictionary<ProtoId<RDPerkTreePrototype>, int> Progress = new();

    [DataField, AutoNetworkedField]
    public int SkillsSumExperience;
}
