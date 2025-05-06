using Content.Shared._RD.Perks.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._RD.Perks.Events;

[Serializable, NetSerializable]
public sealed class RDLearnPerkMessage : EntityEventArgs
{
    public readonly NetEntity Entity;
    public readonly ProtoId<RDPerkPrototype> Skill;

    public RDLearnPerkMessage(NetEntity entity, ProtoId<RDPerkPrototype> skill)
    {
        Entity = entity;
        Skill = skill;
    }
}
