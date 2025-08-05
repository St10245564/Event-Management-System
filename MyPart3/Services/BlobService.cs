using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyPart3.Services
{
    using Azure.Storage.Blobs;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    namespace MyPart3.Services
    {
        using Azure.Storage.Blobs;
        using Microsoft.AspNetCore.Http;
        using System;
        using System.IO;
        using System.Threading.Tasks;
        using MyPart3.Services;

        namespace MyPart3.Services
        {
            public class BlobService : IBlobService
            {
                private readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=eneto;AccountKey=Dip6u3Y5NzYbesCPSMfU+3ja53Ln5uJLUnFvjZHD9ygbcIihBhjUCE8rHlSwboppVeStGM5fkpI4+AStkh3zdg==;EndpointSuffix=core.windows.net";
                private readonly string _containerName = "venue-images"; // use your actual container name

                public async Task<string> UploadFileAsync(IFormFile file)
                {
                    if (file == null || file.Length == 0)
                        throw new ArgumentException("File is empty or null");

                    var blobClient = new BlobContainerClient(_connectionString, _containerName);
                    await blobClient.CreateIfNotExistsAsync();

                    var blobName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var blob = blobClient.GetBlobClient(blobName);

                    using (var stream = file.OpenReadStream())
                    {
                        await blob.UploadAsync(stream, overwrite: true);
                    }

                    return blob.Uri.ToString(); 
                }
            }
        }
    }
}