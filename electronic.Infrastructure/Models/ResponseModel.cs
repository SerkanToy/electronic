namespace electronic.Infrastructure.Models
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public List<string>? Message { get; set; }
    }

    public class ResponseModel
    {
        public ResponseModel()
        {
            Message = new List<string>();
        }
        public object Data { get; set; }
        public bool IsSuccess { get; set; }
        public List<string>? Message { get; set; }
    }

}
