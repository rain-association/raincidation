using Content.Shared._RD.Characteristics.Components;
using Content.Shared._RD.Characteristics.Events;
using Content.Shared.Administration;
using Content.Shared.Administration.Managers;
using Content.Shared.Examine;
using Content.Shared.Inventory;
using Content.Shared.Inventory.Events;
using Content.Shared.Verbs;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Shared._RD.Characteristics.Systems;

public sealed class RDCharacteristicClothingModifierSystem : EntitySystem
{
    [Dependency] private readonly ISharedAdminManager _admin = default!;
    [Dependency] private readonly IPrototypeManager _prototype = default!;
    [Dependency] private readonly ExamineSystemShared _examine = default!;
    [Dependency] private readonly RDSharedCharacteristicSystem _characteristic = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<RDCharacteristicClothingModifierComponent, GetVerbsEvent<ExamineVerb>>(OnExamineVerb);
        SubscribeLocalEvent<RDCharacteristicClothingModifierComponent, GotEquippedEvent>(OnEquipped);
        SubscribeLocalEvent<RDCharacteristicClothingModifierComponent, GotUnequippedEvent>(OnUnequipped);
        SubscribeLocalEvent<RDCharacteristicClothingModifierComponent, RDGetCharacteristicModifiersEvent>(OnGetModifiers);

        SubscribeLocalEvent<InventoryComponent, RDGetCharacteristicModifiersEvent>(OnInventoryRelay);
    }

    private void OnExamineVerb(Entity<RDCharacteristicClothingModifierComponent> entity, ref GetVerbsEvent<ExamineVerb> args)
    {
        if (!_admin.HasAdminFlag(args.User, AdminFlags.VarEdit))
            return;

        var message = GetExamine(entity.Comp.Values);
        _examine.AddDetailedExamineVerb(
            args,
            entity.Comp,
            message,
            Loc.GetString("rd-characteristic-clothing-modifier-examinable-verb-text"),
            "/Textures/Interface/VerbIcons/dot.svg.192dpi.png",
            Loc.GetString("rd-characteristic-clothing-modifier-examinable-verb-message"));
    }

    private void OnEquipped(Entity<RDCharacteristicClothingModifierComponent> entity, ref GotEquippedEvent args)
    {
        entity.Comp.Enabled = true;
        DirtyField(entity, entity.Comp, nameof(RDCharacteristicClothingModifierComponent.Enabled));

        _characteristic.Refresh(args.Equipee);
    }

    private void OnUnequipped(Entity<RDCharacteristicClothingModifierComponent> entity, ref GotUnequippedEvent args)
    {
        entity.Comp.Enabled = false;
        DirtyField(entity, entity.Comp, nameof(RDCharacteristicClothingModifierComponent.Enabled));

        _characteristic.Refresh(args.Equipee);
    }

    private void OnGetModifiers(Entity<RDCharacteristicClothingModifierComponent> entity, ref RDGetCharacteristicModifiersEvent args)
    {
        if (!entity.Comp.Enabled || !entity.Comp.Values.TryGetValue(args.Id, out var modifier))
            return;

        args.ValueAdditional += modifier.Additional;
        args.ValueMultiplier *= modifier.Multiplier;
    }

    private void OnInventoryRelay(Entity<InventoryComponent> entity, ref RDGetCharacteristicModifiersEvent args)
    {
        var enumerator = new InventorySystem.InventorySlotEnumerator(entity);
        while (enumerator.NextItem(out var item))
        {
            if (!HasComp<RDCharacteristicClothingModifierComponent>(item))
                continue;
            RaiseLocalEvent(item, args);
        }
    }

    private FormattedMessage GetExamine(Dictionary<ProtoId<RDCharacteristicPrototype>, RDCharacteristicClothingModifierComponent.Modifier> modifiers)
    {
        var message = new FormattedMessage();

        message.AddMarkupOrThrow(Loc.GetString("rd-characteristic-clothing-modifier-examine"));

        foreach (var (type, modifier) in modifiers)
        {
            message.PushNewline();

            var prototype = _prototype.Index(type);
            var name = Loc.GetString(prototype.Name);

            message.AddMarkupOrThrow(Loc.GetString("rd-characteristic-clothing-modifier",
                ("type", name),
                ("additional", modifier.Additional),
                ("multiplier", modifier.Multiplier.ToString("G3"))
            ));
        }

        return message;
    }
}
