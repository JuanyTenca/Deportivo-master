using Dapper;
using Deportivo.Comun.Interfaces;
using Deportivo.Entidades.Dtos.Empleado;
using Deportivo.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportivo.Datos.Repositorios
{
    public class RepositorioEmpleados : IRepositorioEmpleados
    {
        private IDbTransaction _transaction;
        public RepositorioEmpleados(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Agregar(Empleado empleado)
        {
            string addQuery = @"INSERT INTO Empleados (Nombre, Apellido, 
                            NroDocumento, LocalidadId, NroTelefono, Email, RolId)
                            VALUES (@Nombre, @Apellido, @NroDocumento,
                            @LocalidadId, @NroTelefono, @Email, @RolId);
                            SELECT SCOPE_IDENTITY()";


            int id = _transaction.Connection.ExecuteScalar<int>(addQuery, empleado, transaction: _transaction);
            empleado.EmpleadoId = id;

        }

        public void Borrar(int empleadoId)
        {
            string deleteQuery = "DELETE FROM Empleados WHERE EmpleadoId=@EmpleadoId";
            _transaction.Connection.Execute(deleteQuery,
                new { EmpleadoId = empleadoId }, transaction: _transaction);

        }

        public void Editar(Empleado empleado)
        {
            string updateQuery = @"UPDATE Empleados SET Nombre=@Nombre,
                            Apellido=@Apellido, NroDocumento=@NroDocumento, 
                            LocalidadId=@LocalidadId, NroTelefono=@NroTelefono, 
                            Email=@Email, RolId=@RolId                        
                            WHERE SocioId=@SocioId";

            _transaction.Connection.Execute(updateQuery, transaction: _transaction);
        }

        public bool EstaRelacionado(Empleado empleado)
        {
            int cantidad = 0;
            string selectQuery = @"SELECT COUNT(*) FROM Reservas WHERE EmpleadoId=@EmpleadoId";
            cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, new { empleadoId = empleado.EmpleadoId }, transaction: _transaction);
            return cantidad > 0;
        }

        public bool Existe(Empleado empleado)
        {
            var cantidad = 0;
            string selectQuery;
            if (empleado.EmpleadoId == 0)
            {
                selectQuery = @"SELECT COUNT(*) FROM Empleados 
                            WHERE Nombre=@Nombre AND Apellido=@Apellido";
                cantidad = _transaction.Connection.ExecuteScalar<int>(
                    selectQuery, new { Nombre = empleado.Nombre, Apellido = empleado.Apellido }, transaction: _transaction);
            }
            else
            {
                selectQuery = @"SELECT COUNT(*) FROM Empleados 
                            WHERE Nombre=@Nombre AND Apellido=@Apellido AND EmpleadoId=@EmpleadoId";
                cantidad = _transaction.Connection.ExecuteScalar<int>(
                    selectQuery, new { Nombre = empleado.Nombre, Apellido = empleado.Apellido, EmpleadoId = empleado.EmpleadoId }, transaction: _transaction);

            }
            return cantidad > 0;
        }

        public int GetCantidad(int? localidadId, int? rolId)
        {
            int cantidad = 0;
            string selectQuery = "SELECT COUNT(*) FROM Empleados";
            if (localidadId == null)
            {
                cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, transaction: _transaction);
            }
            else if (rolId == null)
            {
                selectQuery += " WHERE LocalidadId=@LocalidadId";
                cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, new { LocalidadId = localidadId }, transaction: _transaction);

            }
            else
            {
                selectQuery += " WHERE LocalidadId=@LocalidadId AND RolId=@RolId";
                cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, new { LocalidadId = localidadId, RolId = rolId }, transaction: _transaction);

            }
            return cantidad;

        }

        public Empleado GetEmpleadoPorId(int empleadoId)
        {
            Empleado empleado = null;
            string selectQuery = @"SELECT EmpleadoId, Nombre, Apellido, 
                    NroDocumento, LocalidadId,
                    NroTelefono, Email, RolId
                    FROM Empleados WHERE EmpleadoId=@EmpleadoId";
            empleado = _transaction.Connection.QuerySingleOrDefault<Empleado>(selectQuery, new { EmpleadoId = empleadoId }, transaction: _transaction);
            return empleado;
        }

        public List<EmpleadoListDto> GetEmpleados(int? localidadId, int? rolId)
        {
            List<EmpleadoListDto> lista = new List<EmpleadoListDto>();
            string selectQuery = @"SELECT EmpleadoId, Nombre, Apellido, NroDocumento, LocalidadId, NroTelefono, Email, RolId 
                    FROM Empleados INNER JOIN Localidades ON Empleados.LocalidadId=Localidades.LocalidadId
                    INNER JOIN Roles ON Empleados.RolId=Roles.RolId ";
            string orderBy = " ORDER BY Apellido, Nombre";
            if (localidadId == null)
            {
                selectQuery += orderBy;
                lista = _transaction.Connection.Query<EmpleadoListDto>(selectQuery, transaction: _transaction).ToList();
            }
            else if (rolId == null)
            {
                selectQuery += " WHERE LocalidadId=@LocalidadId" + orderBy;
                lista = _transaction.Connection.Query<EmpleadoListDto>(selectQuery, new { LocalidadId = localidadId }, transaction: _transaction).ToList();
            }
            else
            {
                selectQuery += " WHERE LocalidadId=@LocalidadId AND RolId=@RolId" + orderBy;
                lista = _transaction.Connection.Query<EmpleadoListDto>(selectQuery, new { LocalidadId = localidadId, RolId = rolId }, transaction: _transaction).ToList();

            }
            return lista;
        }

        public List<EmpleadoListDto> GetEmpleadosPorPagina(int registrosPorPagina, int paginaActual, int? localidadId, int? rolId)
        {
            List<EmpleadoListDto> lista = new List<EmpleadoListDto>();
            if (localidadId == null)
            {
                string selectQuery = @"SELECT EmpleadoId, Nombre, Apellido, NroDocumento, LocalidadId, NroTelefono, Email, RolId 
                    FROM Empleados INNER JOIN Localidades ON Empleados.LocalidadId=Localidades.LocalidadId
                    INNER JOIN Roles ON Empleados.RolId=Roles.RolId
                    ORDER BY Apellido, Nombre
                    OFFSET @registrosSaltados ROWS 
                    FETCH NEXT @cantidadPorPagina ROWS ONLY";
                lista = _transaction.Connection.Query<EmpleadoListDto>(selectQuery, new
                {
                    registrosSaltados = registrosPorPagina * (paginaActual - 1),
                    cantidadPorPagina = registrosPorPagina
                }, transaction: _transaction).ToList();

            }
            else if (rolId == null)
            {
                string selectQuery = @"SELECT EmpleadoId, Nombre, Apellido, NroDocumento, LocalidadId, NroTelefono, Email, RolId
                    FROM Empleados INNER JOIN Localidades ON Empleados.LocalidadId=Localidades.LocalidadId
                    INNER JOIN Roles ON Empleados.RolId=Roles.RolId
                    WHERE Empleados.LocalidadId=@LocalidadId
                    ORDER BY Apellido, Nombre
                    OFFSET @registrosSaltados ROWS 
                    FETCH NEXT @cantidadPorPagina ROWS ONLY";
                lista = _transaction.Connection.Query<EmpleadoListDto>(selectQuery, new
                {
                    registrosSaltados = registrosPorPagina * (paginaActual - 1),
                    cantidadPorPagina = registrosPorPagina,
                    LocalidadId = localidadId.Value,
                }, transaction: _transaction).ToList();

            }
            else
            {
                string selectQuery = @"SELECT EmpleadoId, Nombre, Apellido, NroDocumento, LocalidadId, NroTelefono, Email, RolId 
                    FROM Empleados INNER JOIN Localidades ON Empleados.LocalidadId=Localidades.LocalidadId
                    INNER JOIN Roles ON Empleados.RolId=Roles.RolId
                    WHERE Empleados.LocalidadId=@LocalidadId AND Empleados.RolId=@RolId
                    ORDER BY Apellido, Nombre
                    OFFSET @registrosSaltados ROWS 
                    FETCH NEXT @cantidadPorPagina ROWS ONLY";
                lista = _transaction.Connection.Query<EmpleadoListDto>(selectQuery, new
                {
                    registrosSaltados = registrosPorPagina * (paginaActual - 1),
                    cantidadPorPagina = registrosPorPagina,
                    LocalidadId = localidadId.Value,
                    RolId = rolId.Value,
                }, transaction: _transaction).ToList();

            }
            return lista;
        }
    }
}
