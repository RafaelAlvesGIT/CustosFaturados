using CustosFaturados.Interfaces;
using CustosFaturados.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CustosFaturados.Dao
{
    public class VerticalDao : IVertical
    {
        private Conexao Conexao;
        private IDbConnection IDConnection;
        public List<Vertical> ListaVertical()
        {
            List<Vertical> Lista = new List<Vertical>();

            try
            {
                Conexao = new Conexao();
                IDConnection = Conexao.ConexaoBanco();

                var Sql = ("SELECT " +
                             "* " +
                      "FROM " +
                           "Vertical");
               

                IDConnection.Open();
                Lista = IDConnection.Query<Vertical>(Sql).ToList();
            }
            catch
            {

            }
            finally
            {
                IDConnection.Close();
            }

            return Lista;
        }
    }
}
