using Deportivo.Entidades.Dtos.Socio;
using Deportivo.Entidades.Entidades;
using Deportivo.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deportivo.Datos;
using System.Configuration;
using Deportivo.Comun;

namespace Deportivo.Servicios.Servicio
{
    public class ServicioSocios : IServicioSocios
    {


        public ServicioSocios()
        {

        }

        public void Borrar(int socioId)
        {
            using (var unitOfWork = new UnitofWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {

                try
                {
                    unitOfWork.Socios.Borrar(socioId);
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork?.Rollback();

                    throw;
                }
            }
        }

        public bool EstaRelacionado(Socio socio)
        {
            using (var unitOfWork = new UnitofWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Socios.EstaRelacionado(socio);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool Existe(Socio socio)
        {
            using (var unitOfWork = new UnitofWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {


                try
                {
                    return unitOfWork.Socios.Existe(socio);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public int GetCantidad(int? localidadId)
        {
            using (var unitOfWork = new UnitofWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Socios.GetCantidad(localidadId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public Socio GetSocioPorId(int socioId)
        {
            using (var unitOfWork = new UnitofWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Socios.GetSocioPorId(socioId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<SocioListDto> GetSocios(int? localidadId)
        {
            using (var unitOfWork = new UnitofWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Socios.GetSocios(localidadId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<SocioListDto> GetSociosPorPagina(int registrosPorPagina, int paginaActual, int? localidadId)
        {
            using (var unitOfWork = new UnitofWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Socios
                        .GetSociosPorPagina(registrosPorPagina,
                        paginaActual, localidadId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public void Guardar(Socio socio)
        {
            using (var unitOfWork = new UnitofWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    if (socio.SocioId == 0)
                    {
                        unitOfWork.Socios.Agregar(socio);
                    }
                    else
                    {
                        unitOfWork.Socios.Editar(socio);
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
