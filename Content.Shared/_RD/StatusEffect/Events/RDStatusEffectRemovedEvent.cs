namespace Content.Shared._RD.StatusEffect.Events;

/// <summary>
/// Calls on both effect entity and target entity, when a status effect is removed.
/// </summary>
[ByRefEvent]
public readonly record struct RDStatusEffectRemovedEvent(EntityUid Target, Entity<Components.RDStatusEffectComponent> Effect);
