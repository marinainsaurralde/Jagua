using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jagua.Models
{
    public class Caja
    {
        [Key]//propiedad que identifica que Id_caja es el PK
        public int Id_caja { get; set; }
        public string Descripcion { get; set; }
        public int Id_usuario { get; set; }
    }
}