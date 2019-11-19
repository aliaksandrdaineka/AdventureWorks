using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AdventureWorks.Services
{
    public class DocumentsService : IDocumentsService
    {
        private readonly IConfiguration _configuration;
        private readonly CloudBlobClient _blobClient;

        public DocumentsService(IConfiguration configuration)
        {
            _configuration = configuration;
            var credentials = new StorageCredentials(_configuration["StorageName"], _configuration["StorageKey"]);
            _blobClient = new CloudBlobClient(new Uri(_configuration["BlobStorageUri"]), credentials);
        }

        public async Task<string> Save(Stream stream)
        {
            var container = _blobClient.GetContainerReference(_configuration["StorageDocumentsContainerName"]);
            await container.CreateIfNotExistsAsync();

            var documentId = Guid.NewGuid().ToString();
            var blob = container.GetBlockBlobReference(documentId);
            await blob.UploadFromStreamAsync(stream);

            return documentId;
        }
    }
}
