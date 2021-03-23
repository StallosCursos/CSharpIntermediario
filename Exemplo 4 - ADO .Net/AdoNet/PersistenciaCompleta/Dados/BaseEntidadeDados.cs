using System.Text;

namespace PersistenciaCompleta.Dados
{
    public abstract class BaseEntidadeDados
    {
        protected readonly StringBuilder Sql;
        protected readonly AcessoDados AcessoDados;

        public BaseEntidadeDados() 
        {
            Sql = new StringBuilder();
            AcessoDados = new AcessoDados("connectionString");
        }
    }
}
