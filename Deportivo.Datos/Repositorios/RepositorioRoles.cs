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
    public class RepositorioRoles : IRepositorioRoles
    {

        private readonly string cadenaConexion;
        public RepositorioRoles()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }


        public void Agregar(Rol rol)
        {
            using (IDbConnection conn = new SqlConnection(cadenaConexion))
            {

                string insertQuery = "INSERT INTO Roles (Descripcion) VALUES(@Descripcion); SELECT SCOPE_IDENTITY()";
                int id = conn.QuerySingle<int>(insertQuery, rol);
                rol.RolId = id;

            }
        }

        public void Borrar(int rolId)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string deleteQuery = "DELETE FROM Roles WHERE RolId=@RolId";
                conn.Execute(deleteQuery, new { rolId });
            }
        }

        public void Editar(Rol rol)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {

                string updateQuery = "UPDATE Roles SET Descripcion=@Descripcion WHERE RolId=@RolId";
                conn.Execute(updateQuery, rol);
            }
        }

        public bool EstaRelacionado(Rol rol)
        {
            int cantidad = 0;
            using (IDbConnection conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = "SELECT COUNT(*) FROM Empleados WHERE RolId=@RolId";
                cantidad = conn.QuerySingle<int>(selectQuery, new { RolId = rol.RolId });
            }
            return cantidad > 0;
        }

        public bool Existe(Rol rol)
        {
            var cantidad = 0;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (rol.RolId == 0)
                {
                    selectQuery = "SELECT COUNT(*) FROM Roles WHERE Descripcion=@Descripcion";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, rol);
                }
                else
                {
                    selectQuery = "SELECT COUNT(*) FROM Roles WHERE Descripcion=@Descripcion AND RolId!=@RolId";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, rol);
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
                    selectQuery = "SELECT COUNT(*) FROM Roles";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
                else
                {
                    selectQuery = "SELECT COUNT(*) FROM Roles WHERE Descripcion LIKE @textoFiltro";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { textoFiltro });

                }
            }
            return cantidad;

        }

        public List<Rol> GetRoles(string textoFiltro = null)
        {
            List<Rol> lista = new List<Rol>();
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (textoFiltro != null)
                {
                    selectQuery = @"SELECT RolId, Descripcion FROM Roles
                            WHERE UPPER(Descripcion) LIKE @textoFiltro ORDER BY Descripcion";
                    textoFiltro = $"{textoFiltro.ToUpper()}%";
                    lista = conn.Query<Rol>(selectQuery, new { textoFiltro }).ToList();

                }
                else
                {
                    selectQuery = @"SELECT RolId, Descripcion FROM Roles
                             ORDER BY Descripcion";
                    lista = conn.Query<Rol>(selectQuery).ToList();

                }
            }
            return lista;
        }

        public List<Rol> GetRolesPorPagina(int cantidad, int paginaActual, string textoFiltro = null)
        {
            List<Rol> lista = new List<Rol>();
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (textoFiltro == null)
                {
                    selectQuery = @"SELECT RolId, Descripcion FROM Roles
                        ORDER BY Descripcion 
                        OFFSET @cantidadRegistros ROWS FETCH NEXT @cantidadPorPagina ROWS ONLY";
                    lista = conn.Query<Rol>(selectQuery, new
                    {
                        cantidadRegistros = cantidad * (paginaActual - 1),
                        cantidadPorPagina = cantidad
                    }).ToList();
                }
                else
                {
                    selectQuery = @"SELECT RolId, Descripcion FROM Roles WHERE Descripcion LIKE @textoFiltro
                        ORDER BY Descripcion 
                        OFFSET @cantidadRegistros ROWS FETCH NEXT @cantidadPorPagina ROWS ONLY";
                    lista = conn.Query<Rol>(selectQuery, new
                    {
                        cantidadRegistros = cantidad * (paginaActual - 1),
                        cantidadPorPagina = cantidad,
                        textoFiltro = textoFiltro
                    }).ToList();

                }
            }
            return lista;
        }

        public Rol GetRolPorId(int rolId)
        {
            Rol rol = null;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = "SELECT RolId, Descripcion FROM Roles WHERE RolId=@RolId";
                rol = conn.QueryFirstOrDefault<Rol>(selectQuery, new { rolId });
            }
            return rol;
        }
    }
}
