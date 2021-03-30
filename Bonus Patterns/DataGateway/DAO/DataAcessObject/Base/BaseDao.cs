using DAO.DataGateWay.Base;
using System.Text;

namespace DAO.DataAcessObject.Base
{
    public abstract class BaseDao
    {
        protected readonly StringBuilder Sql;
        protected readonly AcessoDados AcessoDados;

        public BaseDao()
        {
            Sql = new StringBuilder();
            AcessoDados = new AcessoDados("connectionString");
        }
    }
}
