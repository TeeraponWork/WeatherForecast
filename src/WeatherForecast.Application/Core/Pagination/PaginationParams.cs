namespace WeatherForecast.Application.Core.Pagination
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50; // ป้องกันการขอข้อมูลมากเกินไป
        private int _pageSize = 10; // ค่าตั้งต้นของจำนวนต่อหน้า

        public int PageNumber { get; set; } = 1; // หน้าปัจจุบัน
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value; // จำกัดขนาดสูงสุด
        }
    }
}
