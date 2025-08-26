namespace PrefixFixer.Extentions;

public static class StringExtentions
{
    private static PrefixFixer Plugin => PrefixFixer.Instance;
    private static string[] PrefixsToFix => Plugin.Config.PrefixsToFix.ToArray();
    private static string Prefix => Plugin.Config.Prefix;

    public static bool ContainsPrefix( this string msg) => PrefixsToFix.Any(msg.Contains) || msg.Contains("[CSS]");
    
    public static string ReplacePrefixs(this string msg)
    {
        msg = PrefixsToFix.Where(prefixToFix => !prefixToFix.Equals("[CSS]")).Aggregate(msg, (current, prefixToFix) => current.Replace(prefixToFix, Prefix));

        return msg.Replace("[CSS]", Prefix);
    }
}