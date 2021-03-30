using DataGateway.DataGateWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace RowDataGateway.GateWay.Cliente
{
    public class ClienteRowGateWay: BaseGateWayDados
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int Idade { get; set; }
        public DateTime DataNascimento { get; set; }

        public void Insert()
        {
            this.Sql.Clear();

            this.Sql.AppendLine("INSERT INTO Clientes (Nome, Sobrenome, Idade, DataNascimento) ");
            this.Sql.AppendLine("OUTPUT inserted.Id ");
            this.Sql.AppendLine("VALUES (@Nome, @Sobrenome, @Idade, @DataNascimento) ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());

            Command.Parameters.AddWithValue("@Nome", this.Nome);
            Command.Parameters.AddWithValue("@Sobrenome", this.Sobrenome);
            Command.Parameters.AddWithValue("@Idade", this.Idade);
            Command.Parameters.AddWithValue("@DataNascimento", this.DataNascimento);

            this.Id = (int)Command.ExecuteScalar();
        }

        public void Delete()
        {
            this.Sql.Clear();

            this.Sql.AppendLine("DELETE FROM Clientes ");
            this.Sql.AppendLine("WHERE Id = @Id ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());

            Command.Parameters.AddWithValue("@Id", this.Id);

            Command.ExecuteNonQuery();
        }

        public void Update()
        {
            this.Sql.Clear();

            this.Sql.AppendLine("UPDATE Clientes SET ");
            this.Sql.AppendLine("  Nome = @Nome, @Sobrenome = @Sobrenome, Idade = @Idade, DataNascimento = @DataNascimento ");
            this.Sql.AppendLine("WHERE Id = @Id ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());

            Command.Parameters.AddWithValue("@Id", this.Id);
            Command.Parameters.AddWithValue("@Nome", this.Nome);
            Command.Parameters.AddWithValue("@Sobrenome", this.Sobrenome);
            Command.Parameters.AddWithValue("@Idade", this.Idade);
            Command.Parameters.AddWithValue("@DataNascimento", this.DataNascimento);

            Command.ExecuteNonQuery();
        }
    }
}
