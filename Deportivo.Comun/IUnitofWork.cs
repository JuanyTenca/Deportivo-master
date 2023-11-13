using Deportivo.Comun.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportivo.Comun
{
    public interface IUnitofWork : IDisposable
    {

        void BeginTransaction();
        void Commit();
        void Rollback();
        IRepositorioLocalidades Localidades { get; }
        IRepositorioSocios Socios { get; }
    }
}
