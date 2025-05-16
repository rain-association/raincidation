namespace Content.Shared._RD.StatusEffect.Events;

/// <summary>
/// Calls on both effect entity and target entity, when a status effect is applied.
/// </summary>
[ByRefEvent]
public readonly record struct RDStatusEffectAppliedEvent(EntityUid Target, Entity<Components.RDStatusEffectComponent> Effect);
