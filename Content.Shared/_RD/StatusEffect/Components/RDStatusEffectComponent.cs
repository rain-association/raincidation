using Content.Shared._RD.StatusEffect.Systems;
using Content.Shared.Alert;
using Content.Shared.Whitelist;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

namespace Content.Shared._RD.StatusEffect.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState, AutoGenerateComponentPause]
[Access(typeof(RDStatusEffectSystem))]
public sealed partial class RDStatusEffectComponent : Component
{
    [DataField, AutoNetworkedField]
    public EntityUid? AppliedTo;

    [DataField, AutoNetworkedField]
    public ProtoId<AlertPrototype>? Alert;

    [DataField(customTypeSerializer: typeof(TimeOffsetSerializer)), AutoPausedField, AutoNetworkedField]
    public TimeSpan? EndEffectTime;

    [DataField]
    public EntityWhitelist? Whitelist;

    [DataField]
    public EntityWhitelist? Blacklist;

    [DataField]
    public ComponentRegistry Components = new();
}
