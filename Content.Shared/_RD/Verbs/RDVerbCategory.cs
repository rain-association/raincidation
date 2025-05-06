using Content.Shared.Verbs;

namespace Content.Shared._RD.Verbs;

public static class RDVerbCategory
{
    public static readonly VerbCategory AdminSkillAdd =
        new ("rd-verb-categories-admin-skill-add", null, iconsOnly: true) { Columns = 6 };
    public static readonly VerbCategory AdminSkillRemove =
        new ("rd-verb-categories-admin-skill-remove", null, iconsOnly: true) { Columns = 6 };
}
