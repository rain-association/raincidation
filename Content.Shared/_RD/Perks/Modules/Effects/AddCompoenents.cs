using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Perks.Modules.Effects;

public sealed partial class AddComponents : RDPerkEffect
{
    [DataField]
    public ComponentRegistry Components = new();

    public override void Add(IEntityManager entityManager, EntityUid target)
    {
        entityManager.AddComponents(target, Components);
    }

    public override void Remove(IEntityManager entityManager, EntityUid target)
    {
        entityManager.RemoveComponents(target, Components);
    }

    public override string GetName(IEntityManager entityManager)
    {
        return string.Empty;
    }

    public override string GetDescription(IEntityManager entityManager)
    {
        return string.Empty;
    }
}
