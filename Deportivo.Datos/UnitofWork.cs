using Deportivo.Comun;
using Deportivo.Comun.Interfaces;
using Deportivo.Datos.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportivo.Datos
{
    public class UnitofWork : IUnitofWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        public IRepositorioLocalidades Localidades { get; }

        public IRepositorioRoles Roles { get; }
     
        public IRepositorioSocios Socios { get; }

        public IRepositorioEmpleados Empleados { get; } 

        public IRepositorioCanchas Canchas { get; }

        public UnitofWork(string cadenaConexion)
        {
            if (string.IsNullOrWhiteSpace(cadenaConexion))
            {
                throw new ArgumentException("La cadena de conexión no puede estar vacía.", nameof(cadenaConexion));
            }

            _connection = new SqlConnection(cadenaConexion);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            Socios = new RepositorioSocios(_transaction);
            Empleados = new RepositorioEmpleados(_transaction);
           

        }

        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }



    }
}
