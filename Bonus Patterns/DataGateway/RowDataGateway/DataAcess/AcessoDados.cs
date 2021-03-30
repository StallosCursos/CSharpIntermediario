using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PersistenciaCompleta.Dados
{
    //CA1063: Implementar IDisposable corretamente
    public class AcessoDados: IDisposable
    {
        private bool _isDisposing;
        private readonly SqlConnection _sqlConnection;
        
        public AcessoDados(string stringName)
        {
            _sqlConnection = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionString"));
            _sqlConnection.Open();
        }

        public SqlCommand CreateCommand(string Query)
        {
            SqlCommand command = _sqlConnection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = Query;

            return command;
        }

        public SqlDataReader ExecuteReader(SqlCommand command) =>
            command.ExecuteReader();

        public IEnumerable<IDataRecord> Reader(SqlCommand command)
        {
            SqlDataReader reader = ExecuteReader(command);
            while (reader.Read())
                yield return reader;
            reader.Close();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposing) return;

            if (disposing)
            {
                _sqlConnection.Close();
                _sqlConnection.Dispose();
            }

            _isDisposing = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
