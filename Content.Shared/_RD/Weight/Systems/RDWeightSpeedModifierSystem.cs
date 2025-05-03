using Content.Shared._RD.Mathematics.Extensions;
using Content.Shared._RD.Weight.Components;
using Content.Shared._RD.Weight.Events;
using Content.Shared.Movement.Systems;

namespace Content.Shared._RD.Weight.Systems;

public sealed class RDWeightSpeedModifierSystem : EntitySystem
{
    [Dependency] private readonly MovementSpeedModifierSystem _movementSpeedModifierSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<RDWeightSpeedModifierComponent, RDWeightRefreshEvent>(OnRefresh);
        SubscribeLocalEvent<RDWeightSpeedModifierComponent, RefreshMovementSpeedModifiersEvent>(OnSpeedRefresh);
    }

    private void OnRefresh(Entity<RDWeightSpeedModifierComponent> entity, ref RDWeightRefreshEvent args)
    {
        var value = entity.Comp.Curve.Calculate(args.Entity, args.Total);
        if (value.AboutEquals(entity.Comp.Value))
            return;

        entity.Comp.Value = value;
        DirtyField(entity, entity.Comp, nameof(RDWeightSpeedModifierComponent.Value));

        _movementSpeedModifierSystem.RefreshMovementSpeedModifiers(entity);
    }

    private void OnSpeedRefresh(Entity<RDWeightSpeedModifierComponent> entity, ref RefreshMovementSpeedModifiersEvent args)
    {
        args.ModifySpeed(entity.Comp.Value);
    }
}
