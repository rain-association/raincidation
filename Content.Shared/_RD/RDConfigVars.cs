using Robust.Shared.Configuration;

namespace Content.Shared._RD;

[CVarDefs]
public sealed class RDConfigVars
{
    /*
     * Weight
     */

    public static readonly CVarDef<int> WeightMaxUpdates =
        CVarDef.Create("rd_weight.max_updates", 10_000, CVar.REPLICATED);
}
