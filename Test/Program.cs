namespace Test;

public static class Program
{
    public static string Truncate(this string value, int maxLength, string truncationSuffix = "…")
    {
        return value.Length > maxLength
            ? value.Substring(0, maxLength) + truncationSuffix
            : value;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("asadshaj".Truncate(5));
    }
}
