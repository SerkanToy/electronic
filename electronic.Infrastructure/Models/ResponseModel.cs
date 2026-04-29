namespace electronic.Infrastructure.Models
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public List<T> DataList { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Message { get; set; }
    }
}
