using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Comun
{
    public class FechasDiff
    {
        public static TimeSpan CalcularDuracionFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return fechaFin.Subtract(fechaInicio);
        }

        public static double CalcularDuracionMinutos(DateTime fechaInicio, DateTime fechaFin)
        {
            return CalcularDuracionFechas(fechaInicio, fechaFin).TotalMinutes;
        }
    }
}
