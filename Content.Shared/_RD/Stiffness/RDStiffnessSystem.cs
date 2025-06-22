using Content.Shared._RD.Stiffness.Components;
using Content.Shared._RD.Stiffness.Prototypes;
using Content.Shared.Interaction;
using Content.Shared.Rejuvenate;
using Content.Shared.Weapons.Melee.Events;
using Robust.Shared.Containers;
using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Stiffness;

public sealed class RDStiffnessSystem : RDEntitySystem
{
    [Dependency] private readonly IComponentFactory _componentFactory = default!;
    [Dependency] private readonly IPrototypeManager _prototype = default!;
    [Dependency] private readonly SharedContainerSystem _container = default!;

    private EntityQuery<RDStiffnessComponent> _stiffnessQuery;

    public override void Initialize()
    {
        base.Initialize();

        _stiffnessQuery = GetEntityQuery<RDStiffnessComponent>();

        SubscribeLocalEvent<RDStiffnessComponent, AfterInteractEvent>(OnAfterInteract);
        SubscribeLocalEvent<RDStiffnessComponent, MeleeHitEvent>(OnMeleeHit);

        SubscribeLocalEvent<RDStiffnessContainerComponent, ComponentInit>(OnInit);
        SubscribeLocalEvent<RDStiffnessContainerComponent, RejuvenateEvent>(OnRejuvenate);
        SubscribeLocalEvent<RDStiffnessContainerComponent, EntInsertedIntoContainerMessage>(OnInserted);
    }

    public bool TryApply(Entity<RDStiffnessContainerComponent?> entity,
        Entity<RDStiffnessComponent> stiffness,
        bool forced = false)
    {
        if (!Resolve(entity, ref entity.Comp, logMissing: false))
            return false;

        // Check if we already have an item in the matching slot
        foreach (var contained in entity.Comp.Container.ContainedEntities)
        {
            if (!_stiffnessQuery.TryGetComponent(contained, out var existingStiffness))
                continue;

            if (existingStiffness.Slot == stiffness.Comp.Slot && !forced)
                return false;
        }

        var instance = SpawnAttachedTo(stiffness.Comp.AppliedEntityId, Transform(entity).Coordinates);
        if (!_container.Insert(instance, entity.Comp.Container))
        {
            QueueDel(entity);
            return false;
        }

        var comp = EnsureComp<RDStiffnessComponent>(instance);
        comp.Slot = stiffness.Comp.Slot;
        comp.AppliedEntityId = stiffness.Comp.AppliedEntityId;
        return true;
    }

    private void OnAfterInteract(Entity<RDStiffnessComponent> entity, ref AfterInteractEvent args)
    {
        if (args.Target is not { Valid: true } target || !args.CanReach)
            return;

        args.Handled = TryApply(target, entity);
    }

    private void OnMeleeHit(Entity<RDStiffnessComponent> entity, ref MeleeHitEvent args)
    {
        if (args.HitEntities.Count == 0)
            return;

        TryApply(args.HitEntities[0], entity);
        args.Handled = true;
    }

    private void OnInit(Entity<RDStiffnessContainerComponent> entity, ref ComponentInit _)
    {
        entity.Comp.Container = _container.EnsureContainer<Container>(entity, nameof(RDStiffnessContainerComponent));
    }

    private void OnRejuvenate(Entity<RDStiffnessContainerComponent> entity, ref RejuvenateEvent _)
    {
        _container.EmptyContainer(entity.Comp.Container, true);
    }

    private void OnInserted(Entity<RDStiffnessContainerComponent> entity, ref EntInsertedIntoContainerMessage args)
    {

    }
}
