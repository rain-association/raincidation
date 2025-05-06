namespace Content.Shared._RD.Perks;

[ImplicitDataDefinitionForInheritors, Serializable]
public abstract partial class RDPerkRestriction
{
    public abstract bool Check(IEntityManager entityManager, EntityUid target);
    public abstract string GetDescription(IEntityManager entityManager);
}
