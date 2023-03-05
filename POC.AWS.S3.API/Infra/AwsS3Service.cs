using Amazon.S3.Transfer;
using Amazon.S3;
using Amazon;
using POC.AWS.S3.API.Services.Interfaces;

namespace POC.AWS.S3.API.Infra
{
    public class AwsS3Service : IFileStorageService
    {
        private const string BUCKET_NAME = "poc-awss3-dotnet";
        private readonly IConfiguration _configuration;

        public AwsS3Service(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task UploadFile(IFormFile file)
        {
            using (AmazonS3Client client = GetClient())
            {
                using (MemoryStream ms = new())
                {
                    file.CopyTo(ms);

                    TransferUtilityUploadRequest uploadRequest = new()
                    {
                        InputStream = ms,
                        Key = file.FileName,
                        BucketName = BUCKET_NAME,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    TransferUtility fileTransferUtility = new(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }
            }
        }

        public async Task DownloadFile(string fileId)
        {
            using (AmazonS3Client client = GetClient())
            {
                TransferUtilityDownloadRequest request = new()
                {
                    BucketName = BUCKET_NAME,
                    FilePath = "arq_teste_sales.png"
                };

                TransferUtility fileTransferUtility = new(client);
                await fileTransferUtility.DownloadAsync(request);
            }
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
