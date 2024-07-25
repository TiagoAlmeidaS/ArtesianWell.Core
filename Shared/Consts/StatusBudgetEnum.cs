namespace Shared.Consts;

public enum StatusBudgetEnum
{
    WatingForAction,
    Accepted,
    Rejected
}

public class StatusBudgetConsts
{
    public const string WatingForAction = "WatingForAction";
    public const string Accepted = "Accepted";
    public const string Rejected = "Rejected";
    
    public static StatusBudgetEnum stringToEnum(string statusBudget)
    {
        return statusBudget switch
        {
            "0" => StatusBudgetEnum.WatingForAction,
            "1" => StatusBudgetEnum.Accepted,
            "2" => StatusBudgetEnum.Rejected,
            _ => throw new ArgumentOutOfRangeException(nameof(statusBudget), statusBudget, null)
        };
    }
    
    public static string GetStatusBudgetEnum(StatusBudgetEnum statusBudgetEnum)
    {
        return statusBudgetEnum switch
        {
            StatusBudgetEnum.WatingForAction => WatingForAction,
            StatusBudgetEnum.Accepted => Accepted,
            StatusBudgetEnum.Rejected => Rejected,
            _ => throw new ArgumentOutOfRangeException(nameof(statusBudgetEnum), statusBudgetEnum, null)
        };
    }
}