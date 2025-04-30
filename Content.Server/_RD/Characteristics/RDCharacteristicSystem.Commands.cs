using Content.Server.Administration;
using Content.Shared.Administration;
using Robust.Shared.Console;

namespace Content.Server._RD.Characteristics;

public sealed partial class RDCharacteristicSystem
{
    [Dependency] private readonly IConsoleHost _console = default!;

    private void InitializeCommands()
    {
        _console.RegisterCommand(
            "rd_characteristics:set",
            "",
            "rd_characteristics:set <uid> <protoId> <value>",
            OnCommandSet);

        _console.RegisterCommand(
            "rd_characteristics:get",
            "",
            "rd_characteristics:get <uid> <protoId>",
            OnCommandGet);

        _console.RegisterCommand(
            "rd_characteristics:all",
            "",
            "rd_characteristics:all <uid>",
            OnCommandAll);

        _console.RegisterCommand(
            "rd_characteristics:check",
            "",
            "rd_characteristics:check <uid> <protoId> <difficult>",
            OnCommandCheck);

        _console.RegisterCommand(
            "rd_characteristics:refresh",
            "",
            "rd_characteristics:check <uid>",
            OnCommandRefresh);
    }

    [AdminCommand(AdminFlags.VarEdit)]
    private void OnCommandSet(IConsoleShell console, string raw, string[] args)
    {
        if (args.Length != 3)
        {
            console.WriteError(Loc.GetString("shell-argument-count-must-be", ("value", 3)));
            return;
        }

        if (!NetEntity.TryParse(args[0], out var uidNet) || !TryGetEntity(uidNet, out var entityUid) || entityUid is not { } uid)
        {
            console.WriteError(Loc.GetString("shell-could-not-find-entity", ("entity", args[0])));
            return;
        }

        if (!int.TryParse(args[2], out var value))
        {
            console.WriteError(Loc.GetString("shell-invalid-int", ("value", args[2])));
            return;
        }

        Set(uid, args[1], value);
    }

    [AdminCommand(AdminFlags.VarEdit)]
    private void OnCommandGet(IConsoleShell console, string raw, string[] args)
    {
        if (args.Length != 2)
        {
            console.WriteError(Loc.GetString("shell-argument-count-must-be", ("value", 2)));
            return;
        }

        if (!NetEntity.TryParse(args[0], out var uidNet) || !TryGetEntity(uidNet, out var entityUid) || entityUid is not { } uid)
        {
            console.WriteError(Loc.GetString("shell-could-not-find-entity", ("entity", args[0])));
            return;
        }

        console.WriteLine(Get(uid, args[1]).ToString());
    }

    [AdminCommand(AdminFlags.VarEdit)]
    private void OnCommandAll(IConsoleShell console, string raw, string[] args)
    {
        console.WriteLine("TODO");
    }

    [AdminCommand(AdminFlags.VarEdit)]
    private void OnCommandCheck(IConsoleShell console, string raw, string[] args)
    {
        if (args.Length != 3)
        {
            console.WriteError(Loc.GetString("shell-argument-count-must-be", ("value", 3)));
            return;
        }

        if (!NetEntity.TryParse(args[0], out var uidNet) || !TryGetEntity(uidNet, out var entityUid) || entityUid is not { } uid)
        {
            console.WriteError(Loc.GetString("shell-could-not-find-entity", ("entity", args[0])));
            return;
        }

        if (!int.TryParse(args[2], out var difficulty))
        {
            console.WriteError(Loc.GetString("shell-invalid-int", ("value", args[2])));
            return;
        }

        var result = Check(uid, args[1], difficulty, out var roll, out var modifier);
        var name = result ? "success" : "fail";
        var symbol = result ? ">=" : "<";
        console.WriteLine($"Check {name} ({roll} + {modifier} {{{roll + modifier}}} {symbol} {difficulty})");
    }

    [AdminCommand(AdminFlags.VarEdit)]
    private void OnCommandRefresh(IConsoleShell console, string raw, string[] args)
    {
        if (args.Length != 0)
        {
            console.WriteError(Loc.GetString("shell-argument-count-must-be", ("value", 3)));
            return;
        }

        if (!NetEntity.TryParse(args[0], out var uidNet) || !TryGetEntity(uidNet, out var entityUid) || entityUid is not { } uid)
        {
            console.WriteError(Loc.GetString("shell-could-not-find-entity", ("entity", args[0])));
            return;
        }

        Refresh(uid);
    }
}
