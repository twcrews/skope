namespace Skope.Widgets;

public record CounterViewModel(
    double Value,
    string FormattedValue,
    string? Subtitle,
    bool TrendUp,
    string Unit = "");

public record ChartSeriesViewModel(string Name, double[] Data);

public record SeriesChartViewModel(string[] Labels, ChartSeriesViewModel[] Series);

public record DonutChartViewModel(string[] Labels, double[] Data, string[]? Colors = null);
