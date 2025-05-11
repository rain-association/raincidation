using Robust.Shared.Prototypes;

namespace Content.Shared._RD.StatusEffect.Events;

/// <summary>
/// Raised on an entity before a status effect is added to determine if adding it should be cancelled.
/// </summary>
[ByRefEvent]
public record struct RDBeforeStatusEffectAddedEvent(EntProtoId Effect, bool Cancelled = false);
