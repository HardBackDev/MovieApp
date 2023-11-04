using Shared.Exceptions;
using Shared.RequestFeatures;

namespace Repositories.Queries
{
    public static class SharedQuery
    {
        public const string PaggingQuery = $"""
            OFFSET ((@{nameof(RequestParameters.PageNumber)} - 1) * @{nameof(RequestParameters.PageSize)}) ROWS 
            FETCH NEXT @{nameof(RequestParameters.PageSize)} ROWS ONLY
            """;

        static Dictionary<string, string> OrderByQueriesCache = new();
        public static string GetOrderByQuery(Type entityColumnsType, string direction, string propertyName, string alias = null)
        {
            string aliasDot = string.IsNullOrEmpty(alias) ? "" : alias + '.';
            string dir;
            if (direction is not null && direction.Equals("ASC", StringComparison.OrdinalIgnoreCase))
                dir = "ASC";
            else
                dir = "DESC";
            string orderByStatement = "";

            if (!string.IsNullOrEmpty(propertyName))
            {
                if (OrderByQueriesCache.TryGetValue(propertyName, out string orderStatement))
                {
                    orderByStatement = orderStatement;
                }
                else if (entityColumnsType.GetFields().Any(p => p.Name.Equals(propertyName)))
                {
                    orderByStatement = @$"ORDER BY {entityColumnsType.GetField(propertyName).GetValue(null)}";
                    OrderByQueriesCache.Add(propertyName, orderByStatement);
                }
                else
                {
                    throw new BadRequestException($"column {propertyName} not exist in {entityColumnsType.Name}");
                }

                orderByStatement = orderByStatement.Replace("ORDER BY ", "ORDER BY " + aliasDot) + $" {dir}";
                return orderByStatement;
            }

            return "";
        }
    }
}
