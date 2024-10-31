using DNQ.DataFeed.Domain.Common.Exceptions;
using DNQ.DataFeed.Domain.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DNQ.DataFeed.Domain.Sites;

public class SiteManager 
{
    private readonly ISiteRepo _siteRepo;
    public SiteManager(ISiteRepo siteRepo) 
    {
        _siteRepo = siteRepo;
    }

    public async Task<Site> CreateAsync([Required]string code, [Required]string name)
    {
        if (await _siteRepo.ExistsAsync(SiteSpecifications.HasCode(code)))
        {
            throw new DomainException("Cannot have two sites with the same code.");
        }

        if (await _siteRepo.ExistsAsync(SiteSpecifications.HasName(name)))
        {
            throw new DomainException("Cannot have two sites with the same name.");
        }

        return new Site(code, name);
    }
}
