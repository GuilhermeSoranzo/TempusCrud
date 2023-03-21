using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempusCrud.Models;

namespace TempusCrud.Repository
{
    public interface IClienteRepository
    {
        ClienteModel GetCliente(long id);
        List<ClienteModel> BuscarTodos();
        ClienteModel Adicionar(ClienteModel cliente);

        ClienteModel Atualizar(ClienteModel cliente);

        bool Remover(long id);

        bool ValidarCPF(string cpf);

        bool ValidarCPFExistente(ClienteModel cliente);
    }
}
