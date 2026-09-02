; Shipped analyzer releases
; https://github.com/dotnet/roslyn-analyzers/blob/main/src/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

## Release 3.7.0

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|-------
FC0001  | Usage    | Warning  | A string argument's BeAnyOf/NotBeAnyOf call binds to the message-first overload, so its first value is silently taken as the message

## Release 4.0.0

### Removed Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|-------
FC0001  | Usage    | Warning  | The message-first overload it policed no longer exists: a message can only follow a bracketed set, so the trap cannot be written
