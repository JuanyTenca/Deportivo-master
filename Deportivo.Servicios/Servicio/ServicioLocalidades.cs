using Deportivo.Comun;
using Deportivo.Datos.Repositorios;
using Deportivo.Entidades.Entidades;
using Deportivo.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportivo.Servicios.Servicio
{
    public class ServicioLocalidades : IServicioLocalidades
    {
        private readonly IRepositorioLocalidades _repositorio;
        public ServicioLocalidades()
        {
            _repositorio = new RepositorioLocalidades();
        }

        public void Borrar(int localidadId)
        {
            try
            {
                _repositorio.Borrar(localidadId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionado(Localidad localidad)
        {
            try
            {
                return _repositorio.EstaRelacionado(localidad);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Localidad localidad)
        {
            try
            {
                return _repositorio.Existe(localidad);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(string textoFiltro = null)
        {
            try
            {
                return _repositorio.GetCantidad(textoFiltro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Localidad> GetLocalidades(string textoFiltro = null)
        {
            try
            {
                return _repositorio.GetLocalidades(textoFiltro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Localidad> GetLocalidadesPorPagina(int cantidad, int paginaActual, string textoFiltro = null)
        {
            try
            {
                return _repositorio.GetLocalidadesPorPagina(cantidad, paginaActual, textoFiltro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Localidad GetLocalidadPorId(int localidadId)
        {
            try
            {
                return _repositorio.GetLocalidadPorId(localidadId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Localidad localidad)
        {
            try
            {
                if (localidad.LocalidadId == 0)
                {
                    _repositorio.Agregar(localidad);

                }
                else
                {
                    _repositorio.Editar(localidad);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

  

}

