using Skope.Data;

namespace Skope.Widgets;

public static class MockWidgetData
{
    public static CounterViewModel GetCounter(DataSource source) => source switch
    {
        DataSource.CheckInsWeeklyTotal => new CounterViewModel(
            847, "847", "▲ 8.4% vs last week", TrendUp: true, Unit: "check-ins"),

        DataSource.GivingWeeklyTotal => new CounterViewModel(
            24320.50, "$24,320", "▲ 12.1% vs last week", TrendUp: true, Unit: "this week"),

        _ => new CounterViewModel(0, "—", null, TrendUp: false)
    };

    public static SeriesChartViewModel GetSeriesChart(DataSource source) => source switch
    {
        DataSource.CheckInsByServiceTime => new SeriesChartViewModel(
            Labels: ["8:00 AM", "9:30 AM", "11:00 AM", "12:30 PM"],
            Series:
            [
                new("Adults", [210.0, 312.0, 289.0, 36.0]),
                new("Students", [45.0, 67.0, 71.0, 12.0]),
                new("Kids", [88.0, 134.0, 122.0, 14.0]),
            ]),

        DataSource.CheckInsWeeklyTrend => new SeriesChartViewModel(
            Labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
            Series: [new("Check-ins", [620.0, 588.0, 710.0, 695.0, 730.0, 712.0, 680.0, 750.0, 810.0, 847.0, 0.0, 0.0])]),

        DataSource.GivingByFund => new SeriesChartViewModel(
            Labels: ["General", "Missions", "Building", "Benevolence", "Youth"],
            Series: [new("This Month", [14200.0, 3800.0, 4100.0, 1200.0, 1020.0])]),

        DataSource.GivingMonthlyTrend => new SeriesChartViewModel(
            Labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
            Series: [new("Giving", [18400.0, 16200.0, 21500.0, 19800.0, 22100.0, 20400.0, 17600.0, 23200.0, 25100.0, 24320.0, 0.0, 0.0])]),

        _ => new SeriesChartViewModel(
            Labels: ["A", "B", "C"],
            Series: [new("Data", [1.0, 1.0, 1.0])])
    };

    public static DonutChartViewModel GetDonutChart(DataSource source) => source switch
    {
        DataSource.CheckInsByServiceTime => new DonutChartViewModel(
            Labels: ["8:00 AM", "9:30 AM", "11:00 AM", "12:30 PM"],
            Data: [210.0, 312.0, 289.0, 36.0]),

        DataSource.GivingByFund => new DonutChartViewModel(
            Labels: ["General", "Missions", "Building", "Benevolence", "Youth"],
            Data: [14200.0, 3800.0, 4100.0, 1200.0, 1020.0]),

        DataSource.VolunteersStatusBreakdown => new DonutChartViewModel(
            Labels: ["Confirmed", "Unconfirmed", "Declined", "No Response"],
            Data: [142.0, 38.0, 17.0, 53.0]),

        _ => new DonutChartViewModel(
            Labels: ["A", "B", "C"],
            Data: [1.0, 1.0, 1.0])
    };
}
