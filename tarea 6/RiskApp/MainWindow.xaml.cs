using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using RiskApp.Models;
using RiskApp.Services;
using RiskApp.UserControls;

namespace RiskApp;

public partial class MainWindow : Window
{
    private int _currentStep = 1;
    private readonly List<Border> _stepIndicators;
    private readonly List<TextBlock> _stepLabels;

    public MainWindow()
    {
        InitializeComponent();

        _stepIndicators = new List<Border> { Step1Indicator, Step2Indicator, Step3Indicator, Step4Indicator };
        _stepLabels = new List<TextBlock> { Step1Label, Step2Label, Step3Label, Step4Label };

        BtnNext.Click += BtnNext_Click;
        BtnBack.Click += BtnBack_Click;
        BtnSave.Click += BtnSave_Click;
        BtnNewEval.Click += BtnNewEval_Click;
        BtnHistory.Click += BtnHistory_Click;
    }

    private void BtnNext_Click(object sender, RoutedEventArgs e)
    {
        if (_currentStep == 1)
        {
            if (string.IsNullOrWhiteSpace(Step1.ProjectName) || string.IsNullOrWhiteSpace(Step1.EvaluatorName))
            {
                MessageBox.Show("Complete el nombre del proyecto y el evaluador.", "Validación",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Step2.SetRisks(new List<RiskItem>());
        }
        else if (_currentStep == 2)
        {
            if (Step2.Risks.Count == 0)
            {
                MessageBox.Show("Agregue al menos un riesgo.", "Validación",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Step3.LoadRisks(Step2.Risks.ToList());
        }
        else if (_currentStep == 3)
        {
            Step4.LoadSummary(
                Step1.ProjectName,
                Step1.EvaluatorName,
                DateTime.Now,
                Step3.Risks);
        }
        else if (_currentStep == 4)
        {
            return;
        }

        if (_currentStep < 4)
        {
            _currentStep++;
            UpdateUI();
        }
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        if (_currentStep == 3)
        {
            Step2.SetRisks(Step3.Risks.ToList());
        }

        if (_currentStep > 1)
        {
            _currentStep--;
            UpdateUI();
        }
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var evaluation = new RiskEvaluation
            {
                ProjectName = Step1.ProjectName,
                EvaluatorName = Step1.EvaluatorName,
                CreatedDate = DateTime.Now,
                Risks = Step3.Risks
            };

            DatabaseService.SaveEvaluation(evaluation);
            MessageBox.Show("Evaluación guardada exitosamente.", "Éxito",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al guardar: {ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BtnNewEval_Click(object sender, RoutedEventArgs e)
    {
        var result = MessageBox.Show("¿Crear nueva evaluación? Se perderán los datos no guardados.",
            "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            _currentStep = 1;
            Step1.SetData(string.Empty, string.Empty);
            Step2.SetRisks(new List<RiskItem>());
            UpdateUI();
        }
    }

    private void BtnHistory_Click(object sender, RoutedEventArgs e)
    {
        var evaluations = DatabaseService.GetAllEvaluations();

        if (evaluations.Count == 0)
        {
            MessageBox.Show("No hay evaluaciones guardadas.", "Historial",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var items = evaluations.Select(ev =>
            $"{ev.CreatedDate:dd/MM/yyyy HH:mm} | {ev.ProjectName} | {ev.EvaluatorName} | {ev.Risks.Count} riesgos")
            .ToArray();

        var selected = ShowSelectionDialog("Historial de Evaluaciones", items);
        if (selected >= 0)
        {
            var ev = evaluations[selected];
            _currentStep = 4;
            Step1.SetData(ev.ProjectName, ev.EvaluatorName);
            Step2.SetRisks(ev.Risks);
            Step3.LoadRisks(ev.Risks);
            Step4.LoadSummary(ev.ProjectName, ev.EvaluatorName, ev.CreatedDate, ev.Risks);
            UpdateUI();
        }
    }

    private int ShowSelectionDialog(string title, string[] items)
    {
        var window = new Window
        {
            Title = title,
            Width = 600,
            Height = 400,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E1E2E")),
            Owner = this
        };

        var panel = new StackPanel { Margin = new Thickness(20) };

        var listbox = new ListBox
        {
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#313244")),
            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CDD6F4")),
            BorderThickness = new Thickness(0),
            FontSize = 13,
            Margin = new Thickness(0, 0, 0, 15)
        };

        foreach (var item in items)
        {
            listbox.Items.Add(item);
        }
        listbox.SelectedIndex = 0;

        var btnSelect = new Button
        {
            Content = "Seleccionar",
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#89B4FA")),
            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E1E2E")),
            FontWeight = FontWeights.Bold,
            Padding = new Thickness(20, 8, 20, 8),
            BorderThickness = new Thickness(0),
            HorizontalAlignment = HorizontalAlignment.Right,
            Cursor = System.Windows.Input.Cursors.Hand
        };

        int result = -1;
        btnSelect.Click += (_, _) =>
        {
            result = listbox.SelectedIndex;
            window.Close();
        };

        panel.Children.Add(listbox);
        panel.Children.Add(btnSelect);
        window.Content = panel;
        window.ShowDialog();

        return result;
    }

    private void UpdateUI()
    {
        var allSteps = new UserControl[] { Step1, Step2, Step3, Step4 };
        foreach (var step in allSteps)
            step.Visibility = Visibility.Collapsed;

        allSteps[_currentStep - 1].Visibility = Visibility.Visible;

        var activeColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#89B4FA"));
        var activeTextColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#89B4FA"));
        var inactiveColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#45475A"));
        var inactiveTextColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#585B70"));
        var doneColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4CAF50"));
        var doneTextColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4CAF50"));

        for (int i = 0; i < 4; i++)
        {
            var border = _stepIndicators[i];
            var label = _stepLabels[i];
            var tb = (TextBlock)border.Child;

            if (i + 1 == _currentStep)
            {
                border.Background = activeColor;
                tb.Foreground = activeTextColor;
                label.Foreground = activeTextColor;
            }
            else if (i + 1 < _currentStep)
            {
                border.Background = doneColor;
                tb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E1E2E"));
                label.Foreground = doneTextColor;
            }
            else
            {
                border.Background = inactiveColor;
                tb.Foreground = inactiveTextColor;
                label.Foreground = inactiveTextColor;
            }
        }

        BtnBack.Visibility = _currentStep > 1 ? Visibility.Visible : Visibility.Collapsed;
        BtnNext.Visibility = _currentStep < 4 ? Visibility.Visible : Visibility.Collapsed;
        BtnSave.Visibility = _currentStep == 4 ? Visibility.Visible : Visibility.Collapsed;
    }
}
