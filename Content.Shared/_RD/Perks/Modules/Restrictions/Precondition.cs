using System.Text;
using Content.Shared._RD.Perks.Prototypes;
using Content.Shared._RD.Perks.Systems;
using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Perks.Modules.Restrictions;

[Serializable]
public sealed partial class Precondition : RDPerkRestriction
{
    [DataField]
    public List<ProtoId<RDPerkPrototype>> Perks = new();

    private IPrototypeManager? _prototype;
    private RDSharedPerkSystem? _perk;

    public override bool Check(IEntityManager entityManager, EntityUid target)
    {
        _perk ??= entityManager.System<RDSharedPerkSystem>();

        return _perk.Learned(target, Perks);
    }

    public override string GetDescription(IEntityManager entityManager)
    {
        _perk ??= entityManager.System<RDSharedPerkSystem>();
        _prototype ??= IoCManager.Resolve<IPrototypeManager>();

        var message = new StringBuilder();
        message.AppendLine(Loc.GetString("rd-perk-restriction-precondition"));

        foreach (var perk in Perks)
        {
            var prekIndexed = _prototype.Index(perk);
            var line = $"- {_perk.GetName(prekIndexed)}";
            message.AppendLine(line);
        }

        return message.ToString();
    }
}
