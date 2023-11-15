using Deportivo.Entidades.Dtos.Empleado;
using Deportivo.Entidades.Dtos.Socio;
using Deportivo.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportivo.Servicios.Interfaces
{
    public interface IServicioEmpleados
    {
        void Borrar(int empleadoId);
        bool Existe(Empleado empleado);
        bool EstaRelacionado(Empleado empleado);
        int GetCantidad(int? localidadId, int? rolId);
        List<EmpleadoListDto> GetEmpleadosPorPagina(int registrosPorPagina, int paginaActual, int? localidadId, int? rolId);
        Empleado GetEmpleadoPorId(int empleadoId);
        List<EmpleadoListDto> GetEmpleados(int? localidadId, int? rolId);
        void Guardar(Empleado empleado);
    }
}
