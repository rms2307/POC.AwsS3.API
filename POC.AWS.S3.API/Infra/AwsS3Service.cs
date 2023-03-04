using Amazon.S3.Transfer;
using Amazon.S3;
using Amazon;
using POC.AWS.S3.API.Services.Interfaces;

namespace POC.AWS.S3.API.Infra
{
    public class AwsS3Service: IFileStorageService
    {
        private readonly IConfiguration _configuration;

        public AwsS3Service(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task UploadFile(IFormFile file)
        {
            using (var client = GetClient())
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = ms,
                        Key = file.FileName,
                        BucketName = "poc-awss3-dotnet",
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }
            }
        }

        public async Task DownloadFile(string fileId)
        {
            throw new NotImplementedException();
        }

        private AmazonS3Client GetClient()
        {
            return new AmazonS3Client(
                        _configuration["AwsS3:AwsAccessKeyId"],
                        _configuration["AwsS3:AwsSecretAccessKey"],
                        RegionEndpoint.USEast1);
        }
    }
}
