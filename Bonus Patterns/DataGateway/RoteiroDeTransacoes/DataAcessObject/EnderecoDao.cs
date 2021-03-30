using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DAO.DataAcessObject.Base;
using DAO.DataGateWay.Base;
using DAO.Entitites;

namespace DAO.DataAcessObject
{
    public class EnderecoDao : BaseDao, IEnderecoDao
    {
        public EnderecoDao(AcessoDados acessoDados) : base(acessoDados)
        {
        }

        public EnderecoDao()
        {
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


        public void Delete(Endereco objeto)
        {
            Sql.Clear();

            Sql.AppendLine("DELETE FROM Endereco ");
            Sql.AppendLine("WHERE Id = @Id ");

            var Command = AcessoDados.CreateCommand(Sql.ToString());

            Command.Parameters.AddWithValue("@Id", objeto.Id);

            Command.ExecuteNonQuery();
        }

        public Endereco Find(int Id)
        {
            Sql.Clear();

            Sql.AppendLine(" SELECT * FROM Endereco ");
            Sql.AppendLine(" WHERE Id = @Id ");

            var Command = AcessoDados.CreateCommand(Sql.ToString());
            Command.Parameters.AddWithValue("@Id", Id);

            var Reader = AcessoDados.Reader(Command);

            var Enderecos = Cast(Reader);

            return Enderecos.FirstOrDefault();
        }

        public Endereco Insert(Endereco objeto)
        {
            Sql.Clear();

            Sql.AppendLine("INSERT INTO Endereco (IdCliente, Logradouro, Bairro, Cidade, Estado, Cep, Tipo) ");
            Sql.AppendLine("OUTPUT inserted.Id ");
            Sql.AppendLine("VALUES (@IdCliente, @Logradouro, @Bairro, @Cidade, @Estado, @Cep, @Tipo) ");

            var Command = AcessoDados.CreateCommand(Sql.ToString());

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
            Sql.Clear();

            Sql.AppendLine(" SELECT * FROM Endereco ");

            var Command = AcessoDados.CreateCommand(Sql.ToString());
            var Reader = AcessoDados.Reader(Command);

            var Enderecos = Cast(Reader);

            return Enderecos;
        }

        public void Update(Endereco objeto)
        {
            Sql.Clear();

            Sql.AppendLine("UPDATE Endereco SET ");
            Sql.AppendLine("  IdCliente = @IdCliente, @Logradouro = @Logradouro, ");
            Sql.AppendLine("  Bairro = @Bairro, Cidade = @Cidade, Estado = @Estado, ");
            Sql.AppendLine("  Cep = @Cep, Tipo = @Tipo ");
            Sql.AppendLine("WHERE Id = @Id ");

            var Command = AcessoDados.CreateCommand(Sql.ToString());

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
            Sql.Clear();

            Sql.AppendLine(" SELECT * FROM Endereco ");
            Sql.AppendLine(" WHERE IdCliente = @IdCliente ");

            var Command = AcessoDados.CreateCommand(Sql.ToString());
            Command.Parameters.AddWithValue("@IdCliente", Object.IdCliente);

            var Reader = AcessoDados.Reader(Command);

            var Enderecos = Cast(Reader);

            return Enderecos;
        }
    }
}
