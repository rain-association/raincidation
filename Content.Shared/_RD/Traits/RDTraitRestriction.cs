namespace Content.Shared._RD.Traits;

[ImplicitDataDefinitionForInheritors, Serializable]
public abstract partial class RDTraitRestriction
{
    public abstract bool Check(IEntityManager entityManager, EntityUid target);
    public abstract string GetDescription(IEntityManager entityManager);
}
