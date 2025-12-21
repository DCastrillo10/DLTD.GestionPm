using DLTD.GestionPm.Dto.Response.GrupoTrabajo;

namespace DLTD.GestionPm.UI.Services
{
    public class GrupoTrabajoEstado
    {
        public string NombresConcatenados {  get; set; } = string.Empty;
        public List<int> IdTecnicoEquipo { get; set; } = new();
        public event Action? OnChange;

        public void ActualizarGrupoTrabajo(List<ListaGrupoTrabajoResponse> vinculados, string nombrePrincipal, int idPrincipal)
        {
                       
            IdTecnicoEquipo = new List<int> { idPrincipal };
            //Adicionamos al resto de tecnicos
            IdTecnicoEquipo.AddRange(vinculados.Select(x => x.IdTecnicoVinculado));

            NombresConcatenados = string.Join(", ", vinculados.Select(x => x.NombresCompletos));
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
