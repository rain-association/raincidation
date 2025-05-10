using Robust.Shared.Serialization;

namespace Content.Shared._RD.DeathScreen;

[Serializable, NetSerializable]
public sealed class RDDeathScreenShowEvent : EntityEventArgs
{
    public readonly string Title;
    public readonly string Reason;
    public readonly string AudioPath;

    public RDDeathScreenShowEvent(string title, string reason = "", string audioPath = "")
    {
        Title = title;
        Reason = reason;
        AudioPath = audioPath;
    }

    public override string ToString()
    {
        return $"{Title}: {Reason} ({AudioPath})";
    }
}
