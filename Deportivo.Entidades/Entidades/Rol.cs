using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportivo.Entidades.Entidades
{
    public class Rol : ICloneable
    {

        public int RolId { get; set; }

        public string Descripcion { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
