using System.Text;

public static class ConsoleHelper {
    private const string FilledChar = "█";
    private const string UnfilledChar = "▒";

    public static string PrintLoading(double progress) {
        var filledPartsCount = (int)Math.Truncate(progress / 10d);
        var filledPart = Enumerable.Repeat(FilledChar, filledPartsCount);
        var unfilledPart = Enumerable.Repeat(UnfilledChar, 10 - filledPartsCount);

        return new StringBuilder()
            .Insert(0, FilledChar, filledPartsCount)
            .Insert(filledPartsCount, UnfilledChar, 10 - filledPartsCount)
            .ToString();
    }

    public static string PrintRange(double range) {
        var index = (int)(Math.Floor(range / 20) + 5);
        var indexRange = index < 5 ? (index, 4) : (5, index);
        var builder = new StringBuilder();

        for (var i = 0; i < 10; i++) {
           if (i >= indexRange.Item1 && i <= indexRange.Item2) {
                builder.Append(FilledChar);
           } else {
                builder.Append(UnfilledChar);
           }
        }

        return builder.ToString();
    }
}
