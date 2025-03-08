using WeatherForecast.Application.Core.Pagination;

namespace WeatherForecast.Application.Core.Results
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string Error { get; set; }
        public PagedResult Pagination { get; set; }

        public static Result<T> Success(T value, PagedResult pagination)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Value = value,
                Pagination = pagination
            };
        }
        public static Result<T> Success(T value)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Value = value,
            };
        }
        public static Result<T> Failure(string error)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Error = error
            };
        }
        public static Result<T> Exception(Exception error)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Error = error.ToString()
            };
        }
    }
}
