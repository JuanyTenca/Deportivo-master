using Deportivo.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportivo.Comun
{
    public interface IRepositorioLocalidades
    {
        void Agregar(Localidad localidad);
        void Borrar(int localidadId);
        void Editar(Localidad localidad);
        bool Existe(Localidad localidad);
        bool EstaRelacionado(Localidad localidad);
        int GetCantidad(string textoFiltro);
        List<Localidad> GetLocalidades(string textoFiltro);
        List<Localidad> GetLocalidadesPorPagina(int cantidad, int paginaActual, string textoFiltro);
        Localidad GetLocalidadPorId(int localidadId);

    }
}
