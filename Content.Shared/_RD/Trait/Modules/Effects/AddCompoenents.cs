using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Trait.Modules.Effects;

public sealed partial class AddComponents : RDTraitEffect
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
}
