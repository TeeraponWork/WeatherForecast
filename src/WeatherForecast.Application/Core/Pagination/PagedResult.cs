using WeatherForecast.Application.Core.Results;

namespace WeatherForecast.Application.Core.Pagination
{
    public class PagedResult
    {
        public int TotalCount { get; set; } // จำนวนรายการทั้งหมด
        public int PageNumber { get; set; } // หน้าปัจจุบัน
        public int PageSize { get; set; } // จำนวนต่อหน้า
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize); // คำนวณจำนวนหน้าทั้งหมด

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public PagedResult(int totalCount, int pageNumber, int pageSize)
        {
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
