using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportivo.Entidades.Entidades
{
    public class Empleado : ICloneable
    {

        public int EmpleadoId { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string NroDocumento { get; set; }

        public int LocalidadId { get; set; }

        //Localidad Localidad { get; set; }

        public string NroTelefono { get; set; }

        public string Email { get; set; }

        public int RolId { get; set; }

        //Rol Rol { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
