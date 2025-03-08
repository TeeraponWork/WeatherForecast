using WeatherForecast.Application.Core.Pagination;

namespace WeatherForecast.Application.Core.Exceptions
{
    public static class QueryableExtensions
    {
        public static (List<T> Items, PagedResult Pagination) ToPagedList<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            var totalCount = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var pagedResult = new PagedResult(totalCount, pageNumber, pageSize);

            return (items, pagedResult); 
        }
    }
}
