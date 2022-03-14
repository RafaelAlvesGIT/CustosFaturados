using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustosFaturados.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Link { get; set; }
        public Boolean Historico { get; set; }
        public Vertical Vertical { get; set; }

        public string ValidarFornecedor(Fornecedor Fornecedor)
        {
            try
            {
                if (Fornecedor.Link == null)
                {
                    Exception exception = new Exception("Link é um campo obrigatório!");
                    throw exception;
                }

                if (Fornecedor.Nome == null)
                {
                    Exception exception = new Exception("Nome é um campo obrigatório!");
                    throw exception;
                }

                if (Fornecedor.Vertical == null)
                {
                    Exception exception = new Exception("Vertical é um campo obrigatório");
                    throw exception;
                }
            }
            catch(Exception exception){
                return exception.Message;
            }
            return "";
        }
    }
}