using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Api.Services.StorageServices
{
    public class BlobStorageService
    {
        private readonly BlobServiceClient BlobServiceClient;
        private readonly BlobContainerClient BlobContainerClient;

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            BlobServiceClient = blobServiceClient;
            BlobContainerClient = BlobServiceClient.GetBlobContainerClient("user-profile-picture-container");
        }

        public async Task<string> Upload([FromForm] IFormFile file)
        {
            var blobClient = BlobContainerClient.GetBlobClient(file.FileName);

            await using (var stream = file.OpenReadStream()) {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri.AbsoluteUri;
        }

        public async Task Delete(string url)
        {
            var fileName = new Uri(url).Segments.LastOrDefault();
            var blobClient = BlobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}
