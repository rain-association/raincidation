using Content.Shared._RD.Perks.Prototypes;
using Content.Shared._RD.Perks.Systems;
using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Perks.Modules.Restrictions;

[Serializable]
public sealed partial class RDPerkRestrictionPrecondition : RDPerkRestriction
{
    [DataField]
    public List<ProtoId<RDPerkPrototype>> Perks = new();

    private RDSharedPerkSystem? _perk;

    public override bool Check(IEntityManager entityManager, EntityUid target)
    {
        _perk ??= entityManager.System<RDSharedPerkSystem>();
        return _perk.Learned(target, Perks);
    }

    public override string GetDescription(IEntityManager entityManager)
    {
        return string.Empty;
    }
}
