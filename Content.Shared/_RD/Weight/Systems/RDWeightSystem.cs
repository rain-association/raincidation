using Content.Shared._RD.Mathematics.Extensions;
using Content.Shared._RD.Weight.Components;
using Content.Shared._RD.Weight.Events;

namespace Content.Shared._RD.Weight.Systems;

// TODO: Work with stack and reagents
public sealed class RDWeightSystem : EntitySystem
{
    private EntityQuery<RDWeightComponent> _weightQuery;

    public override void Initialize()
    {
        base.Initialize();

        _weightQuery = GetEntityQuery<RDWeightComponent>();

        SubscribeLocalEvent<RDWeightComponent, EntParentChangedMessage>(OnParentChanged);
    }

    private void OnParentChanged(Entity<RDWeightComponent> entity, ref EntParentChangedMessage args)
    {
        Refresh((entity, entity));

        if (args.OldParent is not null)
            Refresh(args.OldParent.Value);
    }

    public void Refresh(Entity<RDWeightComponent?> entity)
    {
        while (true)
        {
            if (!_weightQuery.Resolve(entity, ref entity.Comp, logMissing: false))
                break;

            var weight = RDWeightComponent.DefaultWeight;
            var transform = Transform(entity);
            var parent = transform.ParentUid;
            var enumerator = transform.ChildEnumerator;

            while (enumerator.MoveNext(out var childUid))
            {
                weight += GetTotal(childUid);
            }

            if (!weight.AboutEquals(RDWeightComponent.DefaultWeight) && weight.AboutEquals(entity.Comp.Inside))
            {
                entity.Comp.Inside = weight;
                DirtyField(entity, entity.Comp, nameof(RDWeightComponent.Inside));

                var ev = new RDWeightRefreshEvent((entity, entity.Comp), GetTotal(entity));
                RaiseLocalEvent(entity, ref ev);
            }

            if (parent != EntityUid.Invalid)
            {
                entity = parent;
                continue;
            }

            break;
        }
    }

    public float GetTotal(Entity<RDWeightComponent?> entity, bool refresh = false)
    {
        if (!_weightQuery.Resolve(entity, ref entity.Comp, logMissing: false))
            return RDWeightComponent.DefaultWeight;

        return entity.Comp.Inside + entity.Comp.Value;
    }
}
