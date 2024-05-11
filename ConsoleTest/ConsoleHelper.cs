using System.Text;

public static class ConsoleHelper {
    private const string FilledChar = "█";
    private const string UnfilledChar = "▒";
    private const int Width = 50;

    public static string PrintLoading(double progress) {
        progress = Math.Clamp(progress, 0, 1);
        var filledPartsCount = (int)Math.Truncate(Width * progress);
        var filledPart = Enumerable.Repeat(FilledChar, filledPartsCount);
        var unfilledPart = Enumerable.Repeat(UnfilledChar, Width - filledPartsCount);

        return new StringBuilder()
            .Insert(0, FilledChar, filledPartsCount)
            .Insert(filledPartsCount, UnfilledChar, Width - filledPartsCount)
            .ToString();
    }

    public static string PrintRange(double value) {
        value = Math.Clamp(value, -1, 1);
        var multiplier = 1 / (Width / 2d - 1);
        var builder = new StringBuilder();

        var midIndex = Width / 2;

        if (value > 0) {
            builder.Insert(0, UnfilledChar, midIndex);

            for (var i = midIndex; i < Width; i++) {
                if (value >= (i - midIndex) * multiplier) {
                    builder.Append(FilledChar);
                } else {
                    builder.Append(UnfilledChar);
                }
            }
        }

        if (value < 0) {
            value = Math.Abs(value);

            for (var i = 0; i < midIndex; i++) {
                if (value >= (midIndex - 1 - i) * multiplier) {
                    builder.Append(FilledChar);
                } else {
                    builder.Append(UnfilledChar);
                }
            }

            builder.Insert(midIndex, UnfilledChar, midIndex);
        }

        return builder.ToString();
    }
}
