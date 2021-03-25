using System;
using System.Data.SqlClient;

namespace PersistenciaSimples
{
    class Program
    {
        static void Main(string[] args)
        {
            Cliente cliente = new Cliente
            {
                DataNascimento = DateTime.Parse("01/05/1982"),
                Nome = "José",
                Sobrenome = "José"
            };

            SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Clientes;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            connection.Open();

            //InserindoDados(cliente, connection);

            InserindoAtribuindoID(cliente, connection);

            AtualizandoCliente(cliente, connection);
            
            RemovendoRegistro(cliente, connection);

            connection.Close();
        }

        private static void RemovendoRegistro(Cliente cliente, SqlConnection connection)
        {
            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.Text;

            sqlCommand.CommandText = "DELETE FROM Clientes WHERE Id = @Id";
            sqlCommand.Parameters.AddWithValue("@Id", cliente.Id);

            sqlCommand.ExecuteNonQuery();
        }

        private static void InserindoAtribuindoID(Cliente cliente, SqlConnection connection)
        {
            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.Text;

            sqlCommand.CommandText =  "INSERT INTO Clientes (Nome, Sobrenome, Idade, DataNascimento) ";
            sqlCommand.CommandText += "OUTPUT inserted.Id ";
            sqlCommand.CommandText += "VALUES (@Nome, @Sobrenome, @Idade, @DataNascimento) ";

            sqlCommand.Parameters.AddWithValue("@Nome", cliente.Nome);
            sqlCommand.Parameters.AddWithValue("@Sobrenome", cliente.Sobrenome);
            sqlCommand.Parameters.AddWithValue("@Idade", cliente.Idade);
            sqlCommand.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);

            cliente.Id = (int)sqlCommand.ExecuteScalar();
        }

        private static void AtualizandoCliente(Cliente cliente, SqlConnection connection)
        {
            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.Text;

            cliente.Nome = "José Alterado";

            sqlCommand.CommandText = "UPDATE Clientes SET Nome = @Nome ";
            sqlCommand.CommandText += "WHERE Id = @Id ";

            sqlCommand.Parameters.AddWithValue("@Id", cliente.Id);
            sqlCommand.Parameters.AddWithValue("@Nome", cliente.Nome);

            sqlCommand.ExecuteNonQuery();
        }

        private static void InserindoDados(Cliente cliente, SqlConnection connection)
        {
            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.Text;

            /// ERRADO nunca implementem desta forma
            sqlCommand.CommandText = "INSERT INTO Clientes (Nome, Sobrenome, Idade, DataNascimento) ";
            sqlCommand.CommandText += @" VALUES ('" + cliente.Nome + "','" + cliente.Sobrenome + "'," +
                                                      cliente.Idade.ToString() + ",'" + cliente.DataNascimento.ToString("dd/MM/yyyy") + "')";

            sqlCommand.ExecuteNonQuery();

            sqlCommand.CommandText = "INSERT INTO Clientes (Nome, Sobrenome, Idade, DataNascimento) ";
            sqlCommand.CommandText += "VALUES (@Nome, @Sobrenome, @Idade, @DataNascimento) ";

            sqlCommand.Parameters.AddWithValue("@Nome", cliente.Nome);
            sqlCommand.Parameters.AddWithValue("@Sobrenome", cliente.Sobrenome);
            sqlCommand.Parameters.AddWithValue("@Idade", cliente.Idade);
            sqlCommand.Parameters.Add(new SqlParameter()
            {
                DbType = System.Data.DbType.DateTime,
                Value = cliente.DataNascimento,
                ParameterName = "@DataNascimento"
            });

            sqlCommand.ExecuteNonQuery();
        }
    }
}
