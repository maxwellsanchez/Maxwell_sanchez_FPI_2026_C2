using System.Windows;
using System.Windows.Controls;
using RiskApp.Models;

namespace RiskApp.UserControls;

public partial class Step2RiskIdentification : UserControl
{
    private readonly List<RiskItem> _risks = new();

    public List<RiskItem> Risks => _risks;

    public Step2RiskIdentification()
    {
        InitializeComponent();
        BtnAddRisk.Click += BtnAddRisk_Click;
        BtnClearForm.Click += BtnClearForm_Click;
        RefreshGrid();
    }

    public void SetRisks(List<RiskItem> risks)
    {
        _risks.Clear();
        _risks.AddRange(risks);
        RefreshGrid();
    }

    private void BtnAddRisk_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TxtDescription.Text))
        {
            MessageBox.Show("Ingrese una descripción del riesgo.", "Validación",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var categoryItem = CboCategory.SelectedItem as ComboBoxItem;
        var risk = new RiskItem
        {
            Description = TxtDescription.Text.Trim(),
            Category = categoryItem?.Content?.ToString() ?? "Operacional",
            Consequences = TxtConsequences.Text.Trim(),
            MitigationPlan = TxtMitigation.Text.Trim()
        };

        _risks.Add(risk);
        RefreshGrid();
        ClearForm();
    }

    private void BtnClearForm_Click(object sender, RoutedEventArgs e)
    {
        ClearForm();
    }

    private void BtnDeleteRisk_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.Tag is Guid id)
        {
            var risk = _risks.FirstOrDefault(r => r.Id == id);
            if (risk != null)
            {
                _risks.Remove(risk);
                RefreshGrid();
            }
        }
    }

    private void RefreshGrid()
    {
        DgRisks.ItemsSource = null;
        DgRisks.ItemsSource = _risks;
    }

    private void ClearForm()
    {
        TxtDescription.Text = string.Empty;
        TxtConsequences.Text = string.Empty;
        TxtMitigation.Text = string.Empty;
        CboCategory.SelectedIndex = 0;
    }
}
