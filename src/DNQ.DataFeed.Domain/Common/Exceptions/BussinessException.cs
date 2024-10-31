namespace DNQ.DataFeed.Domain.Common.Exceptions;

public class BussinessException: Exception
{
    public BussinessException(string message) : base(message)
    {
    }
}
