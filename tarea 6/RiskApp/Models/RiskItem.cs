using LiteDB;

namespace RiskApp.Models;

public class RiskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Consequences { get; set; } = string.Empty;
    public int Probability { get; set; } = 1;
    public int Impact { get; set; } = 1;
    public string MitigationPlan { get; set; } = string.Empty;

    public int RiskScore => Probability * Impact;

    public string RiskLevel
    {
        get
        {
            int score = RiskScore;
            if (score <= 4) return "Bajo";
            if (score <= 9) return "Medio";
            if (score <= 16) return "Alto";
            return "Crítico";
        }
    }

    public string RiskColor
    {
        get
        {
            int score = RiskScore;
            if (score <= 4) return "#4CAF50";
            if (score <= 9) return "#FFC107";
            if (score <= 16) return "#FF9800";
            return "#F44336";
        }
    }
}
