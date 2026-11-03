using electronic.Application.Interfaces;
using electronic.Domain.DTOs.PhotographDTOs;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using static System.Net.WebRequestMethods;

namespace electronic.Infrastructure.Service
{
    public class PhotographUpload: IPhotographUpload
    {
        private readonly IWebHostEnvironment _env;
        public PhotographUpload(IWebHostEnvironment env)
        {
            this._env = env; ;
        }
        public async Task<(List<string>, bool)> UploadPhotograph(List<IFormFile> files)
        {
            try
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var fileExtension = "";
                List<string> fileUrl = new List<string>();
                foreach (var file in files)
                {
                    fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                    if (!Array.Exists(allowedExtensions, e => e == fileExtension))
                    {
                        return (new List<string> { "Sadece .jpg, .jpeg, .png ve .webp formatları desteklenmektedir." }, false);
                    }
                }

                foreach (var file in files)
                {
                    fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                    var imageName = Guid.NewGuid().ToString();
                    var uniqueFileName = $"{imageName}{fileExtension}";
                    fileUrl.Add($"/ProductImages/{uniqueFileName}");

                    var uploadsFolder = Path.Combine(_env.WebRootPath, "ProductImages");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);



                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        foreach (var item in files)
                        {
                            await item.CopyToAsync(stream);
                        }
                    }
                }
                return (fileUrl, true);
            }
            catch (Exception ex)
            {                
                return (new List<string> { "Dosya yükleme sırasında bir hata oluştu" }, false);
            }
        }

        public async Task<(string, bool)> UploadPhotograph(IFormFile file)
        {
            try
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var fileExtension = "";

                fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!Array.Exists(allowedExtensions, e => e == fileExtension))
                {
                    return ("Sadece .jpg, .jpeg, .png ve .webp formatları desteklenmektedir.", false);
                }

                fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                var imageName = Guid.NewGuid().ToString();
                var uniqueFileName = $"{imageName}{fileExtension}";
                var uploadsFolder = Path.Combine(_env.WebRootPath, "ProductImages");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);



                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return ($"/ProductImages/{uniqueFileName}", true);

            }
            catch (Exception ex)
            {
                return ("Dosya yükleme sırasında bir hata oluştu", false);
            }
        }

    }
}
