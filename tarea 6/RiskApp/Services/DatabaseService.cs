using LiteDB;
using RiskApp.Models;
using IOPath = System.IO.Path;

namespace RiskApp.Services;

public static class DatabaseService
{
    private static readonly string DbPath = IOPath.Combine(
        AppDomain.CurrentDomain.BaseDirectory, "riskapp.db");

    private static LiteDatabase GetDatabase() => new($"Filename={DbPath}");

    public static void SaveEvaluation(RiskEvaluation evaluation)
    {
        using var db = GetDatabase();
        var col = db.GetCollection<RiskEvaluation>("evaluations");
        if (evaluation.Id == 0)
        {
            col.Insert(evaluation);
        }
        else
        {
            col.Update(evaluation);
        }
    }

    public static List<RiskEvaluation> GetAllEvaluations()
    {
        using var db = GetDatabase();
        var col = db.GetCollection<RiskEvaluation>("evaluations");
        return col.FindAll().OrderByDescending(x => x.CreatedDate).ToList();
    }

    public static RiskEvaluation? GetEvaluation(int id)
    {
        using var db = GetDatabase();
        var col = db.GetCollection<RiskEvaluation>("evaluations");
        return col.FindById(id);
    }

    public static void DeleteEvaluation(int id)
    {
        using var db = GetDatabase();
        var col = db.GetCollection<RiskEvaluation>("evaluations");
        col.Delete(id);
    }
}
