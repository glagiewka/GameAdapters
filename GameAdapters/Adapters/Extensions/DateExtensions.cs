namespace GameAdapters.Adapters.Extensions;

public static class DateExtensions
{
    public static long GetMillisecondsSinceEpoch(this DateTime dateTime)
    {
        return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
    }
}