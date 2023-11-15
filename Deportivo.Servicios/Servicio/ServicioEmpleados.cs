using Deportivo.Comun;
using Deportivo.Datos;
using Deportivo.Entidades.Dtos.Empleado;
using Deportivo.Entidades.Entidades;
using Deportivo.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportivo.Servicios.Servicio
{
    public class ServicioEmpleados : IServicioEmpleados
    {
        public void Borrar(int empleadoId)
        {
            using (var unitOfWork = new UnitofWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {

                try
                {
                    unitOfWork.Socios.Borrar(empleadoId);
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork?.Rollback();

                    throw;
                }
            }
        }

        public bool EstaRelacionado(Empleado empleado)
        {
            using (var unitOfWork = new UnitofWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Empleados.EstaRelacionado(empleado);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool Existe(Empleado empleado)
        {
            using (var unitOfWork = new UnitofWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {


                try
                {
                    return unitOfWork.Empleados.Existe(empleado);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public int GetCantidad(int? localidadId, int? rolId)
        {
            using (var unitOfWork = new UnitofWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Empleados.GetCantidad(localidadId, rolId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public Empleado GetEmpleadoPorId(int empleadoId)
        {
            using (var unitOfWork = new UnitofWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Empleados.GetEmpleadoPorId(empleadoId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<EmpleadoListDto> GetEmpleados(int? localidadId, int? rolId)
        {
            using (var unitOfWork = new UnitofWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Empleados.GetEmpleados(localidadId, rolId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<EmpleadoListDto> GetEmpleadosPorPagina(int registrosPorPagina, int paginaActual, int? localidadId, int? rolId)
        {
            using (var unitOfWork = new UnitofWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Empleados
                        .GetEmpleadosPorPagina(registrosPorPagina,
                        paginaActual, localidadId, rolId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public void Guardar(Empleado empleado)
        {
            using (var unitOfWork = new UnitofWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    if (empleado.EmpleadoId == 0)
                    {
                        unitOfWork.Empleados.Agregar(empleado);
                    }
                    else
                    {
                        unitOfWork.Empleados.Editar(empleado);
                    }
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    unitOfWork.Dispose();
                    throw;
                }
            }
        }
    }
}
