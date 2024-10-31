using DNQ.DataFeed.Domain.Common.Exceptions;

namespace DNQ.DataFeed.Domain.Sites;

public class Site
{
    public Guid Id { get; private set; }
    public string Code { get; internal set; } = null!;    /* Has the rule */
    public string Name { get; internal set; } = null!;    /* Has the rule */
    public bool Deleted { get; private set; }
    private Site() { }
    internal Site(string code, string name, Guid? id = null)
    {
        SetCode(code);
        SetName(name);
        Id = id ?? Guid.NewGuid();
    }
    internal void SetCode(string newCode)
    {
        if (string.IsNullOrEmpty(newCode)) throw new DomainException("Site code cannot be empty.");
        Code = newCode;
    }

    internal void SetName(string newName)
    {
        if (string.IsNullOrEmpty(newName)) throw new DomainException("Site name cannot be empty.");
        Name = newName;
    }
}
