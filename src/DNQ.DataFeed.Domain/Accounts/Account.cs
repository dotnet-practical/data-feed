using DNQ.DataFeed.Domain.Common.Exceptions;

namespace DNQ.DataFeed.Domain.Accounts;

public class Account
{
    public Guid Id { get; private set; }
    public Guid PlatformId { get; private set; } /* Has the rule */
    public Guid InternalId { get; private set; } /* Has the rule */
    public string ReferenceValue { get; private set; } = default!; /* Has the rule */
    public Guid SiteId { get; private set; }  /* Has the rule */
    public DateTime StartDate { get; private set; } /* Has the rule */
    public DateTime? EndDate { get; private set; } /* Has the rule */
    public uint FinYear { get; private set; } /* Has the rule */

    internal Account(Guid platformId, Guid siteId, Guid internalId, string referenceValue, DateTime startDate, DateTime? endDate, uint finYear)
    {
        Id = Guid.NewGuid();
        PlatformId = platformId;
        SiteId = siteId;
        InternalId = internalId;
        ReferenceValue = referenceValue;
        SetFinYear(finYear);
        SetStartDateEndDate(startDate, endDate);
    }
    internal void Update(Guid platformId, Guid siteId, Guid internalId, string referenceValue, DateTime startDate, DateTime? endDate, uint finYear)
    {
        PlatformId = platformId;
        SiteId = siteId;
        InternalId = internalId;
        ReferenceValue = referenceValue;
        SetFinYear(finYear);
        SetStartDateEndDate(startDate, endDate);
    }

    public void SetStartDateEndDate(DateTime startDate, DateTime? endDate)
    {
        if (endDate.HasValue && startDate > endDate.Value)
            throw new BussinessException("StartDate cannot be greater than or equal to EndDate.");
        
        StartDate = startDate;
        EndDate = endDate;
    }

    public void SetFinYear(uint finYear)
    {
        if (finYear == 0) throw new BussinessException("FinYear cannot be zero.");
        FinYear = finYear;
    }
}
