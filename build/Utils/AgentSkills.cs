using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Utils;

/// <summary>
/// Validates a skills tree against the <a href="https://agentskills.io/specification">Agent Skills
/// specification</a>.
/// </summary>
/// <remarks>
/// The skills are served to three harnesses — Claude Code, Codex and Gemini CLI — that each load the
/// folders directly, and the specification requires a skill's frontmatter <c>name</c> to equal its
/// directory name. A rename that touches only one of the two ships a skill that silently never
/// loads, with nothing else in the build to notice.
/// </remarks>
public static class AgentSkills
{
    /// <summary><c>name</c> per the specification: lowercase alphanumerics and single hyphens.</summary>
    static readonly Regex NamePattern = new("^[a-z0-9]+(-[a-z0-9]+)*$");

    /// <summary>The specification's maximum <c>name</c> length.</summary>
    const int NameMaxLength = 64;

    /// <summary>The specification's maximum <c>description</c> length.</summary>
    const int DescriptionMaxLength = 1024;

    /// <summary>
    /// A YAML block-scalar indicator (<c>&gt;</c> or <c>|</c>, with optional chomping and indent
    /// modifiers). This validator does not resolve block scalars, so a field written with one is
    /// reported as uncheckable rather than validated as the indicator it parses to.
    /// </summary>
    static readonly Regex BlockScalar = new("^[>|][+-]?[0-9]*$");

    /// <summary>
    /// YAML wants whitespace after the colon, so <c>key:value</c> is not a mapping. Matching only the
    /// spec-legal form means a required field written that way is reported missing rather than read.
    /// </summary>
    static readonly Regex Field = new("^([A-Za-z][A-Za-z0-9_-]*):(?:[ \t]+(.*))?$");

    /// <summary>
    /// Every way the skills under <paramref name="root"/> violate the specification, one message per
    /// problem and each prefixed with the offending document's path. Empty means the tree conforms.
    /// </summary>
    /// <param name="root">The skills directory.</param>
    /// <returns>The problems found.</returns>
    public static IReadOnlyList<string> Check(string root)
    {
        if (!Directory.Exists(root))
            return [$"{root}: missing — the skills tree is gone entirely."];

        var problems = new List<string>();
        var directories = Directory.GetDirectories(root).OrderBy(x => x, StringComparer.Ordinal).ToList();

        if (directories.Count == 0)
            problems.Add($"{root}: holds no skill folder.");

        foreach (var directory in directories)
        {
            var document = Path.Combine(directory, "SKILL.md");

            if (!File.Exists(document))
            {
                problems.Add($"{document}: missing — every skill folder needs a SKILL.md.");
                continue;
            }

            problems.AddRange(
                CheckDocument(Path.GetFileName(directory), File.ReadAllText(document))
                    .Select(x => $"{document}: {x}"));
        }

        return problems;
    }

    /// <summary>
    /// The ways one skill document violates the specification, given the directory it lives under.
    /// </summary>
    /// <param name="directoryName">The name of the folder holding the document.</param>
    /// <param name="content">The document's text.</param>
    /// <returns>The problems found; empty means the document conforms.</returns>
    public static IReadOnlyList<string> CheckDocument(string directoryName, string content)
    {
        var frontmatter = ParseFrontmatter(content);

        if (frontmatter == null)
            return ["has no YAML frontmatter block (--- … ---)."];

        var problems = frontmatter.Duplicates
            .Select(x => $"frontmatter repeats the \"{x}\" key; strict YAML parsers reject that outright.")
            .ToList();

        problems.AddRange(CheckName(directoryName, frontmatter));
        problems.AddRange(CheckDescription(frontmatter));

        return problems;
    }

    static IEnumerable<string> CheckName(string directoryName, Frontmatter frontmatter)
    {
        if (!frontmatter.Fields.TryGetValue("name", out var name) || name.Length == 0)
        {
            yield return "frontmatter is missing the required \"name\" field.";
            yield break;
        }

        if (BlockScalar.IsMatch(name))
        {
            yield return "\"name\" uses a YAML block scalar, which cannot be checked here; keep it on one line.";
            yield break;
        }

        if (name != directoryName)
        {
            yield return
                $"frontmatter name \"{name}\" does not match the directory \"{directoryName}\"; "
                + "the specification and every harness require them to be identical.";
        }

        if (!NamePattern.IsMatch(name))
            yield return $"name \"{name}\" is not lowercase alphanumerics with single hyphens.";

        if (name.Length > NameMaxLength)
            yield return $"name is {name.Length} characters (the maximum is {NameMaxLength}).";
    }

    static IEnumerable<string> CheckDescription(Frontmatter frontmatter)
    {
        if (!frontmatter.Fields.TryGetValue("description", out var description) || description.Length == 0)
        {
            yield return "frontmatter is missing the required \"description\" field.";
            yield break;
        }

        if (BlockScalar.IsMatch(description))
        {
            yield return
                "\"description\" uses a YAML block scalar, which cannot be checked here; keep it on one line.";
            yield break;
        }

        if (description.Length > DescriptionMaxLength)
            yield return $"description is {description.Length} characters (the maximum is {DescriptionMaxLength}).";
    }

    /// <summary>A document's parsed frontmatter: its single-line fields, and any key it repeats.</summary>
    sealed class Frontmatter
    {
        public Dictionary<string, string> Fields { get; } = new(StringComparer.Ordinal);
        public List<string> Duplicates { get; } = [];
    }

    /// <summary>
    /// Parses the leading YAML frontmatter block, or returns <c>null</c> when the document has no
    /// closed frontmatter fence.
    /// </summary>
    /// <remarks>
    /// Deliberately not a YAML parser, but strict where YAML is strict: a byte-order mark is stripped
    /// the way a real loader strips it, one layer of matching quotes is removed the way a real loader
    /// resolves it, and a line without whitespace after the colon is not a mapping and is not read.
    /// </remarks>
    static Frontmatter ParseFrontmatter(string content)
    {
        var lines = content.TrimStart('\uFEFF').Split('\n');

        if (lines.Length == 0 || lines[0].TrimEnd('\r').TrimEnd() != "---")
            return null;

        var frontmatter = new Frontmatter();

        foreach (var line in lines.Skip(1).Select(x => x.TrimEnd('\r')))
        {
            if (line.TrimEnd() == "---") return frontmatter;

            var match = Field.Match(line);
            if (!match.Success) continue;

            var key = match.Groups[1].Value;
            if (frontmatter.Fields.ContainsKey(key)) frontmatter.Duplicates.Add(key);

            frontmatter.Fields[key] = Unquote(match.Groups[2].Value.Trim());
        }

        // Never closed, so it is not a frontmatter block at all.
        return null;
    }

    /// <summary>Strips one layer of matching single or double quotes from a scalar.</summary>
    static string Unquote(string value)
    {
        if (value.Length < 2) return value;

        var first = value[0];
        return (first == '"' || first == '\'') && value[^1] == first
            ? value[1..^1]
            : value;
    }
}
