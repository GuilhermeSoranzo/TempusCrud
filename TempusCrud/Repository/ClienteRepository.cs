using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempusCrud.Data;
using TempusCrud.Models;

namespace TempusCrud.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly BancoContext _bancoContext;
        public ClienteRepository(BancoContext context)
        {
            _bancoContext = context;
        }

        public ClienteModel Adicionar(ClienteModel cliente)
        {
            _bancoContext.Clientes.Add(cliente);
            _bancoContext.SaveChanges();
            return cliente;
        }

        public ClienteModel Atualizar(ClienteModel cliente)
        {
            var clienteDB = GetCliente(cliente.Id);

            if (cliente == null) throw new Exception("Ocorreu um erro na atualização.");

            clienteDB.Nome = cliente.Nome;
            clienteDB.Cpf = cliente.Cpf;
            clienteDB.DataCadastro = cliente.DataCadastro;
            clienteDB.DataNascimento = cliente.DataNascimento;
            clienteDB.RendaFamiliar = cliente.RendaFamiliar;

            _bancoContext.Clientes.Update(clienteDB);
            _bancoContext.SaveChanges();

            return clienteDB;
        }

        public List<ClienteModel> BuscarTodos()
        {
            return _bancoContext.Clientes.OrderBy(x => x.Nome).ToList();

        }

        public ClienteModel GetCliente(long id)
        {
            return _bancoContext.Clientes.FirstOrDefault(x => x.Id == id);
        }

        public bool Remover(long id)
        {
            var clienteDB = GetCliente(id);

            if (clienteDB == null) throw new Exception("Ocorreu um erro na remoção do cliente.");

            _bancoContext.Clientes.Remove(clienteDB);
            _bancoContext.SaveChanges();

            return true;
        }

        public bool ValidarCPF(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }

        public bool ValidarCPFExistente(ClienteModel cliente)
        {
            if (_bancoContext.Clientes.Any(x => x.Cpf.Equals(cliente.Cpf) && x.Id != cliente.Id))
                return false;

            return true;
        }

    }
}
