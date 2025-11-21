using DLTD.GestionPm.Dto.Request.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Interfaces
{
    public interface IAzureBlobService
    {
        Task SaveResourceBlob(AzureBlobRequest request);
    }
}
