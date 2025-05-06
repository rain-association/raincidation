namespace Content.Shared._RD.Perks;

[ImplicitDataDefinitionForInheritors]
public abstract partial class RDPerkEffect
{
    public abstract void Add(IEntityManager entityManager, EntityUid target);
    public abstract void Remove(IEntityManager entityManager, EntityUid target);
    public abstract string GetName(IEntityManager entityManager);
    public abstract string GetDescription(IEntityManager entityManager);
}
