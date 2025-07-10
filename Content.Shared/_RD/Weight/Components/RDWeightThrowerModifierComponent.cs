using Content.Shared._RD.Weight.Systems;
using Robust.Shared.GameStates;

namespace Content.Shared._RD.Weight.Components;

[RegisterComponent, NetworkedComponent]
[Access(typeof(RDWeightThrowModifierSystem))]
public sealed partial class RDWeightThrowerModifierComponent : Component;
