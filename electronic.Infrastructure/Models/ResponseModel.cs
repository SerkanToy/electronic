namespace electronic.Infrastructure.Models
{
    public class ResponseModel<T>
    {
        public ResponseModel(T Data, List<T> DataList, bool IsSuccess, List<string> Message)
        {
            this.Data = Data;
            this.DataList = DataList;
            this.IsSuccess = IsSuccess;
            this.Message = Message;
        }
        public T Data { get; set; }
        public List<T> DataList { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Message { get; set; }
    }
}
