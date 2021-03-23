using PersistenciaCompleta.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PersistenciaCompleta.Dados
{
    public class EnderecoDados: BaseEntidadeDados
    {
        public int Insert(Endereco Endereco)
        {
            this.Sql.Clear();

            this.Sql.AppendLine("INSERT INTO Endereco (IdCliente, Logradouro, Bairro, Cidade, Estado, Cep, Tipo) ");
            this.Sql.AppendLine("OUTPUT inserted.Id ");
            this.Sql.AppendLine("VALUES (@IdCliente, @Logradouro, @Bairro, @Cidade, @Estado, @Cep, @Tipo) ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());

            Command.Parameters.AddWithValue("@IdCliente", Endereco.IdCliente);
            Command.Parameters.AddWithValue("@Logradouro", Endereco.Logradouro);
            Command.Parameters.AddWithValue("@Bairro", Endereco.Bairro);
            Command.Parameters.AddWithValue("@Cidade", Endereco.Cidade);
            Command.Parameters.AddWithValue("@Estado", Endereco.Estado);
            Command.Parameters.AddWithValue("@Cep", Endereco.Cep);
            Command.Parameters.AddWithValue("@Tipo", Endereco.Tipo);

            return (int)Command.ExecuteScalar();
        }

        public void Update(Endereco Endereco)
        {
            this.Sql.Clear();

            this.Sql.AppendLine("UPDATE Endereco SET ");
            this.Sql.AppendLine("  IdCliente = @IdCliente, @Logradouro = @Logradouro, ");
            this.Sql.AppendLine("  Bairro = @Bairro, Cidade = @Cidade, Estado = @Estado, ");
            this.Sql.AppendLine("  Cep = @Cep, Tipo = @Tipo ");
            this.Sql.AppendLine("WHERE Id = @Id ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());

            Command.Parameters.AddWithValue("@IdCliente", Endereco.IdCliente);
            Command.Parameters.AddWithValue("@Logradouro", Endereco.Logradouro);
            Command.Parameters.AddWithValue("@Bairro", Endereco.Bairro);
            Command.Parameters.AddWithValue("@Cidade", Endereco.Cidade);
            Command.Parameters.AddWithValue("@Estado", Endereco.Estado);
            Command.Parameters.AddWithValue("@Cep", Endereco.Cep);
            Command.Parameters.AddWithValue("@Tipo", Endereco.Tipo);
            Command.Parameters.AddWithValue("@Id", Endereco.Id);

            Command.ExecuteNonQuery();
        }

        public void Delete(Endereco Endereco)
        {
            this.Sql.Clear();

            this.Sql.AppendLine("DELETE FROM Endereco ");
            this.Sql.AppendLine("WHERE Id = @Id ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());

            Command.Parameters.AddWithValue("@Id", Endereco.Id);

            Command.ExecuteNonQuery();
        }

        public List<Endereco> FindByCliente(int IdCliente)
        {
            this.Sql.Clear();

            this.Sql.AppendLine(" SELECT * FROM Endereco ");
            this.Sql.AppendLine(" WHERE IdCliente = @IdCliente ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());
            Command.Parameters.AddWithValue("@IdCliente", IdCliente);

            var Reader = this.AcessoDados.Reader(Command);

            var Enderecos = Cast(Reader);

            return Enderecos;
        }

        private List<Endereco> Cast(IEnumerable<IDataRecord> Reader)
        {
            return Reader.Select(endereco => new Endereco
            {
                Id = Convert.ToInt32(endereco["Id"]),
                IdCliente = Convert.ToInt32(endereco["IdCliente"]),
                Bairro = Convert.ToString(endereco["Bairro"]),
                Cep = Convert.ToString(endereco["Cep"]),
                Cidade = Convert.ToString(endereco["Cidade"]),
                Estado = Convert.ToString(endereco["Estado"]),
                Logradouro = Convert.ToString(endereco["Logradouro"]),
                Tipo = (TipoEndereco)Convert.ToInt32(endereco["Tipo"])
            }).ToList();
        }
    }
}
