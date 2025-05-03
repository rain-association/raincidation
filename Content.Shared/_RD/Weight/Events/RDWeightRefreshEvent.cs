using Content.Shared._RD.Weight.Components;

namespace Content.Shared._RD.Weight.Events;

[ByRefEvent]
public record RDWeightRefreshEvent(Entity<RDWeightComponent> Entity, float Total);
