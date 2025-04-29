using Content.Shared._RD.Characteristics;

namespace Content.Server._RD.Characteristics;

public sealed partial class RDCharacteristicSystem : RDSharedCharacteristicSystem
{
    public override void Initialize()
    {
        base.Initialize();

        InitializeCommands();
    }
}
