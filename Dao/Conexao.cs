using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CustosFaturados.Dao
{
    public class Conexao
    {
        private string ConectionString = "";
        public Conexao()
        {
            ConectionString = "Password=Programacao123;Persist Security Info=True;User ID=Rafael;Initial Catalog=CustosFaturados;Data Source=DESKTOP-H2E7R6H";
        }

        public System.Data.IDbConnection ConexaoBanco()
        {
            return new SqlConnection(ConectionString);
        }
    }
}
