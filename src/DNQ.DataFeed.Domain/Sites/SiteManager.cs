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
            throw new BussinessException("Cannot have two sites with the same code.");
        }

        if (await _siteRepo.ExistsAsync(SiteSpecifications.HasName(name)))
        {
            throw new BussinessException("Cannot have two sites with the same name.");
        }

        return new Site(code, name);
    }
    public async Task UpdateAsync(Site updateSite, [Required] string code, [Required] string name)
    {
        if (await _siteRepo.ExistsAsync(x => x.Code == code && x.Id != updateSite.Id))
        {
            throw new BussinessException("Cannot have two sites with the same code.");
        }

        if (await _siteRepo.ExistsAsync(x => x.Name == name && x.Id != updateSite.Id))
        {
            throw new BussinessException("Cannot have two sites with the same name.");
        }

        updateSite.SetName(name);
        updateSite.SetCode(code);
    }
}
