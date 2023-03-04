namespace POC.AWS.S3.API.Services.Interfaces
{
    public interface IFileStorageService
    {
        public Task UploadFile(IFormFile file);
        public Task DownloadFile(string fileId);
    }
}
