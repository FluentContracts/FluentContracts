namespace FluentContracts.Enums;

/// <summary>
/// The month numbers the date checks take, so a call reads <c>BeInMonth(Month.January)</c> rather
/// than <c>BeInMonth(1)</c>.
/// </summary>
public static class Month
{
    /// <summary>January, month 1 of the year.</summary>
    public static int January { get; }      = 1;
    /// <summary>February, month 2 of the year.</summary>
    public static int February { get; }     = 2;
    /// <summary>March, month 3 of the year.</summary>
    public static int March { get; }        = 3;
    /// <summary>April, month 4 of the year.</summary>
    public static int April { get; }        = 4;
    /// <summary>May, month 5 of the year.</summary>
    public static int May { get; }          = 5;
    /// <summary>June, month 6 of the year.</summary>
    public static int June { get; }         = 6;
    /// <summary>July, month 7 of the year.</summary>
    public static int July { get; }         = 7;
    /// <summary>August, month 8 of the year.</summary>
    public static int August { get; }       = 8;
    /// <summary>September, month 9 of the year.</summary>
    public static int September { get; }    = 9;
    /// <summary>October, month 10 of the year.</summary>
    public static int October { get; }      = 10;
    /// <summary>November, month 11 of the year.</summary>
    public static int November { get; }     = 11;
    /// <summary>December, month 12 of the year.</summary>
    public static int December { get; }     = 12;
}