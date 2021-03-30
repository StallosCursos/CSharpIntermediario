using DataGateway.DataGateWay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RowDataGateway.GateWay.Cliente
{
    public class ClienteFinderRowGate: BaseGateWayDados
    {
        private List<ClienteRowGateWay> Cast(IEnumerable<System.Data.IDataRecord> Reader)
        {
            return Reader.Select(cliente => new ClienteRowGateWay
            {
                Id = Convert.ToInt32(cliente["Id"]),
                Nome = Convert.ToString(cliente["Nome"]),
                Sobrenome = Convert.ToString(cliente["Sobrenome"]),
                DataNascimento = Convert.ToDateTime(cliente["DataNascimento"]),
                Idade = Convert.ToInt32(cliente["Idade"])
            }).ToList();
        }

        public List<ClienteRowGateWay> Select()
        {
            this.Sql.Clear();

            this.Sql.AppendLine(" SELECT * FROM Clientes ");

            var Command = this.AcessoDados.CreateCommand(this.Sql.ToString());

            var Reader = this.AcessoDados.Reader(Command);

            return Cast(Reader);
        }

        public ClienteRowGateWay FindById(int Id)
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
    }
}
