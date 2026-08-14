using LiteDB;

namespace RiskApp.Models;

public class RiskEvaluation
{
    [BsonId]
    public int Id { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string EvaluatorName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public List<RiskItem> Risks { get; set; } = new();
}
