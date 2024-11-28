using DNQ.DataFeed.Domain.Accounts;
using DNQ.DataFeed.Domain.Common.Exceptions;
using DNQ.DataFeed.Domain.Common.Interfaces;
using System.Linq.Expressions;

namespace DNQ.DataFeed.Domain.Transactions;

public class TransactionManager
{
    private readonly IAccountRepo _accountRepo;
    private readonly ITransactionRepo _transactionRepo;
    private readonly List<Guid> allowFutureDates = new() { Guid.Parse("XXXX-XXXX-XXXX-XXXX") };

    public TransactionManager(IAccountRepo accountRepo, ITransactionRepo transactionRepo)
    {
        _accountRepo = accountRepo;
        _transactionRepo = transactionRepo;
    }

    public async Task<Transaction> CreateAsync(Guid platformId, Guid fileID, string transactionType, string referenceValue, DateTime effectiveDate, string processingStatus, string processingReason, DateTime transactionDate, decimal transactionAmount, string narrationText, int institutionID, string transactionReference, string reversedTransactionReference, string transactionCode, string referenceCode, int recordOrder, DateTime loadedDate, string transactionHashValue, string currency, decimal exchangeRate, string exchangeRateSource, decimal nativeCurrencyAmount, string suppliedSecurityCode)
    {
        /*
         * allow future date
         * find linked accounts, if found, transaction-date should be inside account.start-date -> account.end-date.
         * find existing transaction with same reference, 
         */
        if (transactionDate > DateTime.Today && !allowFutureDates.Contains(platformId))
            throw new BussinessException("Cannot load future date.");

        Expression<Func<Account, bool>> queryReferenceValue = x => x.PlatformId == platformId && x.ReferenceValue == referenceValue;
        var accounts = await _accountRepo.ListAsync(queryReferenceValue, null, null, null);
        if (accounts != null) 
        {
            bool foundAtLeastOneValidDateRange = false;

            foreach (var account in accounts)
            {
                if (account.EndDate != null)
                {
                    foundAtLeastOneValidDateRange = transactionDate >= account.StartDate && transactionDate <= account.EndDate;
                }
                else
                {
                    foundAtLeastOneValidDateRange = transactionDate >= account.StartDate;
                }

                if (foundAtLeastOneValidDateRange)
                    break;
            }

            if (!foundAtLeastOneValidDateRange)
                throw new BussinessException("Transaction date is outside start-date and end-date.");
        }

        Expression<Func<Transaction, bool>> queryAnotherActiveTranHasSameTransactionReference = 
            x => x.PlatformId == platformId 
            && x.TransactionReference == transactionReference
            && x.ProcessingStatus != TransactionStatus.Reversed
            && x.ProcessingStatus != TransactionStatus.Ignore
            && x.ProcessingStatus != TransactionStatus.Deleted
            && x.ProcessingStatus != TransactionStatus.Check;

        var foundExisitingTran = await _transactionRepo.ExistsAsync(queryAnotherActiveTranHasSameTransactionReference);

        if (!foundExisitingTran)
            throw new BussinessException("Transaction date is loaded.");

        return Transaction.Create(platformId, fileID, Guid.Empty, transactionType, referenceValue, effectiveDate, processingStatus, processingReason, transactionDate, transactionAmount, narrationText, institutionID, transactionReference, reversedTransactionReference, transactionCode, referenceCode, recordOrder, loadedDate, transactionHashValue, currency, exchangeRate, exchangeRateSource, nativeCurrencyAmount, suppliedSecurityCode);
    }
}
