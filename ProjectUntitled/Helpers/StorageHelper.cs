using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using ProjectUntitled.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProjectUntitled.Helpers
{
    public static class StorageHelper
    {
        public static async Task<CloudBlockBlob> UploadFileToStorage(Stream fileStream, string fileName, AzureStorageConfig _storageConfig)
        {
            string sasToken = GetAccountSASToken(_storageConfig);

            StorageCredentials accountSAS = new StorageCredentials(sasToken);

            Uri containerUri = GetContainerUri(_storageConfig.ClipsContainer, _storageConfig);

            CloudBlobContainer container = new CloudBlobContainer(containerUri, accountSAS);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            await blockBlob.UploadFromStreamAsync(fileStream);

            await Task.FromResult(true);

            return blockBlob;
        }

        private static Uri GetContainerUri(string containerName, AzureStorageConfig _storageConfig)
        {
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(_storageConfig);
            return storageAccount.CreateCloudBlobClient().GetContainerReference(containerName).Uri;
        }

        private static string GetAccountSASToken(AzureStorageConfig _storageConfig)
        {
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(_storageConfig);

            SharedAccessAccountPolicy policy = new SharedAccessAccountPolicy()
            {
                Permissions = SharedAccessAccountPermissions.Read | SharedAccessAccountPermissions.Write | SharedAccessAccountPermissions.List |
                SharedAccessAccountPermissions.Create | SharedAccessAccountPermissions.Delete,
                Services = SharedAccessAccountServices.Blob,
                ResourceTypes = SharedAccessAccountResourceTypes.Container | SharedAccessAccountResourceTypes.Object,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                Protocols = SharedAccessProtocol.HttpsOrHttp
            };

            string sasToken = storageAccount.GetSharedAccessSignature(policy);

            return sasToken;
        }

        private static CloudStorageAccount CreateStorageAccountFromConnectionString(AzureStorageConfig _storageConfig)
        {
            CloudStorageAccount storageAccount;

            storageAccount = CloudStorageAccount.Parse(_storageConfig.StorageConnectionString);

            return storageAccount;
        }
    }
}