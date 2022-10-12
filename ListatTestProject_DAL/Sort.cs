using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ListatTestProject_DAL
{
    public static class Sort
    {
        public static IQueryable<TEntity> OrderBy<TEntity>(
            this IQueryable<TEntity> source,
            string orderByProperty,
            bool desc)
        {
            if(string.IsNullOrEmpty(orderByProperty) || (orderByProperty.ToLower() != "createddt" && orderByProperty.ToLower() != "price"))
            {
                orderByProperty = "Id";
            }
            string command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = string.IsNullOrEmpty(orderByProperty) ? type.GetProperty("Id") : type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(
                typeof(Queryable),
                command,
                new Type[]
                {
                    type,
                    property.PropertyType
                },
                source.Expression, 
                Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}
