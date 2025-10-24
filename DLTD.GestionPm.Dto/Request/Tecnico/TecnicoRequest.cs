﻿using DLTD.GestionPm.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.Tecnico
{
    public class TecnicoRequest
    {
        
        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string Nombres { get; set; } = null!;

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string Apellidos { get; set; } = null!;

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string Email { get; set; } = null!;

        public string? TurnoActual { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string NoIdentificacion { get; set; } = null!;

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string Telefono { get; set; } = null!;

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string Codigo { get; set; } = null!;

        public string? Especialidad { get; set; }

        public int? IdUsuario { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string Clave { get; set; } = null!;

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        [Compare(nameof(Clave), ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarClave { get; set; } = null!;
    }
}
