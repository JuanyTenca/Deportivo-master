using Deportivo.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportivo.Comun.Interfaces
{
    public interface IRepositorioRoles
    {
        void Agregar(Rol rol);
        void Borrar(int rolId);
        void Editar(Rol rol);
        bool Existe(Rol rol);
        bool EstaRelacionado(Rol rol);
        int GetCantidad(string textoFiltro);
        List<Rol> GetRoles(string textoFiltro);
        List<Rol> GetRolesPorPagina(int cantidad, int paginaActual, string textoFiltro);
        Rol GetRolPorId(int rolId);

    }
}
