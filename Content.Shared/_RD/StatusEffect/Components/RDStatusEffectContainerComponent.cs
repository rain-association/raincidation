using Content.Shared._RD.StatusEffect.Systems;
using Robust.Shared.GameStates;

namespace Content.Shared._RD.StatusEffect.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(true)]
[Access(typeof(RDStatusEffectSystem))]
public sealed partial class RDStatusEffectContainerComponent : Component
{
    [DataField, AutoNetworkedField]
    public HashSet<EntityUid> ActiveStatusEffects = new();
}
