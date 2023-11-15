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
    public class ServicioCanchas : IServicioCanchas
    {

        private readonly IRepositorioCanchas _repositorio;
        public ServicioCanchas()
        {
            _repositorio = new RepositorioCanchas();
        }

        public void Borrar(int canchaId)
        {
            try
            {
                _repositorio.Borrar(canchaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionado(Cancha cancha)
        {
            try
            {
                return _repositorio.EstaRelacionado(cancha);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Cancha cancha)
        {
            try
            {
                return _repositorio.Existe(cancha);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Cancha GetCanchaPorId(int canchaId)
        {
            try
            {
                return _repositorio.GetCanchaPorId(canchaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Cancha> GetCanchas(string textoFiltro = null)
        {
            try
            {
                return _repositorio.GetCanchas(textoFiltro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Cancha> GetCanchasPorPagina(int cantidad, int paginaActual, string textoFiltro = null)
        {
            try
            {
                return _repositorio.GetCanchasPorPagina(cantidad, paginaActual, textoFiltro);
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

        public void Guardar(Cancha cancha)
        {
            try
            {
                if (cancha.CanchaId == 0)
                {
                    _repositorio.Agregar(cancha);

                }
                else
                {
                    _repositorio.Editar(cancha);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
