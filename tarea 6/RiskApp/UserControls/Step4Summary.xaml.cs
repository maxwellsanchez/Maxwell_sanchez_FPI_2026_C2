using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using RiskApp.Models;

namespace RiskApp.UserControls;

public partial class Step4Summary : UserControl
{
    public Step4Summary()
    {
        InitializeComponent();
    }

    public void LoadSummary(string projectName, string evaluator, DateTime date, List<RiskItem> risks)
    {
        TxtProject.Text = projectName;
        TxtEvaluator.Text = evaluator;
        TxtDate.Text = date.ToString("dd/MM/yyyy");
        TxtTotal.Text = risks.Count.ToString();

        DgSummary.ItemsSource = risks;

        BuildDistribution(risks);
        BuildCategories(risks);
    }

    private void BuildDistribution(List<RiskItem> risks)
    {
        PnlDistribution.Children.Clear();

        var groups = new Dictionary<string, int>
        {
            { "Bajo", risks.Count(r => r.RiskLevel == "Bajo") },
            { "Medio", risks.Count(r => r.RiskLevel == "Medio") },
            { "Alto", risks.Count(r => r.RiskLevel == "Alto") },
            { "Crítico", risks.Count(r => r.RiskLevel == "Crítico") }
        };

        var colors = new Dictionary<string, string>
        {
            { "Bajo", "#4CAF50" },
            { "Medio", "#FFC107" },
            { "Alto", "#FF9800" },
            { "Crítico", "#F44336" }
        };

        int max = groups.Values.Max();
        if (max == 0) max = 1;

        foreach (var kv in groups)
        {
            var row = new Grid { Margin = new Thickness(0, 0, 0, 8) };
            row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });
            row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });

            var label = new TextBlock
            {
                Text = kv.Key,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colors[kv.Key])),
                FontSize = 12,
                FontWeight = FontWeights.SemiBold,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(label, 0);

            var barBg = new Border
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#45475A")),
                CornerRadius = new CornerRadius(4),
                Height = 18,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            Grid.SetColumn(barBg, 1);

            var barFill = new Border
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colors[kv.Key])),
                CornerRadius = new CornerRadius(4),
                Height = 18,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = Math.Max(4, (kv.Value / (double)max) * 150)
            };

            var barContainer = new Grid();
            barContainer.Children.Add(barBg);
            barContainer.Children.Add(barFill);

            var count = new TextBlock
            {
                Text = kv.Value.ToString(),
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CDD6F4")),
                FontSize = 12,
                FontWeight = FontWeights.Bold,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            Grid.SetColumn(count, 2);

            row.Children.Add(label);
            row.Children.Add(barContainer);
            row.Children.Add(count);
            Grid.SetColumn(barContainer, 1);

            PnlDistribution.Children.Add(row);
        }
    }

    private void BuildCategories(List<RiskItem> risks)
    {
        PnlCategories.Children.Clear();

        var groups = risks.GroupBy(r => r.Category)
                          .OrderByDescending(g => g.Count());

        foreach (var group in groups)
        {
            var row = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 8) };

            var border = new Border
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#45475A")),
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(8, 3, 8, 3),
                Margin = new Thickness(0, 0, 10, 0),
                VerticalAlignment = VerticalAlignment.Center
            };
            border.Child = new TextBlock
            {
                Text = group.Key,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#89B4FA")),
                FontSize = 12,
                FontWeight = FontWeights.SemiBold
            };

            var count = new TextBlock
            {
                Text = $"{group.Count()} riesgo(s)",
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A6ADC8")),
                FontSize = 12,
                VerticalAlignment = VerticalAlignment.Center
            };

            var avgScore = new TextBlock
            {
                Text = $"  |  Score prom: {group.Average(r => r.RiskScore):F1}",
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CDD6F4")),
                FontSize = 12,
                FontWeight = FontWeights.Bold,
                VerticalAlignment = VerticalAlignment.Center
            };

            row.Children.Add(border);
            row.Children.Add(count);
            row.Children.Add(avgScore);

            PnlCategories.Children.Add(row);
        }
    }
}
