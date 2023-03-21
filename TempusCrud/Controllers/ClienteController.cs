using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempusCrud.Models;
using TempusCrud.Repository;

namespace TempusCrud.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _clienteRepository;


        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IActionResult Index()
        {
            var clientes = _clienteRepository.BuscarTodos();
            return View(clientes);
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            var cliente = _clienteRepository.GetCliente(id);
            return View(cliente);
        }

        public IActionResult Remover(long id)
        {
            var cliente = _clienteRepository.GetCliente(id);
            return View(cliente);
        }

        public IActionResult RemoverCliente(long id)
        {
            _clienteRepository.Remover(id);
            TempData["MensagemSucesso"] = "Cliente removido com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Adicionar(ClienteModel cliente)
        {
            if (ModelState.IsValid)
            {
                if (!_clienteRepository.ValidarCPF(cliente.Cpf))
                {
                    TempData["MensagemErro"] = "Não foi possível salvar o cliente. CPF está incorreto!";
                    return RedirectToAction("Index");
                }

                if (!_clienteRepository.ValidarCPFExistente(cliente))
                {
                    TempData["MensagemErro"] = "Não foi possível salvar o cliente. O CPF já está vinculado a outro cliente!";
                    return RedirectToAction("Index");
                }

                _clienteRepository.Adicionar(cliente);
                TempData["MensagemSucesso"] = "Cliente cadastrado com sucesso!";
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        [HttpPost]
        public IActionResult Alterar(ClienteModel cliente)
        {
            if(ModelState.IsValid)
            {
                if (!_clienteRepository.ValidarCPF(cliente.Cpf))
                {
                    TempData["MensagemErro"] = "Não foi possível salvar o cliente. CPF está incorreto!";
                    return RedirectToAction("Index");
                }

                if(!_clienteRepository.ValidarCPFExistente(cliente))
                {
                    TempData["MensagemErro"] = "Não foi possível salvar o cliente. O CPF já está vinculado a outro cliente!";
                    return RedirectToAction("Index");
                }

                _clienteRepository.Atualizar(cliente);
                TempData["MensagemSucesso"] = "Cliente alterado com sucesso!";
                return RedirectToAction("Index");
            }
            return View(cliente);
            
        }
    }
}
