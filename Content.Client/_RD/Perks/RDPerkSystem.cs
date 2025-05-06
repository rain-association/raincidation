using Content.Shared._RD.Perks.Components;
using Content.Shared._RD.Perks.Events;
using Content.Shared._RD.Perks.Prototypes;
using Content.Shared._RD.Perks.Systems;
using Robust.Client.Player;
using Robust.Shared.Prototypes;

namespace Content.Client._RD.Perks;

public sealed class RDPerkSystem : RDSharedPerkSystem
{
    [Dependency] private readonly IPlayerManager _player = default!;

    public event Action<Entity<RDPerkContainerComponent>>? LocalUpdated;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<RDPerkContainerComponent, AfterAutoHandleStateEvent>(OnHandleState);
    }

    private void OnHandleState(Entity<RDPerkContainerComponent> entity, ref AfterAutoHandleStateEvent _)
    {
        if (entity != _player.LocalEntity)
            return;

        LocalUpdated?.Invoke(entity);
    }

    public void RequestUpdate()
    {
        if (!TryComp<RDPerkContainerComponent>(_player.LocalEntity, out var component))
            return;

        LocalUpdated?.Invoke((_player.LocalEntity.Value, component));
    }

    public void RequestLearn(EntityUid target, ProtoId<RDPerkPrototype> perk)
    {
        var ev = new RDLearnPerkMessage(GetNetEntity(target), perk);
        RaiseNetworkEvent(ev);

        /*
        if (_proto.TryIndex(skill.Tree, out var indexedTree))
        {
            _audio.PlayGlobal(indexedTree.LearnSound, target.Value, AudioParams.Default.WithVolume(6f));
        }
        */
    }
}
