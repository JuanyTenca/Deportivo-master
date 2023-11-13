using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportivo.Entidades.Entidades
{
    public class Localidad : ICloneable
    {
        public int LocalidadId { get; set; }

        public string NombreLocalidad { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
