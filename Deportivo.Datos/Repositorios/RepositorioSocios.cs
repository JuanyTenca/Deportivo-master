using Dapper;
using Deportivo.Comun.Interfaces;
using Deportivo.Entidades.Dtos.Socio;
using Deportivo.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportivo.Datos.Repositorios
{
    public class RepositorioSocios : IRepositorioSocios
    {

        private IDbTransaction _transaction;
        public RepositorioSocios(IDbTransaction transaction)
        {
            _transaction = transaction;
        }


        public void Agregar(Socio socio)
        {
            string addQuery = @"INSERT INTO Socios (Nombre, Apellido, 
                            NroDocumento, LocalidadId, NroTelefono, Email)
                            VALUES (@Nombre, @Apellido, @NroDocumento,
                            @LocalidadId, @NroTelefono, @Email);
                            SELECT SCOPE_IDENTITY()";


            int id = _transaction.Connection.ExecuteScalar<int>(addQuery, socio, transaction: _transaction);
            socio.SocioId = id;

        }

        public void Borrar(int socioId)
        {

            string deleteQuery = "DELETE FROM Socios WHERE SocioId=@SocioId";
            _transaction.Connection.Execute(deleteQuery,
                new { SocioId = socioId }, transaction: _transaction);

        }

        public void Editar(Socio socio)
        {
            string updateQuery = @"UPDATE Socios SET Nombre=@Nombre,
                            Apellido=@Apellido, NroDocumento=@NroDocumento, 
                            LocalidadId=@LocalidadId, NroTelefono=@NroTelefono, Email=@Email                         
                            WHERE SocioId=@SocioId";

            _transaction.Connection.Execute(updateQuery, transaction: _transaction);
        }

        public bool EstaRelacionado(Socio socio)
        {
            int cantidad = 0;
            string selectQuery = @"SELECT COUNT(*) FROM Reservas WHERE SocioId=@socioId";
            cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, new { socioId = socio.SocioId }, transaction: _transaction);
            return cantidad > 0;
        }

        public bool Existe(Socio socio)
        {
            var cantidad = 0;
            string selectQuery;
            if (socio.SocioId == 0)
            {
                selectQuery = @"SELECT COUNT(*) FROM Socios 
                            WHERE Nombre=@Nombre AND Apellido=@Apellido";
                cantidad = _transaction.Connection.ExecuteScalar<int>(
                    selectQuery, new { Nombre = socio.Nombre, Apellido = socio.Apellido}, transaction: _transaction);
            }
            else
            {
                selectQuery = @"SELECT COUNT(*) FROM Socios 
                            WHERE Nombre=@Nombre AND Apellido=@Apellido AND SocioId=@SocioId";
                cantidad = _transaction.Connection.ExecuteScalar<int>(
                    selectQuery, new { Nombre = socio.Nombre, Apellido = socio.Apellido, SocioId = socio.SocioId }, transaction: _transaction);

            }
            return cantidad > 0;
        }

        public int GetCantidad(int? localidadId)
        {
            int cantidad = 0;
            string selectQuery = "SELECT COUNT(*) FROM Socios";
            if (localidadId == null)
            {
                cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, transaction: _transaction);
            }
        
            else
            {
                selectQuery += " WHERE LocalidadId=@LocalidadId ";
                cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, new { LocalidadId = localidadId }, transaction: _transaction);

            }
            return cantidad;


        }

        public Socio GetSocioPorId(int socioId)
        {
            Socio socio = null;
            string selectQuery = @"SELECT SocioId, Nombre, Apellido, 
                    NroDocumento, LocalidadId,
                    NroTelefono, Email 
                    FROM Socios WHERE SocioId=@SocioId";
            socio = _transaction.Connection.QuerySingleOrDefault<Socio>(selectQuery, new { SocioId = socioId }, transaction: _transaction);
            return socio;
        }

        public List<SocioListDto> GetSocios(int? localidadId)
        {
            List<SocioListDto> lista = new List<SocioListDto>();
            string selectQuery = @"SELECT SocioId, Nombre, Apellido, NroDocumento, NombreLocalidad, NroTelefono, Email 
                    FROM Socios INNER JOIN Localidades ON Socios.LocalidadId=Localidades.LocalidadId ";
            string orderBy = " ORDER BY Apellido, Nombre";
            if (localidadId == null)
            {
                selectQuery += orderBy;
                lista = _transaction.Connection.Query<SocioListDto>(selectQuery, transaction: _transaction).ToList();
            }
         
            else
            {
                selectQuery += " WHERE LocalidadId=@LocalidadId" + orderBy;
                lista = _transaction.Connection.Query<SocioListDto>(selectQuery, new { LocalidadId = localidadId }, transaction: _transaction).ToList();

            }
            return lista;
        }

        public List<SocioListDto> GetSociosPorPagina(int registrosPorPagina, int paginaActual, int? localidadId)
        {
            List<SocioListDto> lista = new List<SocioListDto>();
            if (localidadId == null)
            {
                string selectQuery = @"SELECT SocioId, Nombre, Apellido, NroDocumento, NombreLocalidad, NroTelefono, Email 
                    FROM Socios INNER JOIN Localidades ON Socios.LocalidadId=Localidades.LocalidadId
                    ORDER BY Apellido, Nombre
                    OFFSET @registrosSaltados ROWS 
                    FETCH NEXT @cantidadPorPagina ROWS ONLY";
                lista = _transaction.Connection.Query<SocioListDto>(selectQuery, new
                {
                    registrosSaltados = registrosPorPagina * (paginaActual - 1),
                    cantidadPorPagina = registrosPorPagina
                }, transaction: _transaction).ToList();

            }
            
            else
            {
                string selectQuery = @"SELECT SocioId, Nombre, Apellido, NroDocumento, LocalidadId, NroTelefono, Email 
                    FROM Socios INNER JOIN Localidades ON Socios.LocalidadId=Localidades.LocalidadId
                    WHERE Socios.LocalidadId=@LocalidadId
                    ORDER BY Apellido, Nombre
                    OFFSET @registrosSaltados ROWS 
                    FETCH NEXT @cantidadPorPagina ROWS ONLY";
                lista = _transaction.Connection.Query<SocioListDto>(selectQuery, new
                {
                    registrosSaltados = registrosPorPagina * (paginaActual - 1),
                    cantidadPorPagina = registrosPorPagina,
                    LocalidadId = localidadId.Value,
                }, transaction: _transaction).ToList();

            }
            return lista;
        }
    }
}
