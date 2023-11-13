using Dapper;
using Deportivo.Comun;
using Deportivo.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace Deportivo.Datos.Repositorios 
{
    public class RepositorioLocalidades : IRepositorioLocalidades
    {

        private readonly string cadenaConexion;
        public RepositorioLocalidades()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }



        public void Agregar(Localidad localidad)
        {
            using (IDbConnection conn = new SqlConnection(cadenaConexion))
            {

                string insertQuery = "INSERT INTO Localidades (NombreLocalidad) VALUES(@NombreLocalidad); SELECT SCOPE_IDENTITY()";
                int id = conn.QuerySingle<int>(insertQuery, localidad);
                localidad.LocalidadId = id;

            }
        }

        public void Borrar(int localidadId)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string deleteQuery = "DELETE FROM Localidades WHERE LocalidadId=@LocalidadId";
                conn.Execute(deleteQuery, new { localidadId });
            }
        }

        public void Editar(Localidad localidad)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {

                string updateQuery = "UPDATE Localidades SET NombreLocalidad=@NombreLocalidad WHERE LocalidadId=@LocalidadId";
                conn.Execute(updateQuery, localidad);
            }
        }

        public bool EstaRelacionado(Localidad localidad)
        {
            int cantidad = 0;
            using (IDbConnection conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = "SELECT COUNT(*) FROM Socios WHERE LocalidadId=@LocalidadId";
                cantidad = conn.QuerySingle<int>(selectQuery, new { LocalidadId = localidad.LocalidadId });
            }
            return cantidad > 0;
        }

        public bool Existe(Localidad localidad)
        {
            var cantidad = 0;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (localidad.LocalidadId == 0)
                {
                    selectQuery = "SELECT COUNT(*) FROM Localidades WHERE NombreLocalidad=@NombreLocalidad";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, localidad);
                }
                else
                {
                    selectQuery = "SELECT COUNT(*) FROM Localidades WHERE NombreLocalidad=@NombreLocalidad AND LocalidadId!=@LocalidadId";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, localidad);
                }
            }
            return cantidad > 0;
        }

        public int GetCantidad(string textoFiltro = null)
        {
            int cantidad = 0;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (textoFiltro == null)
                {
                    selectQuery = "SELECT COUNT(*) FROM Localidades";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
                else
                {
                    selectQuery = "SELECT COUNT(*) FROM Localidades WHERE NombreLocalidad LIKE @textoFiltro";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { textoFiltro });

                }
            }
            return cantidad;

        }

        public List<Localidad> GetLocalidades(string textoFiltro = null)
        {
            List<Localidad> lista = new List<Localidad>();
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (textoFiltro != null)
                {
                    selectQuery = @"SELECT LocalidadId, NombreLocalidad FROM Localidades
                            WHERE UPPER(NombreLocalidad) LIKE @textoFiltro ORDER BY NombreLocalidad";
                    textoFiltro = $"{textoFiltro.ToUpper()}%";
                    lista = conn.Query<Localidad>(selectQuery, new { textoFiltro }).ToList();

                }
                else
                {
                    selectQuery = @"SELECT LocalidadId, NombreLocalidad FROM Localidades
                             ORDER BY NombreLocalidad";
                    lista = conn.Query<Localidad>(selectQuery).ToList();

                }
            }
            return lista;

        }

        public List<Localidad> GetLocalidadesPorPagina(int cantidad, int paginaActual, string textoFiltro = null)
        {
            List<Localidad> lista = new List<Localidad>();
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (textoFiltro == null)
                {
                    selectQuery = @"SELECT LocalidadId, NombreLocalidad FROM Localidades
                        ORDER BY NombreLocalidad 
                        OFFSET @cantidadRegistros ROWS FETCH NEXT @cantidadPorPagina ROWS ONLY";
                    lista = conn.Query<Localidad>(selectQuery, new
                    {
                        cantidadRegistros = cantidad * (paginaActual - 1),
                        cantidadPorPagina = cantidad
                    }).ToList();
                }
                else
                {
                    selectQuery = @"SELECT LocalidadId, NombreLocalidad FROM Localidades WHERE NombreLocalidad LIKE @textoFiltro
                        ORDER BY NombreLocalidad 
                        OFFSET @cantidadRegistros ROWS FETCH NEXT @cantidadPorPagina ROWS ONLY";
                    lista = conn.Query<Localidad>(selectQuery, new
                    {
                        cantidadRegistros = cantidad * (paginaActual - 1),
                        cantidadPorPagina = cantidad,
                        textoFiltro = textoFiltro
                    }).ToList();

                }
            }
            return lista;
        }

        public Localidad GetLocalidadPorId(int localidadId)
        {
            Localidad localidad = null;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = "SELECT LocalidadId, NombreLocalidad FROM Localidades WHERE LocalidadId=@LocalidadId";
                localidad = conn.QueryFirstOrDefault<Localidad>(selectQuery, new { localidadId });
            }
            return localidad;
        }
    }

}
