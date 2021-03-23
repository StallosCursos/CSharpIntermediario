using PersistenciaCompleta.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersistenciaCompleta.Dados
{
    public class ClienteDados: BaseEntidadeDados
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

        public int Insert(Cliente cliente)
        {
            this.Sql.Clear();

            this.Sql.AppendLine("INSERT INTO Clientes (Nome, Sobrenome, Idade, DataNascimento) ");
            this.Sql.AppendLine("OUTPUT inserted.Id ");
            this.Sql.AppendLine("VALUES (@Nome, @Sobrenome, @Idade, @DataNascimento) ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());

            Command.Parameters.AddWithValue("@Nome", cliente.Nome);
            Command.Parameters.AddWithValue("@Sobrenome", cliente.Sobrenome);
            Command.Parameters.AddWithValue("@Idade", cliente.Idade);
            Command.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);

            return (int)Command.ExecuteScalar();
        }

        public void Update(Cliente cliente)
        {
            this.Sql.Clear();

            this.Sql.AppendLine("UPDATE Clientes SET ");
            this.Sql.AppendLine("  Nome = @Nome, @Sobrenome = @Sobrenome, Idade = @Idade, DataNascimento = @DataNascimento ");
            this.Sql.AppendLine("WHERE Id = @Id ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());

            Command.Parameters.AddWithValue("@Id", cliente.Id);
            Command.Parameters.AddWithValue("@Nome", cliente.Nome);
            Command.Parameters.AddWithValue("@Sobrenome", cliente.Sobrenome);
            Command.Parameters.AddWithValue("@Idade", cliente.Idade);
            Command.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);

            Command.ExecuteNonQuery();
        }

        public void Delete(Cliente cliente)
        {
            this.Sql.Clear();

            this.Sql.AppendLine("DELETE FROM Clientes ");
            this.Sql.AppendLine("WHERE Id = @Id ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());

            Command.Parameters.AddWithValue("@Id", cliente.Id);

            Command.ExecuteNonQuery();
        }

        public List<Cliente> Select()
        {
            this.Sql.Clear();

            this.Sql.AppendLine(" SELECT * FROM Clientes ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());
            var Reader = this.AcessoDados.Reader(Command);

            var Clientes = Cast(Reader);

            return Clientes;
        }

        public Cliente Find(int Id)
        {
            this.Sql.Clear();

            this.Sql.AppendLine("SELECT * FROM Clientes ");
            this.Sql.AppendLine("WHERE Clientes.Id = @Id ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());
            Command.Parameters.AddWithValue("@Id", Id);

            var Reader = this.AcessoDados.Reader(Command);

            var Cliente = Cast(Reader).FirstOrDefault();
            Cliente.Enderecos = new EnderecoDados().FindByCliente(Cliente.Id);

            return Cliente;
        }
    }
}
