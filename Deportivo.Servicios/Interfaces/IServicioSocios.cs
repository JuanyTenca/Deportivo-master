using Deportivo.Entidades.Dtos.Socio;
using Deportivo.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportivo.Servicios.Interfaces
{
    public interface IServicioSocios
    {
        void Borrar(int socioId);
        bool Existe(Socio socio);
        bool EstaRelacionado(Socio socio);
        int GetCantidad(int? localidadId);
        List<SocioListDto> GetSociosPorPagina(int registrosPorPagina, int paginaActual, int? localidadId);
        Socio GetSocioPorId(int socioId);
        List<SocioListDto> GetSocios(int? localidadId);
        void Guardar(Socio socio);

    }
}
