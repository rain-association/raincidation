using System.Linq;
using System.Runtime.CompilerServices;
using Content.Shared._RD.Perks.Components;
using Content.Shared._RD.Perks.Prototypes;
using Content.Shared._RD.Verbs;
using Content.Shared.Administration;
using Content.Shared.Administration.Managers;
using Content.Shared.Verbs;
using Robust.Shared.Prototypes;
using Dependency = Robust.Shared.IoC.DependencyAttribute;

namespace Content.Shared._RD.Perks.Systems;

public abstract class RDSharedPerkSystem : RDEntitySystem
{
    [Dependency] private readonly ISharedAdminManager _admin = default!;
    [Dependency] private readonly IEntityManager _entity = default!;
    [Dependency] private readonly IPrototypeManager _prototype = default!;

    public IReadOnlyList<RDPerkPrototype> PerkPrototypes { get; private set; } = new List<RDPerkPrototype>();
    public IReadOnlyList<RDPerkTreePrototype> PerkTreePrototypes { get; private set; } = new List<RDPerkTreePrototype>();

    public override void Initialize()
    {
        base.Initialize();

        OnPrototypesReloaded();
        _prototype.PrototypesReloaded += _ => OnPrototypesReloaded();

        SubscribeLocalEvent<RDPerkContainerComponent, GetVerbsEvent<Verb>>(OnGetVerbs);
    }

    private void OnGetVerbs(Entity<RDPerkContainerComponent> entity, ref GetVerbsEvent<Verb> args)
    {
        if (!_admin.HasAdminFlag(args.User, AdminFlags.VarEdit))
            return;

        var target = args.Target;

        // Add skill points
        foreach (var tree in PerkTreePrototypes)
        {
            entity.Comp.Progress.TryGetValue(tree, out var current);

            var name = Loc.GetString(tree.Name);
            args.Verbs.Add(new Verb
            {
                Text = name,
                Message = $"{name} LV {current} -> {current + 1}",
                Category = RDVerbCategory.AdminSkillAdd,
                Icon = tree.Icon,
                Act = () =>
                {
                    TryAddLevel(target, tree.ID, 1);
                },
                Priority = 2,
            });
        }

        // Remove skill points
        foreach (var tree in PerkTreePrototypes)
        {
            entity.Comp.Progress.TryGetValue(tree, out var current);
            if (current < 1)
                continue;

            var name = Loc.GetString(tree.Name);
            args.Verbs.Add(new Verb
            {
                Text = name,
                Message = $"{name} LV {current} -> {current - 1}",
                Category = RDVerbCategory.AdminSkillRemove,
                Icon = tree.Icon,
                Act = () =>
                {
                    TryRemoveLevel(target, tree.ID, 1);
                },
                Priority = 2,
            });
        }
    }

    public string GetName(ProtoId<RDPerkPrototype> perk)
    {
        if (!_prototype.TryIndex(perk, out var perkPrototype))
            return string.Empty;

        if (perkPrototype.Name != string.Empty)
            return Loc.GetString(perkPrototype.Name);

        return string.Empty;
    }

    public string GetDescription(ProtoId<RDPerkPrototype> perk)
    {
        if (!_prototype.TryIndex(perk, out var perkPrototype))
            return string.Empty;

        if (perkPrototype.Description != string.Empty)
            return Loc.GetString(perkPrototype.Description);

        return string.Empty;
    }

    public bool TryAddLevel(Entity<RDPerkContainerComponent?> entity, ProtoId<RDPerkTreePrototype> tree, int value)
    {
        if (!Resolve(entity, ref entity.Comp, logMissing: false))
            return false;

        if (entity.Comp.Progress.TryGetValue(tree, out var currentLevel))
        {
            // If the tree already exists, add experience to it
            entity.Comp.Progress[tree] = currentLevel + value;
            Dirty(entity);
            return true;
        }

        // If the tree doesn't exist, initialize it with the experience
        entity.Comp.Progress[tree] = value;
        Dirty(entity);
        return true;
    }

