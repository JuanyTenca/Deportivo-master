using Deportivo.Entidades.Dtos.Socio;
using Deportivo.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportivo.Comun.Interfaces
{
    public interface IRepositorioSocios
    {
        void Borrar(int socioId);
        void Editar(Socio socio);
        bool Existe(Socio socio);
        bool EstaRelacionado(Socio socio);
        int GetCantidad(int? localidadId);
        List<SocioListDto> GetSociosPorPagina(int registrosPorPagina, int paginaActual, int? localidadId);
        void Agregar(Socio socio);
        Socio GetSocioPorId(int socioId);
        List<SocioListDto> GetSocios(int? localidadId);
    }
}
