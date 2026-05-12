namespace electronic.Domain.Models
{
    public class ResponseModel
    {
        public bool status { get; set; }
        public string title { get; set; } 
        public string message { get; set; }
        public string details { get; set; }
        public bool isHtmlEnabled { get; set; }
        public bool displayByDefault { get; set; }
        public bool showWithToastr { get; set; }
        public object data { get; set; }
        public IEnumerable<string> errors { get; set; }

      

    }
}
