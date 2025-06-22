using Content.Shared.Input;
using Robust.Shared.Input.Binding;

namespace Content.Shared._RD.Weapons.Range.Systems;

public sealed class RDWeaponRangeSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        // CommandBinds.Builder
        //    .Bind(RDKeyFunctions.Lay, InputCmdHandler.FromDelegate(HandleLay, handle: false, outsidePrediction: false))
        //    .Register<STLaySystem>();
    }
}
