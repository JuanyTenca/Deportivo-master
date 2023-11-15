using Deportivo.Entidades.Dtos.Empleado;
using Deportivo.Entidades.Dtos.Socio;
using Deportivo.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportivo.Comun.Interfaces
{
    public interface IRepositorioEmpleados
    {
        void Borrar(int empleadoId);
        void Editar(Empleado empleado);
        bool Existe(Empleado empleado);
        bool EstaRelacionado(Empleado empleado);
        int GetCantidad(int? localidadId, int? rolId);
        List<EmpleadoListDto> GetEmpleadosPorPagina(int registrosPorPagina, int paginaActual, int? localidadId, int? rolId);
        void Agregar(Empleado empleado);
        Empleado GetEmpleadoPorId(int empleadoId);
        List<EmpleadoListDto> GetEmpleados(int? localidadId, int? rolId);
    }
}
