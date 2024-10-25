namespace DNQ.DataFeed.Domain.Sites;

public class Site
{
    public Guid Id { get; private set; }
    public string Code { get; private set; } /* Has the rule */
    public string Name { get; private set; } /* Has the rule */
    public bool Deleted { get; set; }
    protected Site() { }

    public Site(string code, string name, Guid? id = null)
    {
        SetCode(code);
        SetName(name);
        Id = id ?? Guid.NewGuid();
    }

    public void SetCode(string newCode)
    {
        /* TODO: throw error if found another site has same code */
        Code = newCode;
    }

    public void SetName(string newName)
    {
        /* TODO: throw error if found another site has same code */
        Name = newName;
    }
}
