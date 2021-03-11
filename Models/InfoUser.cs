using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_OpenIDConnect_DotNet.Models
{

    public class Eventos
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }

    }
    public class InfoUser
    {
        [Display(Name = "Nombre de usuario a buscar")]
        public string NombreUsuario { get; set; }
        [Display(Name = "¿Permisos delegados de usuario?")]

        public bool IsDelegated { get; set; }
        public List<Eventos> ListEventos { get; set; }
    }
}
