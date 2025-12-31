using System.Collections.Generic;

namespace RoboCopyElite.Core.Options;

/// <summary>
/// Describes a single robocopy switch and its metadata so the UI can render and the builder can compose arguments deterministically.
/// </summary>
public sealed record OptionDefinition(
    string Switch,
    string DisplayName,
    OptionCategory Category,
    OptionArgumentKind ArgumentKind = OptionArgumentKind.None,
    string? Description = null,
    string? ValuePlaceholder = null,
    bool AllowsMultiple = false,
    bool IsDangerous = false,
    bool RequiresValue = false)
{
    public IReadOnlyCollection<string> SwitchTokens => _switchTokens ??= SplitSwitch();

    private IReadOnlyCollection<string>? _switchTokens;

    private IReadOnlyCollection<string> SplitSwitch()
    {
        if (Switch.Contains(' '))
        {
            return Switch.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
        }

        return new[] { Switch };
    }
}

public enum OptionCategory
{
    Basic,
    Advanced,
    Filters,
    Performance,
    Security,
    Logging,
    Jobs,
    Validation
}

public enum OptionArgumentKind
{
    None,
    Integer,
    String,
    Path,
    SwitchList,
    TimeWindow,
    SizeWithSuffix,
    Boolean
}
