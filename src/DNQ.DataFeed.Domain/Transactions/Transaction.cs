namespace DNQ.DataFeed.Domain.Transactions;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid PlatformId { get; set; }
    public Guid FileID { get; set; }
    public Guid SiteID { get; set; }
    public string TransactionType { get; set; } = default!;
    public string ReferenceValue { get; set; } = default!;
    public DateTime EffectiveDate { get; set; }
    public string ProcessingStatus { get; set; } = default!;
    public string ProcessingReason { get; set; } = default!;
    public DateTime TransactionDate { get; set; }
    public decimal TransactionAmount { get; set; }
    public string NarrationText { get; set; } = default!;
    public int InstitutionID { get; set; }
    public string TransactionReference { get; set; } = default!;
    public string ReversedTransactionReference { get; set; } = default!;
    public string TransactionCode { get; set; } = default!;
    public string ReferenceCode { get; set; } = default!;
    public int RecordOrder { get; set; }
    public DateTime LoadedDate { get; set; }
    public string TransactionHashValue { get; set; } = default!;
    public string Currency { get; set; } = default!;
    public decimal ExchangeRate { get; set; }
    public string ExchangeRateSource { get; set; } = default!;
    public decimal NativeCurrencyAmount { get; set; }
    public string SuppliedSecurityCode { get; set; } = default!;

    private Transaction(Guid id, Guid platformId, Guid fileID, Guid siteID, string transactionType, string referenceValue, DateTime effectiveDate, string processingStatus, string processingReason, DateTime transactionDate, decimal transactionAmount, string narrationText, int institutionID, string transactionReference, string reversedTransactionReference, string transactionCode, string referenceCode, int recordOrder, DateTime loadedDate, string transactionHashValue, string currency, decimal exchangeRate, string exchangeRateSource, decimal nativeCurrencyAmount, string suppliedSecurityCode)
    {
        Id = id;
        PlatformId = platformId;
        FileID = fileID;
        SiteID = siteID;
        TransactionType = transactionType;
        ReferenceValue = referenceValue;
        EffectiveDate = effectiveDate;
        ProcessingStatus = processingStatus;
        ProcessingReason = processingReason;
        TransactionDate = transactionDate;
        TransactionAmount = transactionAmount;
        NarrationText = narrationText;
        InstitutionID = institutionID;
        TransactionReference = transactionReference;
        ReversedTransactionReference = reversedTransactionReference;
        TransactionCode = transactionCode;
        ReferenceCode = referenceCode;
        RecordOrder = recordOrder;
        LoadedDate = loadedDate;
        TransactionHashValue = transactionHashValue;
        Currency = currency;
        ExchangeRate = exchangeRate;
        ExchangeRateSource = exchangeRateSource;
        NativeCurrencyAmount = nativeCurrencyAmount;
        SuppliedSecurityCode = suppliedSecurityCode;
    }

    public static Transaction Create(Guid platformId, Guid fileID, Guid siteID, string transactionType, string referenceValue, DateTime effectiveDate, string processingStatus, string processingReason, DateTime transactionDate, decimal transactionAmount, string narrationText, int institutionID, string transactionReference, string reversedTransactionReference, string transactionCode, string referenceCode, int recordOrder, DateTime loadedDate, string transactionHashValue, string currency, decimal exchangeRate, string exchangeRateSource, decimal nativeCurrencyAmount, string suppliedSecurityCode)
    {
        return new Transaction(
            Guid.NewGuid(),
            platformId,
            fileID,
            siteID,
            transactionType,
            referenceValue,
            effectiveDate,
            processingStatus,
            processingReason,
            transactionDate,
            transactionAmount,
            narrationText,
            institutionID,
            transactionReference,
            reversedTransactionReference,
            transactionCode,
            referenceCode,
            recordOrder,
            loadedDate,
            transactionHashValue,
            currency,
            exchangeRate,
            exchangeRateSource,
            nativeCurrencyAmount,
            suppliedSecurityCode
        );
    }
}
