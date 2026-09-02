; Unshipped analyzer release
; https://github.com/dotnet/roslyn-analyzers/blob/main/src/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|-------
FC0001  | Usage    | Warning  | A string argument's BeAnyOf/NotBeAnyOf call binds to the message-first overload, so its first value is silently taken as the message
