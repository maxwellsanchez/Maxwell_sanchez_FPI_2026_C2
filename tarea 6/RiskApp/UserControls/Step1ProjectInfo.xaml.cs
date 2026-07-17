namespace RiskApp.UserControls;

public partial class Step1ProjectInfo : System.Windows.Controls.UserControl
{
    public string ProjectName => TxtProjectName.Text;
    public string EvaluatorName => TxtEvaluator.Text;

    public Step1ProjectInfo()
    {
        InitializeComponent();
        TxtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
    }

    public void SetData(string projectName, string evaluator)
    {
        TxtProjectName.Text = projectName;
        TxtEvaluator.Text = evaluator;
    }
}
