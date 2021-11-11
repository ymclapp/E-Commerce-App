using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class AzureFileUploadService : IFileUploadService
    {
        public const string AccountName_Key = "AzureStorageAccountName";
        private readonly CloudBlobClient cloudBlobClient;

        public AzureFileUploadService(IConfiguration configuration)
        {
            var accountName = configuration[AccountName_Key] ?? throw new InvalidOperationException("Missing AzureStorageAccountName");
            var blobKey = configuration["AzureBlobKey"] ?? throw new InvalidOperationException("Missing AzureBlobKey");

            var storageCredentials = new StorageCredentials(accountName, blobKey);
            var storageAccount = new CloudStorageAccount(storageCredentials, true);

            cloudBlobClient = storageAccount.CreateCloudBlobClient();
        }

        public Task SetProductImage ( Product product, string url )
        {
            throw new NotImplementedException();
        }

        public async Task<string> Upload(IFormFile productImage)
        {
            //Access to a storage container
            var container = cloudBlobClient.GetContainerReference("ecommerceimages");
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                //Allow anonymous access to individual files "if you have the link"
                PublicAccess = BlobContainerPublicAccessType.Blob,
            });

            //Actually do the upload
            var blobFile = container.GetBlockBlobReference(productImage.FileName);

            //using = close the connection when we're done (IDisposable)
            using var imageStream = productImage.OpenReadStream();
            await blobFile.UploadFromStreamAsync(imageStream);
            return blobFile.Uri.ToString();
        }
    }
}
