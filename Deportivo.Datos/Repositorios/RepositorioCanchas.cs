using Deportivo.Comun.Interfaces;
using Deportivo.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Deportivo.Datos.Repositorios
{
    public class RepositorioCanchas : IRepositorioCanchas
    {

        private readonly string cadenaConexion;

        public RepositorioCanchas()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }

        public void Agregar(Cancha cancha)
        {
            using (IDbConnection conn = new SqlConnection(cadenaConexion))
            {

                string insertQuery = "INSERT INTO Canchas (CanchaNombre) VALUES(@CanchaNombre); SELECT SCOPE_IDENTITY()";
                int id = conn.QuerySingle<int>(insertQuery, cancha);
                cancha.CanchaId = id;

            }
        }

        public void Borrar(int canchaId)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string deleteQuery = "DELETE FROM Canchas WHERE CanchaId=@CanchaId";
                conn.Execute(deleteQuery, new { canchaId });
            }
        }

        public void Editar(Cancha cancha)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {

                string updateQuery = "UPDATE Canchas SET CanchaNombre=@CanchaNombre WHERE CanchaId=@CanchaId";
                conn.Execute(updateQuery, cancha);
            }
        }

        public bool EstaRelacionado(Cancha cancha)
        {
            int cantidad = 0;
            using (IDbConnection conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = "SELECT COUNT(*) FROM Reservas WHERE CanchaId=@CanchaId";
                cantidad = conn.QuerySingle<int>(selectQuery, new { CanchaId = cancha.CanchaId });
            }
            return cantidad > 0;
        }

        public bool Existe(Cancha cancha)
        {
            var cantidad = 0;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (cancha.CanchaId == 0)
                {
                    selectQuery = "SELECT COUNT(*) FROM Canchas WHERE CanchaNombre=@CanchaNombre";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, cancha);
                }
                else
                {
                    selectQuery = "SELECT COUNT(*) FROM Canchas WHERE CanchaNombre=@CanchaNombre AND CanchaId!=@CanchaId";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, cancha);
                }
            }
            return cantidad > 0;
        }

        public Cancha GetCanchaPorId(int canchaId)
        {
            Cancha cancha = null;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = "SELECT CanchaId, CanchaNombre FROM Canchas WHERE CanchaId=@CanchaId";
                cancha = conn.QueryFirstOrDefault<Cancha>(selectQuery, new { canchaId });
            }
            return cancha;
        }

        public List<Cancha> GetCanchas(string textoFiltro = null)
        {
            List<Cancha> lista = new List<Cancha>();
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (textoFiltro != null)
                {
                    selectQuery = @"SELECT CanchaId, CanchaNombre FROM Canchas
                            WHERE UPPER(CanchaNombre) LIKE @textoFiltro ORDER BY CanchaNombre";
                    textoFiltro = $"{textoFiltro.ToUpper()}%";
                    lista = conn.Query<Cancha>(selectQuery, new { textoFiltro }).ToList();

                }
                else
                {
                    selectQuery = @"SELECT CanchaId, CanchaNombre FROM Canchas
                             ORDER BY CanchaNombre";
                    lista = conn.Query<Cancha>(selectQuery).ToList();

                }
            }
            return lista;

        }

        public List<Cancha> GetCanchasPorPagina(int cantidad, int paginaActual, string textoFiltro = null)
        {
            List<Cancha> lista = new List<Cancha>();
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (textoFiltro == null)
                {
                    selectQuery = @"SELECT CanchaId, CanchaNombre FROM Canchas
                        ORDER BY CanchaNombre 
                        OFFSET @cantidadRegistros ROWS FETCH NEXT @cantidadPorPagina ROWS ONLY";
                    lista = conn.Query<Cancha>(selectQuery, new
                    {
                        cantidadRegistros = cantidad * (paginaActual - 1),
                        cantidadPorPagina = cantidad
                    }).ToList();
                }
                else
                {
                    selectQuery = @"SELECT CanchaId, CanchaNombre FROM Canchas WHERE CanchaNombre LIKE @textoFiltro
                        ORDER BY CanchaNombre 
                        OFFSET @cantidadRegistros ROWS FETCH NEXT @cantidadPorPagina ROWS ONLY";
                    lista = conn.Query<Cancha>(selectQuery, new
                    {
                        cantidadRegistros = cantidad * (paginaActual - 1),
                        cantidadPorPagina = cantidad,
                        textoFiltro = textoFiltro
                    }).ToList();

                }
            }
            return lista;
        }

        public int GetCantidad(string textoFiltro = null)
        {
            int cantidad = 0;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (textoFiltro == null)
                {
                    selectQuery = "SELECT COUNT(*) FROM Canchas";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
                else
                {
                    selectQuery = "SELECT COUNT(*) FROM Canchas WHERE CanchaNombre LIKE @textoFiltro";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { textoFiltro });

                }
            }
            return cantidad;

        }
    }
}
