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
    public class FornecedorController : Controller
    {
        private IFornecedor IFornecedor;
        private IVertical IVertical;
        // GET: FornecedorController
        public ActionResult Index()
        {
            try
            {
                IFornecedor = new FornecedorDao();
                ViewBag.ListaFornecedor = IFornecedor.ListarFornecedor();
                if (TempData["sucesso"] == null)
                {
                    TempData["sucesso"] = "Listagem Realizada com sucesso!";
                }
            }
            catch(Exception exception)
            {
                TempData["erro"] = "Erro: "+ exception;
            }

            return View();
        }

        public ActionResult AbrirEdicao(int id)
        {
            Fornecedor fornecedor = new Fornecedor();

            try
            {
                IVertical = new VerticalDao();
                ViewBag.ListaVertical = IVertical.ListaVertical();

                if (TempData["EditarFornecedor"] != null)
                {
                    fornecedor = Newtonsoft.Json.JsonConvert.DeserializeObject<Fornecedor>((string)TempData["EditarFornecedor"]);
                    TempData.Remove("EditarFornecedor");
                }
                else
                {
                    IFornecedor = new FornecedorDao();
                    fornecedor = IFornecedor.BuscarFornecedorPorId(id);
                }   
            }
            catch (Exception exception)
            {
                TempData["erro"] = "Erro: " + exception;
            }

            return View(fornecedor);
        }

        public ActionResult AbrirCadastro()
        {
            Fornecedor fornecedor = new Fornecedor();
            try { 
                IVertical = new VerticalDao();
                ViewBag.ListaVertical = IVertical.ListaVertical();
                if (TempData["CadastrarFornecedor"] != null)
                {
                     fornecedor = Newtonsoft.Json.JsonConvert.DeserializeObject<Fornecedor>((string)TempData["CadastrarFornecedor"]);
                     TempData.Remove("CadastrarFornecedor");
                }
            }
            catch (Exception exception)
            {
                TempData["erro"] = "Erro: " + exception;
            }
            return View(fornecedor);
        }

        public ActionResult Cadastrar(Fornecedor fornecedor)
        {
            try
            {
                String validacao = fornecedor.ValidarFornecedor(fornecedor);
                if (validacao == "")
                {
                    IFornecedor = new FornecedorDao();
                    IFornecedor.CadastrarFornecedor(fornecedor);
                    TempData["sucesso"] = "Cadastro Realizado com sucesso!";
                }else{
                    TempData["CadastrarFornecedor"] = Newtonsoft.Json.JsonConvert.SerializeObject(fornecedor);
                    TempData["erro"] = validacao;
                    return RedirectToAction("AbrirCadastro", "Fornecedor");
                }
            }
            catch (Exception exception)
            {
                TempData["erro"] = "Erro: " + exception;
            }

            return RedirectToAction("Index", "Fornecedor");
        }

        public ActionResult Editar(Fornecedor fornecedor)
        {
            try
            {
                String validacao = fornecedor.ValidarFornecedor(fornecedor);
                if (validacao == "")
                {
                    IFornecedor = new FornecedorDao();
                    IFornecedor.EditarFornecedor(fornecedor);
                    TempData["sucesso"] = "Edição Realizada com sucesso!";
                }
                else
                {
                    TempData["EditarFornecedor"] = Newtonsoft.Json.JsonConvert.SerializeObject(fornecedor);
                    TempData["erro"] = validacao;
                    return RedirectToAction("AbrirEdicao", new { id = fornecedor.Id } );
                }
            }
            catch (Exception exception)
            {
                TempData["erro"] = "Erro: " + exception;
            }

            return RedirectToAction("Index", "Fornecedor");
        }

        public ActionResult Excluir(int id)
        {
            try
            {
                IFornecedor = new FornecedorDao();
                IFornecedor.ExcluirFornecedor(id);
                TempData["sucesso"] = "Exclusão Realizada com sucesso!";
            }
            catch (Exception exception)
            {
                TempData["erro"] = "Erro: " + exception;
            }

            return RedirectToAction("Index", "Fornecedor");
        }
    }
}
