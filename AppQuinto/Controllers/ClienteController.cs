using AppQuinto.Models;
using AppQuinto.Repository;
using AppQuinto.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace AppQuinto.Controllers
{
    public class ClienteController : Controller
    {

        private IClienteRepository _ClienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _ClienteRepository = clienteRepository;
        }

        public IActionResult Index()
        {
            return View(_ClienteRepository.ObterTodosClientes());
        }
        [HttpGet]
        public IActionResult CadastrarCliente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastrarCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _ClienteRepository.Cadastrar(cliente);
            }
            return View();
        }
        [HttpGet]
        public IActionResult AtualizarCliente(int id)
        {
            return View(_ClienteRepository.ObterCliente(id));
        }
        [HttpPost]
        public IActionResult AtualizarCliente(Cliente cliente)
        {
            _ClienteRepository.Atualizar(cliente);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult DetalhesCliente(int id)
        {
            return View(_ClienteRepository.ObterCliente(id));
        }
        [HttpPost]
        public IActionResult DetalhesCliente(Cliente cliente)
        {
            _ClienteRepository.Atualizar(cliente);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult ExcluirCliente(int id)
        {
            _ClienteRepository.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }



}

