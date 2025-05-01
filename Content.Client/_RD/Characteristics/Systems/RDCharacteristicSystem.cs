using Content.Shared._RD.Characteristics.Components;
using Content.Shared._RD.Characteristics.Systems;

namespace Content.Client._RD.Characteristics.Systems;

public sealed class RDCharacteristicSystem : RDSharedCharacteristicSystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<RDCharacteristicContainerComponent, AfterAutoHandleStateEvent>(OnAfterHandleState);
    }

    private void OnAfterHandleState(Entity<RDCharacteristicContainerComponent> entity, ref AfterAutoHandleStateEvent args)
    {
        if (args.State is not  RDCharacteristicContainerComponent.Values_FieldComponentState state)
            return;

        if (state.Values.Equals(entity.Comp.Values))
            return;

        Refresh((entity, entity));
    }
}
