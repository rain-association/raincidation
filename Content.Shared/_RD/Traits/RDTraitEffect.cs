namespace Content.Shared._RD.Traits;

[ImplicitDataDefinitionForInheritors]
public abstract partial class RDTraitEffect
{
    public abstract void Add(IEntityManager entityManager, EntityUid target);
    public abstract void Remove(IEntityManager entityManager, EntityUid target);
}
