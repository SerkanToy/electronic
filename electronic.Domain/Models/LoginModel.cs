namespace electronic.Domain.Models
{
    public class LoginModel
    {
        public bool Status { get; set; }
        public object Data { get; set; }
        public string TokenJwt { get; set; }
        public object Errors { get; set; }
    }
}
