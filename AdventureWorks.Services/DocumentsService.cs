using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AdventureWorks.Services
{
    public class DocumentsService : IDocumentsService
    {
        private readonly IConfiguration _configuration;
        private readonly CloudBlobClient _blobClient;
        private readonly CloudQueueClient _queueClient;

        public DocumentsService(IConfiguration configuration)
        {
            _configuration = configuration;
            var credentials = new StorageCredentials(_configuration["StorageName"], _configuration["StorageKey"]);
            _blobClient = new CloudBlobClient(new Uri(_configuration["BlobStorageUri"]), credentials);
            _queueClient = new CloudQueueClient(new Uri(_configuration["QueueStorageUri"]), credentials);
        }

        public async Task<string> Save(Stream stream, string filename, string metadata)
        {
            var container = _blobClient.GetContainerReference(_configuration["StorageDocumentsContainerName"]);
            await container.CreateIfNotExistsAsync();

            var documentId = Guid.NewGuid().ToString();
            var blob = container.GetBlockBlobReference(documentId);
            await blob.UploadFromStreamAsync(stream);

            var queue = _queueClient.GetQueueReference(_configuration["StorageQueueContainerName"]);
            await queue.CreateIfNotExistsAsync();
            await queue.AddMessageAsync(new CloudQueueMessage($"Document Uploaded: {documentId}:{filename}:{metadata}"));
            return documentId;
        }
    }
}
