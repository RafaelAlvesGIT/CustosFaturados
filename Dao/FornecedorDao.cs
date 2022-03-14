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
    public class FornecedorDao : IFornecedor
    {
        private Conexao Conexao;
        private IDbConnection IDConnection;
        public List<Fornecedor> ListarFornecedor()
        {
            List<Fornecedor> Lista = new List<Fornecedor>();
            
            try
            {
                Conexao = new Conexao();
                IDConnection = Conexao.ConexaoBanco();

                var Sql = ("SELECT " +
                             "forne.*, Ver.* " +
                      "FROM " +
                           "Fornecedor As forne " +
                      "inner JOIN Vertical As Ver " +
                            "on Ver.Id = forne.IdVertical"
                      );

                IDConnection.Open();
                Lista = IDConnection.Query<Fornecedor, Vertical, Fornecedor > (Sql,
                map: (fornecedor, vertical) =>
                {
                    if (vertical != null)
                    {
                        fornecedor.Vertical = new Vertical();
                        fornecedor.Vertical.Id = vertical.Id;
                        fornecedor.Vertical.Descricao = vertical.Descricao;
                    }
                    return fornecedor;
                }, splitOn: "Id").ToList();
            }catch(Exception exception)
            {
                throw exception;
            }
            finally{
                IDConnection.Close();
            }
            return Lista;
        }
        public Fornecedor BuscarFornecedorPorId(int Id)
        {
            Fornecedor Fornecedor = new Fornecedor();

            try
            {
                Conexao = new Conexao();
                IDConnection = Conexao.ConexaoBanco();

                var Sql = ("SELECT " +
                             "forne.*, Ver.* " +
                          "FROM " +
                             "Fornecedor As forne " +
                          "inner JOIN Vertical As Ver " +
                            "on Ver.Id = forne.IdVertical " +
                          "WHERE " +
                            "forne.Id = @Id"
                          );

                IDConnection.Open();
                Fornecedor = IDConnection.Query<Fornecedor, Vertical, Fornecedor>(Sql,
                map: (fornecedor, vertical) =>
                {
                    if (vertical != null)
                    {
                        fornecedor.Vertical = new Vertical();
                        fornecedor.Vertical.Id = vertical.Id;
                        fornecedor.Vertical.Descricao = vertical.Descricao;
                    }
                    return fornecedor;
                }, new { id = Id }, splitOn: "Id").FirstOrDefault();
            }
            catch(Exception exception)
            {
                throw exception;
            }
            finally
            {
                IDConnection.Close();
            }

            return Fornecedor;
        }
        public void ExcluirFornecedor(int Id)
        {
            try
            {
                Conexao = new Conexao();
                IDConnection = Conexao.ConexaoBanco();

                var Sql = ("DELETE FROM " +
                           "Fornecedor WHERE Id = @Id"
                      );

                IDConnection.Open();
                IDConnection.Query(Sql, new { Id = Id });
               
            }
            catch(Exception exception)
            {
                throw exception;
            }
            finally
            {
                IDConnection.Close();
            }
        }
        public void EditarFornecedor(Fornecedor Fornecedor)
        {
            try
            {
                Conexao = new Conexao();
                IDConnection = Conexao.ConexaoBanco();

                var Sql = ("Update Fornecedor "+
                                "Set Nome = @Nome, Link = @Link, Historico = @Historico, IdVertical = @IdVertical " +
                           "Where "+
                                "Id = @Id ");
                IDConnection.Open();
                IDConnection.Query(Sql,
                    new { 
                        Nome = Fornecedor.Nome,
                        Link = Fornecedor.Link,
                        Historico = Fornecedor.Historico, 
                        IdVertical = Fornecedor.Vertical.Id,
                        Id = Fornecedor.Id
                    }
                );
            }
            catch(Exception exception)
            {
                throw exception;
            }
            finally
            {
                IDConnection.Close();
            }
        }
        public void CadastrarFornecedor(Fornecedor Fornecedor)
        {
           try
            {
                Conexao = new Conexao();
                IDConnection = Conexao.ConexaoBanco();

                var Sql = ("Insert Into Fornecedor "+
                                "(Nome, Link, Historico, IdVertical) " +
                            "VALUES " +
                                "(@Nome, @Link, @Historico, @IdVertical) " 
                           );
                IDConnection.Open();
                IDConnection.Query(Sql,
                    new { 
                        Nome = Fornecedor.Nome,
                        Link = Fornecedor.Link,
                        Historico = Fornecedor.Historico, 
                        IdVertical = Fornecedor.Vertical.Id,
                    }
                );
            }
            catch(Exception exception)
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
