using CustosFaturados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustosFaturados.Interfaces
{
    public interface IFornecedor
    {
        public List<Fornecedor> ListarFornecedor();
        public Fornecedor BuscarFornecedorPorId(int Id);
        public void ExcluirFornecedor(int Id);
        public void EditarFornecedor(Fornecedor Fornecedor);
        public void CadastrarFornecedor(Fornecedor Fornecedor);
    }
}
