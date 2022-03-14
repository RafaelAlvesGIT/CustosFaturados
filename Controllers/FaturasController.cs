using CustosFaturados.Dao;
using CustosFaturados.Interfaces;
using CustosFaturados.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustosFaturados.Controllers
{
    public class FaturasController : Controller
    {
        private IFatura IFatura;
        private IFornecedor IFornecedor;
        // GET: FornecedorController
        public ActionResult Index()
        {
            try
            {
                IFatura = new FaturaDao();
                ViewBag.ListaFatura = IFatura.ListarFatura();
                
                if (TempData["sucesso"] == null)
                {
                    TempData["sucesso"] = "Listagem Realizada com sucesso!";
                }
            }
            catch (Exception exception)
            {
                TempData["erro"] = "Erro: " + exception;
            }

            return View();
        }

        public ActionResult AbrirEdicao(int id)
        {
            Fatura Fatura = new Fatura();

            try
            {
                IFornecedor = new FornecedorDao();
                ViewBag.ListaFornecedor = IFornecedor.ListarFornecedor();

                if (TempData["EditarFatura"] != null)
                {
                    Fatura = Newtonsoft.Json.JsonConvert.DeserializeObject<Fatura>((string)TempData["EditarFatura"]);
                    TempData.Remove("EditarFatura");
                }
                else
                {
                    IFatura = new FaturaDao();
                    Fatura = IFatura.BuscarFaturaPorId(id);
                }
            }
            catch (Exception exception)
            {
                TempData["erro"] = "Erro: " + exception;
            }

            return View(Fatura);
        }

        public ActionResult AbrirCadastro()
        {
            Fatura fatura = new Fatura();
            try
            {
                IFornecedor = new FornecedorDao();
                ViewBag.ListaFornecedor = IFornecedor.ListarFornecedor();
                if (TempData["CadastrarFatura"] != null)
                {
                    fatura = Newtonsoft.Json.JsonConvert.DeserializeObject<Fatura>((string)TempData["CadastrarFatura"]);
                    TempData.Remove("CadastrarFatura");
                }
            }
            catch (Exception exception)
            {
                TempData["erro"] = "Erro: " + exception;
            }
            return View(fatura);
        }

        public ActionResult Cadastrar(Fatura fatura)
        {
            try
            {
                String validacao = fatura.ValidarFatura(fatura);
                if (validacao == "")
                {
                    IFatura = new FaturaDao();
                    fatura.DataLancamento = DateTime.Now;
                    IFatura.CadastrarFatura(fatura);
                    TempData["sucesso"] = "Cadastro Realizado com sucesso!";
                }
                else
                {
                    TempData["CadastrarFatura"] = Newtonsoft.Json.JsonConvert.SerializeObject(fatura);
                    TempData["erro"] = validacao;
                    return RedirectToAction("AbrirCadastro", "Faturas");
                }
            }
            catch (Exception exception)
            {
                TempData["erro"] = "Erro: " + exception;
            }

            return RedirectToAction("Index", "Faturas");
        }

        public ActionResult Editar(Fatura fatura)
        {
            try
            {
                String validacao = fatura.ValidarFatura(fatura);
                if (validacao == "")
                {
                    IFatura = new FaturaDao();
                    IFatura.EditarFatura(fatura);
                    TempData["sucesso"] = "Edição Realizada com sucesso!";
                }
                else
                {
                    TempData["EditarFornecedor"] = Newtonsoft.Json.JsonConvert.SerializeObject(fatura);
                    TempData["erro"] = validacao;
                    return RedirectToAction("AbrirEdicao", new { id = fatura.Id });
                }
            }
            catch (Exception exception)
            {
                TempData["erro"] = "Erro: " + exception;
                TempData["EditarFornecedor"] = Newtonsoft.Json.JsonConvert.SerializeObject(fatura);
                return RedirectToAction("AbrirEdicao", new { id = fatura.Id });
            }

            return RedirectToAction("Index", "Faturas");
        }

        public ActionResult Excluir(int id)
        {
            try
            {
                IFatura = new FaturaDao();
                IFatura.ExcluirFatura(id);
                TempData["sucesso"] = "Exclusão Realizada com sucesso!";
            }
            catch (Exception exception)
            {
                TempData["erro"] = "Erro: " + exception;
            }

            return RedirectToAction("Index", "Faturas");
        }
    }
}
