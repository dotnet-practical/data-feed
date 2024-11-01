using System.Linq.Expressions;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DNQ.DataFeed.Persistence.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string? sortBy)
    {
        if (string.IsNullOrEmpty(sortBy))
            return source;

        var orderParams = sortBy.Split('-');
        bool firstOrder = true;

        foreach (var param in orderParams)
        {
            var orderPair = param.Split(':');
            var propertyName = orderPair[0];
            var direction = orderPair.Length > 1 ? orderPair[1] : "asc";

            source = source.OrderByProperty(propertyName, direction, firstOrder);
            firstOrder = false;
        }

        return source;
    }

    private static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName, string direction, bool firstOrder)
    {
        var entityType = typeof(T);

        // Kiểm tra thuộc tính có tồn tại hay không
        var propertyInfo = entityType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (propertyInfo == null)
            throw new ArgumentException($"Thuộc tính '{propertyName}' không tồn tại trên kiểu '{entityType.Name}'.");

        var parameter = Expression.Parameter(entityType, "x");
        var property = Expression.Property(parameter, propertyInfo);
        var lambda = Expression.Lambda(property, parameter);

        string methodName = "";

        if (firstOrder)
        {
            methodName = direction.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";
        }
        else
        {
            methodName = direction.ToLower() == "desc" ? "ThenByDescending" : "ThenBy";
        }

        var resultExpression = Expression.Call(typeof(Queryable), methodName,
            new Type[] { entityType, property.Type },
            source.Expression, Expression.Quote(lambda));

        return source.Provider.CreateQuery<T>(resultExpression);
    }

    public static IQueryable<T> Paging<T>(this IQueryable<T> source, int? page, int? pageSize)
    {
        if (page == null || pageSize == null)
            return source;

        source = source.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

        return source;
    }
}
