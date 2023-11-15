using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportivo.Entidades.Entidades
{
    public class Cancha: ICloneable
    {
        public int CanchaId { get; set; }

        public string NombreCancha { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
