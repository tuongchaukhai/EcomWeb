namespace EcomWeb.Models
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
