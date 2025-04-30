using Content.Shared._RD.Characteristics.Components;
using Content.Shared._RD.Characteristics.Events;
using Content.Shared.Inventory.Events;

namespace Content.Shared._RD.Characteristics.Systems;

public sealed class RDCharacteristicClothingModifierSystem : EntitySystem
{
    [Dependency] private readonly RDSharedCharacteristicSystem _characteristic = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<RDCharacteristicClothingModifierComponent, GotEquippedEvent>(Refresh);
        SubscribeLocalEvent<RDCharacteristicClothingModifierComponent, GotUnequippedEvent>(Refresh);
        SubscribeLocalEvent<RDCharacteristicClothingModifierComponent, RDGetCharacteristicModifiersEvent>(OnGetModifiers);
    }

    private void Refresh<T>(Entity<RDCharacteristicClothingModifierComponent> entity, ref T args)
    {
        _characteristic.Refresh(entity.Owner);
        entity.Comp.Enabled = args is GotEquippedEvent;
    }

    private void OnGetModifiers(Entity<RDCharacteristicClothingModifierComponent> entity, ref RDGetCharacteristicModifiersEvent args)
    {
        if (!entity.Comp.Enabled || !entity.Comp.Values.TryGetValue(args.Id, out var modifier))
            return;

        args.ValueAdditional += modifier.Additional;
        args.ValueMultiplier *= modifier.Multiplier;
    }
}
