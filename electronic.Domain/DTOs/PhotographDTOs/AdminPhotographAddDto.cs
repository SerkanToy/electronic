using Microsoft.AspNetCore.Http;

namespace electronic.Domain.DTOs.PhotographDTOs
{
    public class AdminPhotographAddDto
    {
        public bool IsMain { get; set; }
        public int Orderby { get; set; }
        public string ProductId { get; set; }
        public List<IFormFile> file { get; set; }
    }

    public class AdminPhotographAddDtos
    {
        public bool IsMain { get; set; }
        public int Orderby { get; set; }
        public string ProductId { get; set; }
        public IFormFile file { get; set; }
    }
}
