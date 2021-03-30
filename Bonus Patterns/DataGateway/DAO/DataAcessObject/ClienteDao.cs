using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DAO.DataAcessObject.Base;
using DAO.Entitites;

namespace DAO.DataAcessObject
{
    public class ClienteDao : BaseDao, IClienteDao
    {
        private List<Cliente> Cast(IEnumerable<IDataRecord> Reader)
        {
            return Reader.Select(cliente => new Cliente
            {
                Id = Convert.ToInt32(cliente["Id"]),
                Nome = Convert.ToString(cliente["Nome"]),
                Sobrenome = Convert.ToString(cliente["Sobrenome"]),
                DataNascimento = Convert.ToDateTime(cliente["DataNascimento"])
            }).ToList();
        }

        public void Delete(Cliente objeto)
        {
            Sql.Clear();

            Sql.AppendLine("DELETE FROM Clientes ");
            Sql.AppendLine("WHERE Id = @Id ");

            var Command = AcessoDados.CreateCommand(Sql.ToString());

            Command.Parameters.AddWithValue("@Id", objeto.Id);

            Command.ExecuteNonQuery();
        }

        public Cliente Find(Cliente objeto)
        {
            Sql.Clear();

            Sql.AppendLine("SELECT * FROM Clientes ");
            Sql.AppendLine("WHERE Clientes.Id = @Id ");

            var Command = AcessoDados.CreateCommand(Sql.ToString());
            Command.Parameters.AddWithValue("@Id", objeto.Id);

            var Reader = AcessoDados.Reader(Command);

            var Cliente = Cast(Reader).FirstOrDefault();

            return Cliente;
        }

        public Cliente Insert(Cliente objeto)
        {
            Sql.Clear();

            Sql.AppendLine("INSERT INTO Clientes (Nome, Sobrenome, Idade, DataNascimento) ");
            Sql.AppendLine("OUTPUT inserted.Id ");
            Sql.AppendLine("VALUES (@Nome, @Sobrenome, @Idade, @DataNascimento) ");

            var Command = AcessoDados.CreateCommand(Sql.ToString());

            Command.Parameters.AddWithValue("@Nome", objeto.Nome);
            Command.Parameters.AddWithValue("@Sobrenome", objeto.Sobrenome);
            Command.Parameters.AddWithValue("@Idade", objeto.Idade);
            Command.Parameters.AddWithValue("@DataNascimento", objeto.DataNascimento);

            objeto.Id = (int)Command.ExecuteScalar();

            return objeto;
        }

        public List<Cliente> SelectAll()
        {
            Sql.Clear();

            Sql.AppendLine(" SELECT * FROM Clientes ");

            var Command = AcessoDados.CreateCommand(Sql.ToString());
            var Reader = AcessoDados.Reader(Command);

            var Clientes = Cast(Reader);

            return Clientes;
        }

        public void Update(Cliente objeto)
        {
            Sql.Clear();

            Sql.AppendLine("UPDATE Clientes SET ");
            Sql.AppendLine("  Nome = @Nome, @Sobrenome = @Sobrenome, Idade = @Idade, DataNascimento = @DataNascimento ");
            Sql.AppendLine("WHERE Id = @Id ");

            var Command = AcessoDados.CreateCommand(Sql.ToString());

            Command.Parameters.AddWithValue("@Id", objeto.Id);
            Command.Parameters.AddWithValue("@Nome", objeto.Nome);
            Command.Parameters.AddWithValue("@Sobrenome", objeto.Sobrenome);
            Command.Parameters.AddWithValue("@Idade", objeto.Idade);
            Command.Parameters.AddWithValue("@DataNascimento", objeto.DataNascimento);

            Command.ExecuteNonQuery();
        }

        public Cliente Find(int Id)
        {
            Sql.Clear();

            Sql.AppendLine(" SELECT * FROM Clientes ");
            Sql.AppendLine(" WHERE Id = @Id ");

            var Command = AcessoDados.CreateCommand(Sql.ToString());
            Command.Parameters.AddWithValue("@Id", Id);

            var Reader = AcessoDados.Reader(Command);

            var Clientes = Cast(Reader);

            return Clientes.FirstOrDefault();
        }

        public List<Cliente> Select(Cliente Object)
        {

            Sql.Clear();

            Sql.AppendLine(" SELECT * FROM Clientes ");

            var Command = AcessoDados.CreateCommand(Sql.ToString());

            var Reader = AcessoDados.Reader(Command);

            var Clientes = Cast(Reader);

            return Clientes;
        }
    }
}
