using Content.Shared._RD.Characteristics.Components;
using Content.Shared._RD.Characteristics.Events;
using Content.Shared._RD.Utilities.Random;
using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Characteristics;

public abstract class RDSharedCharacteristicSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _prototype = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<RDCharacteristicContainerComponent, ComponentStartup>(OnStartup);
    }

    private void OnStartup(Entity<RDCharacteristicContainerComponent> entity, ref ComponentStartup args)
    {
        entity.Comp.Seed = GetSeed(GetNetEntity(entity).Id);
    }

    public void Set(Entity<RDCharacteristicContainerComponent?> entity,
        ProtoId<RDCharacteristicPrototype> id,
        int value)
    {
        if (!Resolve(entity, ref entity.Comp, logMissing: false))
            return;

        entity.Comp.Values[id] = value;
        DirtyField(entity.Owner, entity.Comp, nameof(RDCharacteristicContainerComponent.Values));
    }

    public int Get(Entity<RDCharacteristicContainerComponent?> entity,
        ProtoId<RDCharacteristicPrototype> id)
    {
        if (!Resolve(entity, ref entity.Comp, logMissing: false))
            return RDCharacteristicContainerComponent.DefaultValue;

        if (!entity.Comp.Values.TryGetValue(id, out var value))
            return RDCharacteristicContainerComponent.DefaultValue;

        var ev = new RDGetCharacteristicModifiersEvent((entity, entity.Comp), id);
        return (int) Math.Floor((value + ev.ValueAdditional) * ev.ValueMultiplier);
    }

    public bool Check(Entity<RDCharacteristicContainerComponent?> entity,
        ProtoId<RDCharacteristicPrototype> id,
        int difficulty)
    {
        if (!Resolve(entity, ref entity.Comp, logMissing: false))
            return true;

        if (!_prototype.TryIndex(id, out var prototype))
            return true;

        var value = Get(entity, id);
        var modifier = GetModifier(prototype, value);

        var random = new RDXoshiro256(entity.Comp.Seed);
        var checkValue = random.NextInt(RDCharacteristicPrototype.Min, prototype.Max);

        return checkValue + modifier >= difficulty;
    }

    private static long GetSeed(int start)
    {
        var seed = (long) start;
        seed ^= seed << 13;
        seed ^= seed >> 17;
        seed ^= seed << 5;
        return seed;
    }

    private static int GetModifier(RDCharacteristicPrototype prototype, int value)
    {
        return (int) Math.Floor((value - prototype.Medium) / 2f);
    }
}
