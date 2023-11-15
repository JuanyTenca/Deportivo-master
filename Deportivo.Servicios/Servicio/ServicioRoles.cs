using Deportivo.Comun;
using Deportivo.Comun.Interfaces;
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
    public class ServicioRoles : IServicioRoles
    {

        private readonly IRepositorioRoles _repositorio;
        public ServicioRoles()
        {
            _repositorio = new RepositorioRoles();
        }

        public void Borrar(int rolId)
        {
            try
            {
                _repositorio.Borrar(rolId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionado(Rol rol)
        {
            try
            {
                return _repositorio.EstaRelacionado(rol);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Rol rol)
        {
            try
            {
                return _repositorio.Existe(rol);
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

        public List<Rol> GetRoles(string textoFiltro = null)
        {
            try
            {
                return _repositorio.GetRoles(textoFiltro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Rol> GetRolesPorPagina(int cantidad, int paginaActual, string textoFiltro = null)
        {
            try
            {
                return _repositorio.GetRolesPorPagina(cantidad, paginaActual, textoFiltro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Rol GetRolPorId(int rolId)
        {
            try
            {
                return _repositorio.GetRolPorId(rolId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Rol rol)
        {
            try
            {
                if (rol.RolId == 0)
                {
                    _repositorio.Agregar(rol);

                }
                else
                {
                    _repositorio.Editar(rol);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
