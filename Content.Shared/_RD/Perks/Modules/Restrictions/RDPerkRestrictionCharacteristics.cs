using Content.Shared._RD.Characteristics;
using Content.Shared._RD.Characteristics.Systems;
using JetBrains.Annotations;
using Robust.Shared.Prototypes;

namespace Content.Shared._RD.Perks.Modules.Restrictions;

[UsedImplicitly, Serializable]
public sealed partial class RDPerkRestrictionCharacteristics : RDPerkRestriction
{
    [DataField]
    public Dictionary<ProtoId<RDCharacteristicPrototype>, int> Min = new();

    [DataField]
    public Dictionary<ProtoId<RDCharacteristicPrototype>, int> Max = new();

    private RDSharedCharacteristicSystem? _characteristic;

    public override bool Check(IEntityManager entityManager, EntityUid target)
    {
        _characteristic ??= entityManager.System<RDSharedCharacteristicSystem>();

        foreach (var (protoId, value) in Min)
        {
            if (_characteristic.GetRaw(target, protoId) < value)
                return false;
        }

        foreach (var (protoId, value) in Max)
        {
            if (_characteristic.GetRaw(target, protoId) > value)
                return false;
        }

        return true;
    }

    public override string GetDescription(IEntityManager entityManager)
    {
        return string.Empty;
    }
}
