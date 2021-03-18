using ConsoleTables;
using PersistenciaSimples;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LendoDados
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

            InserindoAtribuindoID(cliente, connection);

            SelectComDataReader(connection);
            SelectComDataSet(connection);
        }

        private static void SelectComDataSet(SqlConnection connection)
        {
            string Sql = "SELECT * FROM Clientes";

            DataSet dataSet = new DataSet("DataSet");

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(Sql, connection);
            sqlDataAdapter.Fill(dataSet, "Clientes");

            var Rows = dataSet.Tables["Clientes"].Select();

            ConsoleTable consoleTable = new ConsoleTable();
            consoleTable.AddColumn(new[] { "Id", "Nome", "Sobrenome", "Idade", "Data Nascimento" });
            foreach (var row in Rows)
            {
                consoleTable.AddRow(
                    row.Field<int>("Id"),
                    row.Field<string>("Nome"),
                    row.Field<string>("Sobrenome"),
                    row.Field<int>("Idade"),
                    row.Field<DateTime>("DataNascimento")
                );
            }

            consoleTable.Write();
        }

        private static void SelectComDataReader(SqlConnection connection)
        {
            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM Clientes";

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            ConsoleTable consoleTable = new ConsoleTable();
            consoleTable.AddColumn(new[] { "Id", "Nome", "Sobrenome", "Idade", "Data Nascimento" });

            while (sqlDataReader.Read())
            {
                consoleTable.AddRow
                (
                    sqlDataReader["Id"].ToString(),
                    sqlDataReader.GetString(1),
                    sqlDataReader.GetString(2),
                    sqlDataReader.GetInt32(3),
                    sqlDataReader.GetDateTime(4).ToString("dd/MM/yyyy")
                );
            }

            sqlDataReader.Close();
            consoleTable.Write();
        }

        private static void InserindoAtribuindoID(Cliente cliente, SqlConnection connection)
        {
            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.Text;

            sqlCommand.CommandText = "INSERT INTO Clientes (Nome, Sobrenome, Idade, DataNascimento) ";
            sqlCommand.CommandText += "OUTPUT inserted.Id ";
            sqlCommand.CommandText += "VALUES (@Nome, @Sobrenome, @Idade, @DataNascimento) ";

            sqlCommand.Parameters.AddWithValue("@Nome", cliente.Nome);
            sqlCommand.Parameters.AddWithValue("@Sobrenome", cliente.Sobrenome);
            sqlCommand.Parameters.AddWithValue("@Idade", cliente.Idade);
            sqlCommand.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);

            cliente.Id = (int)sqlCommand.ExecuteScalar();
        }
    }
}
