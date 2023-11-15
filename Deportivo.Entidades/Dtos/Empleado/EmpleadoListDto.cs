using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportivo.Entidades.Dtos.Empleado
{
    public class EmpleadoListDto : ICloneable
    {
        public int EmpleadoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NroDocumento { get; set; }
        public string NombreLocalidad { get; set; }
        public string NroTelefono { get; set; }
        public string NombreRol { get; set; }
        public string Email { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
