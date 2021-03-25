using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataGateway.Entitites;

namespace DataGateway.DataGateWay
{
    public class ClienteDataGateWay : BaseGateWayDados, IDataGateWay<Cliente>
    {
        private List<Cliente> Cast(IEnumerable<System.Data.IDataRecord> Reader)
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
            this.Sql.Clear();

            this.Sql.AppendLine("DELETE FROM Clientes ");
            this.Sql.AppendLine("WHERE Id = @Id ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());

            Command.Parameters.AddWithValue("@Id", objeto.Id);

            Command.ExecuteNonQuery();
        }

        public Cliente Find(Cliente objeto)
        {
            this.Sql.Clear();

            this.Sql.AppendLine("SELECT * FROM Clientes ");
            this.Sql.AppendLine("WHERE Clientes.Id = @Id ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());
            Command.Parameters.AddWithValue("@Id", objeto.Id);

            var Reader = this.AcessoDados.Reader(Command);

            var Cliente = Cast(Reader).FirstOrDefault();

            return Cliente;
        }

        public Cliente Insert(Cliente objeto)
        {
            this.Sql.Clear();

            this.Sql.AppendLine("INSERT INTO Clientes (Nome, Sobrenome, Idade, DataNascimento) ");
            this.Sql.AppendLine("OUTPUT inserted.Id ");
            this.Sql.AppendLine("VALUES (@Nome, @Sobrenome, @Idade, @DataNascimento) ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());

            Command.Parameters.AddWithValue("@Nome", objeto.Nome);
            Command.Parameters.AddWithValue("@Sobrenome", objeto.Sobrenome);
            Command.Parameters.AddWithValue("@Idade", objeto.Idade);
            Command.Parameters.AddWithValue("@DataNascimento", objeto.DataNascimento);

            objeto.Id = (int)Command.ExecuteScalar();

            return objeto;
        }

        public List<Cliente> SelectAll()
        {
            this.Sql.Clear();

            this.Sql.AppendLine(" SELECT * FROM Clientes ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());
            var Reader = this.AcessoDados.Reader(Command);

            var Clientes = Cast(Reader);

            return Clientes;
        }

        public void Update(Cliente objeto)
        {
            this.Sql.Clear();

            this.Sql.AppendLine("UPDATE Clientes SET ");
            this.Sql.AppendLine("  Nome = @Nome, @Sobrenome = @Sobrenome, Idade = @Idade, DataNascimento = @DataNascimento ");
            this.Sql.AppendLine("WHERE Id = @Id ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());

            Command.Parameters.AddWithValue("@Id", objeto.Id);
            Command.Parameters.AddWithValue("@Nome", objeto.Nome);
            Command.Parameters.AddWithValue("@Sobrenome", objeto.Sobrenome);
            Command.Parameters.AddWithValue("@Idade", objeto.Idade);
            Command.Parameters.AddWithValue("@DataNascimento", objeto.DataNascimento);

            Command.ExecuteNonQuery();
        }

        public Cliente Find(int Id)
        {
            this.Sql.Clear();

            this.Sql.AppendLine(" SELECT * FROM Clientes ");
            this.Sql.AppendLine(" WHERE Id = @Id ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());
            Command.Parameters.AddWithValue("@Id", Id);

            var Reader = this.AcessoDados.Reader(Command);

            var Clientes = Cast(Reader);

            return Clientes.FirstOrDefault();
        }

        public List<Cliente> Select(Cliente Object)
        {

            this.Sql.Clear();

            this.Sql.AppendLine(" SELECT * FROM Clientes ");
  
            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());

            var Reader = this.AcessoDados.Reader(Command);

            var Clientes = Cast(Reader);

            return Clientes;
        }
    }
}
