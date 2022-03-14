using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustosFaturados.Models
{
    public class Fatura
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Valor { get; set; }
        public DateTime DataLancamento { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public string ValidarFatura(Fatura Fatura)
        {
            try
            {
                if (Fatura.Valor == null)
                {
                    Exception exception = new Exception("Valor é um campo obrigatório!");
                    throw exception;
                }

                if (Fatura.Fornecedor == null)
                {
                    Exception exception = new Exception("Fornecedor é um campo obrigatório!");
                    throw exception;
                }

                if (Fatura.Descricao == null)
                {
                    Exception exception = new Exception("Descrição é um campo obrigatório");
                    throw exception;
                }

                if (Fatura.DataLancamento == null)
                {
                    Exception exception = new Exception("Data Lançamento é um campo obrigatório");
                    throw exception;
                }
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
            return "";
        }
    }
}
