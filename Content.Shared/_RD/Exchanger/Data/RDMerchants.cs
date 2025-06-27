using Robust.Shared.Serialization;

namespace Content.Shared._RD.Exchanger.Data;

[Serializable, NetSerializable]
public sealed class RDMerchants : List<RDMerchantEntry>;
