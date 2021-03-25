using PersistenciaCompleta.Dados;
using System.Text;

namespace DataGateway.DataGateWay
{
    public abstract class BaseGateWayDados
    {
        protected readonly StringBuilder Sql;
        protected readonly AcessoDados AcessoDados;

        public BaseGateWayDados()
        {
            Sql = new StringBuilder();
            AcessoDados = new AcessoDados("connectionString");
        }
    }
}
