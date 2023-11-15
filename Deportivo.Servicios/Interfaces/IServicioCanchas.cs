using Deportivo.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportivo.Servicios.Interfaces
{
    public interface IServicioCanchas
    {

        void Guardar(Cancha cancha);
        void Borrar(int canchaId);
        bool Existe(Cancha cancha);
        bool EstaRelacionado(Cancha cancha);
        int GetCantidad(string textoFiltro);
        List<Cancha> GetCanchas(string textoFiltro);
        List<Cancha> GetCanchasPorPagina(int cantidad, int paginaActual, string textoFiltro);
        Cancha GetCanchaPorId(int canchaId);

    }
}
