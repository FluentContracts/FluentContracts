# FluentContracts.Analyzers

The home of the package's Roslyn rules. It currently ships no rules: FC0001 policed the
message-first `BeAnyOf` overload, and 4.0.0 removed that overload, so the trap it caught can no
longer be written. The project, its code-fix companion and the packaging into `analyzers/dotnet/cs`
stay in place for the next rule — see `AGENTS.md`, *Analyzers*.
