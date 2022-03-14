using CustosFaturados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustosFaturados.Interfaces
{
    public interface IFatura
    {
        public List<Fatura> ListarFatura();
        public Fatura BuscarFaturaPorId(int Id);
        public void ExcluirFatura(int Id);
        public void EditarFatura(Fatura Fatura);
        public void CadastrarFatura(Fatura Fatura);

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
                    Exception exception = new Exception("Fonecedor é um campo obrigatório!");
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
