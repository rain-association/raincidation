using System.Linq;
using Content.Server.Administration;
using Content.Shared._RD.Traits;
using Content.Shared.Administration;
using Robust.Shared.Console;
using Robust.Shared.Prototypes;

namespace Content.Server._RD.Traits;

[AdminCommand(AdminFlags.VarEdit)]
public sealed class RDTraitAddCommand : LocalizedCommands
{
    [Dependency] private readonly IEntityManager _entity = default!;
    [Dependency] private readonly IPrototypeManager _prototype = default!;

    public override string Command => "rd_trait_add";
    public override string Description => "";
    public override string Help => "rd_trait_add <uid> <traitPrototype>";

    public override void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        if (args.Length != 2)
        {
            shell.WriteError(Loc.GetString("shell-wrong-arguments-number-need-specific", ("properAmount", 2), ("currentAmount", args.Length)));
            return;
        }

        if (!NetEntity.TryParse(args[0], out var uidNet) || !_entity.TryGetEntity(uidNet, out var entityUid) || entityUid is not { } uid)
        {
            shell.WriteError(Loc.GetString("shell-could-not-find-entity", ("entity", args[0])));
            return;
        }

        if (!_prototype.TryIndex<RDTraitPrototype>(args[1], out var prototype))
        {
            shell.WriteError(Loc.GetString("shell-argument-must-be-prototype", ("index", 1), ("prototypeName", args[1])));
            return;
        }

        _entity.System<RDTraitSystem>().Add(uid, prototype);
        shell.WriteLine(Loc.GetString("shell-command-success"));
    }

    public override CompletionResult GetCompletion(IConsoleShell shell, string[] args)
    {
        return args.Length switch
        {
            1 => CompletionResult.FromHintOptions(CompletionHelper.Components<RDTraitContainerComponent>(args[0]), "<uid>"),
            2 => CompletionResult.FromHintOptions(PrototypeIDs(shell, args), "<traitPrototype>"),
            _ => CompletionResult.Empty,
        };
    }

    private IEnumerable<CompletionOption> PrototypeIDs(IConsoleShell _, string[] args)
    {
        if (!NetEntity.TryParse(args[0], out var uidNet) || !_entity.TryGetEntity(uidNet, out var entityUid) || entityUid is not { } uid)
            return CompletionHelper.PrototypeIDs<RDTraitPrototype>();

        if (!_entity.TryGetComponent<RDTraitContainerComponent>(uid, out var containerComponent))
            return CompletionHelper.PrototypeIDs<RDTraitPrototype>();

        var options = new List<CompletionOption>();
        foreach (var trait in _prototype.EnumeratePrototypes<RDTraitPrototype>())
        {
            if (containerComponent.Values.Contains(trait.ID))
                continue;

            options.Add(new CompletionOption(trait.ID));
        }

        return options.OrderBy(o => o.Value);
    }
}
