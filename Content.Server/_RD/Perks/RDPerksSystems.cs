using Content.Shared._RD.Perks.Events;
using Content.Shared._RD.Perks.Systems;

namespace Content.Server._RD.Perks;

public sealed class RDPerksSystems : RDSharedPerkSystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeNetworkEvent<RDLearnPerkMessage>(OnLearn);
    }

    private void OnLearn(RDLearnPerkMessage ev, EntitySessionEventArgs args)
    {
        var entity = GetEntity(ev.Entity);
        if (args.SenderSession.AttachedEntity != entity)
            return;

        Learn(entity, ev.Skill);
    }
}
