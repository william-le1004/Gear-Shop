namespace Application.Common.ExtensionMethods;

public static class TruncateExtensions
{
    public static string Truncate(this string value, int maxLength, string truncationSuffix = "…")
    {
        return value.Length > maxLength
            ? value.Substring(0, maxLength) + truncationSuffix
            : value;
    }
}