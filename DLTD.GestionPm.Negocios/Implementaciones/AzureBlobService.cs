using Azure.Storage.Blobs;
using DLTD.GestionPm.Dto.Request.Azure;
using DLTD.GestionPm.Negocios.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Implementaciones
{
    public class AzureBlobService: IAzureBlobService
    {
        private readonly IConfiguration _config;

        public AzureBlobService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SaveResourceBlob(AzureBlobRequest request)
        {
            var configAzure = _config.GetSection("AzureSettings");
            var client = new BlobServiceClient(configAzure["ConnectionString"]);
            var container = client.GetBlobContainerClient(configAzure["NameContainer"]);
            var resource = container.GetBlobClient(request.Name);

            await using var stream = new MemoryStream(Convert.FromBase64String(request.Base64));
            await resource.UploadAsync(stream);
        }
    }
}