    public bool TryRemoveLevel(Entity<RDPerkContainerComponent?> entity, ProtoId<RDPerkTreePrototype> tree, int value)
    {
        if (!Resolve(entity, ref entity.Comp, logMissing: false))
            return false;

        if (!entity.Comp.Progress.TryGetValue(tree, out var currentValue))
            return false;

        if (currentValue < value)
            return false;

        entity.Comp.Progress[tree] = Math.Max(0, currentValue - value);
        Dirty(entity);
        return true;
    }

    public bool TryAdd(Entity<RDPerkContainerComponent?> entity, ProtoId<RDPerkPrototype> perk)
    {
        if (!Resolve(entity, ref entity.Comp, logMissing: false))
            return false;

        if (entity.Comp.Values.Contains(perk))
            return false;

        if (!_prototype.TryIndex(perk, out var perkPrototype))
            return false;

        if (perkPrototype.Effects.Count > 0)
        {
            foreach (var effect in perkPrototype.Effects)
            {
                effect.Add(EntityManager, entity);
            }
        }

        entity.Comp.SkillsSumExperience += perkPrototype.Cost;
        entity.Comp.Values.Add(perk);

        Dirty(entity);
        return true;
    }

    public bool Learn(Entity<RDPerkContainerComponent?> entity, ProtoId<RDPerkPrototype> perk)
    {
        if (!Resolve(entity, ref entity.Comp, logMissing: false))
            return false;

        if (!_prototype.TryIndex(perk, out var perkPrototype))
            return false;

        if (!CanLearn(entity, perk))
            return false;

        if (!TryRemoveLevel(entity, perkPrototype.Tree, perkPrototype.Cost))
            return false;

        if (!TryAdd(entity, perk))
            return false;

        return false;
    }

    public bool CanLearn(Entity<RDPerkContainerComponent?> entity, ProtoId<RDPerkPrototype> perk)
    {
        if (!Resolve(entity, ref entity.Comp, logMissing: false))
            return false;

        if (!AllowLearn(entity, perk))
            return false;

        if (!_prototype.TryIndex(perk, out var perkPrototype))
            return false;

        if (!entity.Comp.Progress.TryGetValue(perkPrototype.Tree, out var value))
            return false;

        if (value < perkPrototype.Cost)
            return false;

        return true;
    }

    public bool AllowLearn(Entity<RDPerkContainerComponent?> entity, ProtoId<RDPerkPrototype> perk)
    {
        if (!Resolve(entity, ref entity.Comp, logMissing: false))
            return false;

        if (Learned((entity, entity.Comp), perk))
            return false;

        if (!_prototype.TryIndex(perk, out var perkPrototype))
            return false;

        foreach (var restriction in perkPrototype.Restrictions)
        {
            if (!restriction.Check(EntityManager, entity))
                return false;
        }

        return true;
    }

    public bool Learned(Entity<RDPerkContainerComponent?> entity, List<ProtoId<RDPerkPrototype>> perks)
    {
        if (perks.Count == 1)
            return Learned(entity, perks[0]);

        if (!Resolve(entity, ref entity.Comp, logMissing: false))
            return false;

        foreach (var perk in perks)
        {
            if (!Get(entity).Contains(perk))
                return false;
        }

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Learned(Entity<RDPerkContainerComponent?> entity, ProtoId<RDPerkPrototype> perk)
    {
        return Resolve(entity, ref entity.Comp, logMissing: false) && entity.Comp.Values.Contains(perk);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HashSet<ProtoId<RDPerkPrototype>> Get(Entity<RDPerkContainerComponent?> entity)
    {
        return !Resolve(entity, ref entity.Comp, logMissing: false) ? [] : entity.Comp.Values;
    }

    private void OnPrototypesReloaded()
    {
        PerkPrototypes = _prototype.EnumeratePrototypes<RDPerkPrototype>()
            .OrderBy(perk => Loc.GetString(perk.Name))
            .ToList();

        PerkTreePrototypes = _prototype.EnumeratePrototypes<RDPerkTreePrototype>()
            .OrderBy(tree => Loc.GetString(tree.Name))
            .ToList();
    }
}
