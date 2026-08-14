using System.Windows;
using System.Windows.Controls;
using RiskApp.Models;

namespace RiskApp.UserControls;

public partial class Step3RiskEvaluation : UserControl
{
    private List<RiskItem> _risks = new();

    public List<RiskItem> Risks => _risks;

    public Step3RiskEvaluation()
    {
        InitializeComponent();
    }

    public void LoadRisks(List<RiskItem> risks)
    {
        _risks = risks;
        if (_risks.Count == 0)
        {
            TxtNoRisks.Visibility = Visibility.Visible;
            LstRisks.Visibility = Visibility.Collapsed;
        }
        else
        {
            TxtNoRisks.Visibility = Visibility.Collapsed;
            LstRisks.Visibility = Visibility.Visible;
            LstRisks.ItemsSource = null;
            LstRisks.ItemsSource = _risks;
        }
    }
}
