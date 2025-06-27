using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Traits;

public sealed class RDTraitSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _prototype = default!;

    public void Add(Entity<RDTraitContainerComponent?> entity, ProtoId<RDTraitPrototype> id)
    {
        if (!_prototype.TryIndex(id, out var traitPrototype))
            return;

        entity.Comp ??= EnsureComp<RDTraitContainerComponent>(entity);

        if (entity.Comp.Values.Contains(id))
            return;

        foreach (var effect in traitPrototype.Effects)
        {
            effect.Add(EntityManager, entity);
        }

        entity.Comp.Values.Add(id);
    }

    public void Remove(Entity<RDTraitContainerComponent?> entity, ProtoId<RDTraitPrototype> id)
    {
        if (!Resolve(entity, ref entity.Comp))
            return;

        if (!entity.Comp.Values.Contains(id))
            return;

        if (!_prototype.TryIndex(id, out var traitPrototype))
            return;

        foreach (var effect in traitPrototype.Effects)
        {
            effect.Remove(EntityManager, entity);
        }

        entity.Comp.Values.Remove(id);
    }
}
