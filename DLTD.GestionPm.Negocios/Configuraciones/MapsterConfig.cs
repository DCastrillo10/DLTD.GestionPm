using DLTD.GestionPm.Dto.Request.Pm;
using DLTD.GestionPm.Entidad;
using Mapster;

namespace DLTD.GestionPm.Negocios.Configuraciones
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<PmRequest, Pm>.NewConfig()
                            .Ignore(dest => dest.PmDetalles);
        }
    }
}