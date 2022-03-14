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
    public class FaturaDao : IFatura
    {
        private Conexao Conexao;
        private IDbConnection IDConnection;
        public List<Fatura> ListarFatura()
        {
            List<Fatura> Lista = new List<Fatura>();

            try
            {
                Conexao = new Conexao();
                IDConnection = Conexao.ConexaoBanco();

                var Sql = ("SELECT " +
                             "fa.*, forn.* " +
                      "FROM " +
                           "Faturas As fa " +
                      "inner JOIN Fornecedor As forn " +
                            "on fa.IdFornecedor = forn.Id"
                      );

                IDConnection.Open();
                Lista = IDConnection.Query<Fatura, Fornecedor, Fatura>(Sql,
                map: (fatura, fornecedor) =>
                {
                    if (fornecedor != null)
                    {
                        fatura.Fornecedor = new Fornecedor();
                        fatura.Fornecedor.Id = fornecedor.Id;
                        fatura.Fornecedor.Nome = fornecedor.Nome;
                        fatura.Fornecedor.Link = fornecedor.Link;
                        fatura.Fornecedor.Historico = fornecedor.Historico;
                        fatura.Fornecedor.Vertical = fornecedor.Vertical;
                    }
                    return fatura;
                }, splitOn: "Id").ToList();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                IDConnection.Close();
            }
            return Lista;
        }
        public Fatura BuscarFaturaPorId(int Id)
        {
            Fatura Fatura = new Fatura();

            try
            {
                Conexao = new Conexao();
                IDConnection = Conexao.ConexaoBanco();

                var Sql = ("SELECT " +
                               "fa.*, forn.* " +
                            "FROM " +
                                "Faturas As fa " +
                            "inner JOIN Fornecedor As forn " +
                                "on fa.IdFornecedor = forn.Id " + 
                            "WHERE fa.Id = @Id"
                        );

                IDConnection.Open();
                Fatura = IDConnection.Query<Fatura, Fornecedor, Fatura>(Sql,
                map: (fatura, fornecedor) =>
                {
                    if (fornecedor != null)
                    {
                        fatura.Fornecedor = new Fornecedor();
                        fatura.Fornecedor.Id = fornecedor.Id;
                        fatura.Fornecedor.Nome = fornecedor.Nome;
                        fatura.Fornecedor.Link = fornecedor.Link;
                        fatura.Fornecedor.Historico = fornecedor.Historico;
                        fatura.Fornecedor.Vertical = fornecedor.Vertical;
                    }
                    return fatura;
                }, new { Id = Id}, splitOn: "Id").FirstOrDefault();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                IDConnection.Close();
            }

            return Fatura;
        }
        public void ExcluirFatura(int Id)
        {
            try
            {
                Conexao = new Conexao();
                IDConnection = Conexao.ConexaoBanco();

                var Sql = ("DELETE FROM " +
                           "Faturas WHERE Id = @Id"
                      );

                IDConnection.Open();
                IDConnection.Query(Sql, new { Id = Id });

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                IDConnection.Close();
            }
        }
        public void EditarFatura(Fatura Fatura)
        {
            try
            {
                Conexao = new Conexao();
                IDConnection = Conexao.ConexaoBanco();

                var Sql = ("Update Faturas " +
                                "Set Descricao = @Descricao, IdFornecedor = @IdFornecedor, Valor = @Valor " +
                           "Where " +
                                "Id = @Id ");
                IDConnection.Open();
                IDConnection.Query(Sql,
                    new
                    {
                        Descricao = Fatura.Descricao,
                        IdFornecedor = Fatura.Fornecedor.Id,
                        Valor = Fatura.Valor,
                        Id = Fatura.Id
                    }
                );
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                IDConnection.Close();
            }
        }
        public void CadastrarFatura(Fatura Fatura)
        {
            try
            {
                Conexao = new Conexao();
                IDConnection = Conexao.ConexaoBanco();

                var Sql = ("Insert Into Faturas " +
                                "(Descricao, IdFornecedor, Valor, DataLancamento) " +
                            "VALUES " +
                                "(@Descricao, @IdFornecedor, @Valor, @DataLancamento) "
                           );
                IDConnection.Open();
                IDConnection.Query(Sql,
                    new
                    {
                        Descricao = Fatura.Descricao,
                        IdFornecedor = Fatura.Fornecedor.Id,
                        Valor = Fatura.Valor,
                        DataLancamento = Fatura.DataLancamento
                    }
                );
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                IDConnection.Close();
            }
        }
    }
}
