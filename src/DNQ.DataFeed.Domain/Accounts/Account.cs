using DNQ.DataFeed.Domain.Common.Exceptions;

namespace DNQ.DataFeed.Domain.Accounts;

public class Account
{
    public Guid Id { get; private set; }
    public Guid PlatformId { get; init; } /* Has the rule */
    public Guid InternalId { get; private set; }
    public string ReferenceValue { get; init; } = default!; /* Has the rule */
    public Guid SiteId { get; init; }  /* Has the rule */
    public DateTime StartDate { get; private set; } /* Has the rule */
    public DateTime? EndDate { get; private set; } /* Has the rule */
    public uint FinYear { get; init; }

    internal Account(Guid platformId, Guid siteId, Guid internalId, string referenceValue, DateTime startDate, DateTime? endDate, uint finYear)
    {
        Id = Guid.NewGuid();
        PlatformId = platformId;
        SiteId = siteId;
        InternalId = internalId;
        ReferenceValue = referenceValue;
        SetStartDate(startDate);
        if (endDate != null) SetEndDate(endDate.Value);
        FinYear = finYear;
    }

    public void SetStartDate(DateTime startDate)
    {
        if (EndDate != null && StartDate >= EndDate) throw new BussinessException("StartDate cannot be greater than EndDate.");
        StartDate = startDate;
    }

    public void SetEndDate(DateTime endDate)
    {
        if (StartDate >= EndDate) throw new BussinessException("StartDate cannot be greater than EndDate.");
        EndDate = endDate;
    }
}
