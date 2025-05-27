using System.Linq;
using System.Text.RegularExpressions;
using Content.Shared._RD.Perks.Prototypes;
using Content.Shared._RD.Trait;
using Content.Shared.Humanoid;
using Content.Shared.Humanoid.Prototypes;
using Content.Shared.Preferences;
using Robust.Shared.Configuration;
using Robust.Shared.Player;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._RD.Character;

[DataDefinition, Serializable, NetSerializable]
public sealed partial class RDCharacterProfile : ICharacterProfile
{
    public static readonly RDCharacterProfile Default = new RDCharacterProfile
    {
        Name = "Jon Sanabov",
        Description = "Combat bot",
        Age = 24,
        Height = 220,
        Sex = Sex.Male,
        Species = "RDHuman",
    };

    private static readonly Regex RestrictedNameRegex = new(@"[^A-Za-z0-9 '\-]");
    private static readonly Regex ICNameCaseRegex = new(@"^(?<word>\w)|\b(?<word>\w)(?=\w*$)");

    public const int MaxNameLength = 32;
    public const int MaxDescriptionLength = 512;

    public const int MaxLoadoutNameLength = 32;

    [DataField]
    public string Name { get; set; } = "Stalker";

    [DataField]
    public string Description { get; set; } = string.Empty;

    [DataField]
    public int Age { get; set; } = 18;

    [DataField]
    public int Height { get; set; } = 165;

    [DataField]
    public Sex Sex { get; set; } = Sex.Unsexed;

    [DataField]
    public ProtoId<SpeciesPrototype> Species { get; set; } = "RDHuman";

    [DataField]
    private HashSet<ProtoId<RDPerkPrototype>> _perks = new();

    [DataField]
    private HashSet<ProtoId<RDTraitPrototype>> _traits = new();

    public ICharacterAppearance CharacterAppearance { get; }

    public RDCharacterProfile(RDCharacterProfile profile)
    {
        Name = profile.Name;
        Description = profile.Description;
        Age = profile.Age;
        Height = profile.Height;
        Sex = profile.Sex;
        Species = profile.Species;
        _perks = profile._perks;
        _traits = profile._traits;
        CharacterAppearance = profile.CharacterAppearance;
    }

    public bool MemberwiseEquals(ICharacterProfile other)
    {
        throw new NotImplementedException();
    }

    public void EnsureValid(ICommonSession session, IDependencyCollection collection)
    {
        var configManager = collection.Resolve<IConfigurationManager>();
        var prototypeManager = collection.Resolve<IPrototypeManager>();

        // Species
        if (!prototypeManager.TryIndex(Species, out var speciesPrototype) || !speciesPrototype.RoundStart)
        {
            Species = "RDHuman";
            speciesPrototype = prototypeManager.Index(Species);
        }

        // Sex
        var sex = Sex switch
        {
            Sex.Male => Sex.Male,
            Sex.Female => Sex.Female,
            Sex.Unsexed => Sex.Unsexed,
            _ => Sex.Unsexed,
        };

        if (!speciesPrototype.Sexes.Contains(sex))
            sex = speciesPrototype.Sexes[0];

    }

    public ICharacterProfile Validated(ICommonSession session, IDependencyCollection collection)
    {
        throw new NotImplementedException();
    }
}
