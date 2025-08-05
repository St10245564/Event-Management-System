namespace MyPart3.Services
{
    public interface IBlobService
    {
        Task<string> UploadFileAsync(IFormFile file);
    }
}

