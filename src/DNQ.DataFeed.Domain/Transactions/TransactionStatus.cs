namespace DNQ.DataFeed.Domain.Transactions;

public static class TransactionStatus
{
    public const string Ready = "READY";
    public const string Pending = "PENDING";
    public const string Pass = "PASS";
    public const string Fail = "FAIL";
    public const string Ignore = "IGNORE"; 
    public const string Quarantine = "QUARANTINE";
    public const string Reversed = "REVERSED";
    public const string Deleted = "DELETE";
    public const string Check = "CHECK";

    public static IReadOnlyList<string> AlreadyDeliveried = new List<string>
    {
        Ready,
        Pending,
        Pass,
        Fail
    };
}
