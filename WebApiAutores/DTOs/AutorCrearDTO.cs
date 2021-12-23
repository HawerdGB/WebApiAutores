using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.Validaciones;

namespace WebApiAutores.DTOs
{
    public class AutorCrearDTO
    {
        [Required(ErrorMessage = "El campo {0} es requeriod")]
        [PrimeraLetraMayuscula]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe tener mas de (120) caracteres")]
        public string Nombre { get; set; }
    }
}
