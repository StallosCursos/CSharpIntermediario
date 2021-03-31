using ConsoleTables;
using DAO.DataAcessObject;
using DAO.Entitites;
using RoteiroDeTransacoes.Roteiros;
using System;

namespace DAO
{
    class Program
    {
        static void Main(string[] args)
        {
            IClienteDao clienteDao = new ClienteDao();
            IEnderecoDao enderecoDao = new EnderecoDao();

            var cliente = InserindoCliente();
            
            ExibindoTodosClientes(clienteDao);

            AtualizandoCliente(clienteDao, cliente);

            ExibindoTodosClientes(clienteDao);

            BuscandoERemovendoCliente(clienteDao, enderecoDao, cliente);

            ExibindoTodosClientes(clienteDao);
        }

        private static void ExibindoTodosClientes(IClienteDao clientDao)
        {
            var Clientes = clientDao.SelectAll();

            ConsoleTable consoletable = new ConsoleTable();
            consoletable.AddColumn(new[] { "Id", "Nome", "Sobrenome", "Idade", "DataNascimento" });

            Clientes.ForEach(cliente => consoletable.AddRow(
                cliente.Id, cliente.Nome, cliente.Sobrenome, cliente.Idade, cliente.DataNascimento
            ));

            consoletable.Write();
        }

        private static void BuscandoERemovendoCliente(IClienteDao clientDao, IEnderecoDao enderecoDao, Cliente clienteInserido)
        {
            var cliente = clientDao.Find(clienteInserido.Id);
            cliente.Enderecos = enderecoDao.Select(new Endereco { IdCliente = cliente.Id });

            new ClienteRoteiroTransacao().RemoverCliente(cliente);
        }

        private static void AtualizandoCliente(IClienteDao clientDao, Cliente clienteInserido)
        {
            clienteInserido.Nome = "Cliente Alterado";
            clientDao.Update(clienteInserido);
        }

        private static Cliente InserindoCliente()
        {
            Cliente cliente = new Cliente()
            {
                DataNascimento = DateTime.Parse("04/05/1982"),
                Nome = "Cliente Teste",
                Sobrenome = "Teste",
                Enderecos = new System.Collections.Generic.List<Endereco>()
                {
                    new Endereco { Logradouro = "a", Bairro = "b", Cidade = "c", Estado = "e", Cep = "18135070", Tipo = TipoEndereco.Residencial },
                    new Endereco { Logradouro = "f", Bairro = "g", Cidade = "h", Estado = "i", Cep = "18135070", Tipo = TipoEndereco.Comercial }
                }
            };

            return new ClienteRoteiroTransacao().InserirNovoCliente(cliente);
        }
    }
}
