﻿using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Shopping.Helpers.Interfaces;

namespace Shopping.Helpers
{
	public class BlobHelper : IBlobHelper
	{
		private readonly CloudBlobClient _cloudBlobClient;
		public BlobHelper(IConfiguration configuration)
        {
			string keys = configuration["Blob:ConnectionString"];
			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(keys);
			_cloudBlobClient = storageAccount.CreateCloudBlobClient();
		}
        public async Task DeleteBlobAsync(Guid id, string containerName)
		{
			CloudBlobContainer container = _cloudBlobClient.GetContainerReference(containerName);
			CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{id}");
			await blockBlob.DeleteAsync();
		}

		public async Task<Guid> UploadBlobAsync(IFormFile file, string containerName)
		{
			Stream stream = file.OpenReadStream();
			return await UploadBlobAsync(stream, containerName);
			
		}
        public async Task<Guid> UploadBlobAsync(byte[] file, string containerName)
		{
			MemoryStream stream = new MemoryStream(file);
            return await UploadBlobAsync(stream, containerName);
        }

		public async Task<Guid> UploadBlobAsync(string image, string containerName)
		{

            Stream stream = File.OpenRead(image);
            return await UploadBlobAsync(stream, containerName);
        }
        private async Task<Guid> UploadBlobAsync(Stream stream, string containerName)
        {
            Guid name = Guid.NewGuid();
            CloudBlobContainer container = _cloudBlobClient.GetContainerReference(containerName);
            CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference($"{name}");
            await cloudBlockBlob.UploadFromStreamAsync(stream);
            return name;
        }
    }
}
