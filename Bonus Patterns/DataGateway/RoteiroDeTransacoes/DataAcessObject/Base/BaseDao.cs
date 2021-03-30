using DAO.DataGateWay.Base;
using System.Text;

namespace DAO.DataAcessObject.Base
{
    public abstract class BaseDao
    {
        protected readonly StringBuilder Sql;
        
        public AcessoDados AcessoDados { get; private set; }

        public BaseDao()
        {
            Sql = new StringBuilder();
            AcessoDados = new AcessoDados("connectionString");
        }

        public BaseDao(AcessoDados acessoDados)
        {
            Sql = new StringBuilder();
            AcessoDados = acessoDados;
        }
    }
}
