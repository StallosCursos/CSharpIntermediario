using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataGateway.Entitites;

namespace DataGateway.DataGateWay
{
    public class EnderecoDataGateWay : BaseGateWayDados, IDataGateWay<Endereco>
    {
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


        public void Delete(Endereco objeto)
        {
            this.Sql.Clear();

            this.Sql.AppendLine("DELETE FROM Endereco ");
            this.Sql.AppendLine("WHERE Id = @Id ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());

            Command.Parameters.AddWithValue("@Id", objeto.Id);

            Command.ExecuteNonQuery();
        }

        public Endereco Find(int Id)
        {
            this.Sql.Clear();

            this.Sql.AppendLine(" SELECT * FROM Endereco ");
            this.Sql.AppendLine(" WHERE Id = @Id ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());
            Command.Parameters.AddWithValue("@Id", Id);

            var Reader = this.AcessoDados.Reader(Command);

            var Enderecos = Cast(Reader);

            return Enderecos.FirstOrDefault();
        }

        public Endereco Insert(Endereco objeto)
        {
            this.Sql.Clear();

            this.Sql.AppendLine("INSERT INTO Endereco (IdCliente, Logradouro, Bairro, Cidade, Estado, Cep, Tipo) ");
            this.Sql.AppendLine("OUTPUT inserted.Id ");
            this.Sql.AppendLine("VALUES (@IdCliente, @Logradouro, @Bairro, @Cidade, @Estado, @Cep, @Tipo) ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());

            Command.Parameters.AddWithValue("@IdCliente", objeto.IdCliente);
            Command.Parameters.AddWithValue("@Logradouro", objeto.Logradouro);
            Command.Parameters.AddWithValue("@Bairro", objeto.Bairro);
            Command.Parameters.AddWithValue("@Cidade", objeto.Cidade);
            Command.Parameters.AddWithValue("@Estado", objeto.Estado);
            Command.Parameters.AddWithValue("@Cep", objeto.Cep);
            Command.Parameters.AddWithValue("@Tipo", objeto.Tipo);

            objeto.Id = (int)Command.ExecuteScalar();

            return objeto;
        }

        public List<Endereco> SelectAll()
        {
            this.Sql.Clear();

            this.Sql.AppendLine(" SELECT * FROM Endereco ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());
            var Reader = this.AcessoDados.Reader(Command);

            var Enderecos = Cast(Reader);

            return Enderecos;
        }

        public void Update(Endereco objeto)
        {
            this.Sql.Clear();

            this.Sql.AppendLine("UPDATE Endereco SET ");
            this.Sql.AppendLine("  IdCliente = @IdCliente, @Logradouro = @Logradouro, ");
            this.Sql.AppendLine("  Bairro = @Bairro, Cidade = @Cidade, Estado = @Estado, ");
            this.Sql.AppendLine("  Cep = @Cep, Tipo = @Tipo ");
            this.Sql.AppendLine("WHERE Id = @Id ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());

            Command.Parameters.AddWithValue("@IdCliente", objeto.IdCliente);
            Command.Parameters.AddWithValue("@Logradouro", objeto.Logradouro);
            Command.Parameters.AddWithValue("@Bairro", objeto.Bairro);
            Command.Parameters.AddWithValue("@Cidade", objeto.Cidade);
            Command.Parameters.AddWithValue("@Estado", objeto.Estado);
            Command.Parameters.AddWithValue("@Cep", objeto.Cep);
            Command.Parameters.AddWithValue("@Tipo", objeto.Tipo);
            Command.Parameters.AddWithValue("@Id", objeto.Id);

            Command.ExecuteNonQuery();
        }

        public List<Endereco> Select(Endereco Object)
        {
            this.Sql.Clear();

            this.Sql.AppendLine(" SELECT * FROM Endereco ");
            this.Sql.AppendLine(" WHERE IdCliente = @IdCliente ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());
            Command.Parameters.AddWithValue("@IdCliente", Object.IdCliente);

            var Reader = this.AcessoDados.Reader(Command);

            var Enderecos = Cast(Reader);

            return Enderecos;
        }
    }
}
