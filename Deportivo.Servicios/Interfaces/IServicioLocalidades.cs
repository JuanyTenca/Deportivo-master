using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deportivo.Entidades.Entidades;

namespace Deportivo.Servicios.Interfaces
{
    public interface IServicioLocalidades
    {

        void Guardar(Localidad localidad);
        void Borrar(int localidadId);
        bool Existe(Localidad localidad);
        bool EstaRelacionado(Localidad localidad);
        int GetCantidad(string textoFiltro);
        List<Localidad> GetLocalidades(string textoFiltro);
        List<Localidad> GetLocalidadesPorPagina(int cantidad, int paginaActual, string textoFiltro);
        Localidad GetLocalidadPorId(int localidadId);
    }

}
