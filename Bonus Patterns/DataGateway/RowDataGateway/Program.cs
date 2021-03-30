using ConsoleTables;
using DataGateway.Entitites;
using RowDataGateway.GateWay.Cliente;
using System;
using System.Collections.Generic;

namespace RowDataGateway
{
    class Program
    {
        static void Main(string[] args)
        {
            ClienteRowGateWay clienteRowGateWay = new ClienteRowGateWay();

            InserindoCliente(clienteRowGateWay);
            ExibindoTodosClientes();

            AtualizandoCliente(clienteRowGateWay.Id);
            ExibindoTodosClientes();

            RemovendoCliente(clienteRowGateWay);
            ExibindoTodosClientes();
        }

        private static void ExibindoTodosClientes()
        {
            var Clientes = new ClienteFinderRowGate().Select();

            ConsoleTable consoletable = new ConsoleTable();
            consoletable.AddColumn(new[] { "Id", "Nome", "Sobrenome", "Idade", "DataNascimento" });

            Clientes.ForEach(cliente => consoletable.AddRow(
                cliente.Id, cliente.Nome, cliente.Sobrenome, cliente.Idade, cliente.DataNascimento
            ));

            consoletable.Write();
        }

        private static void RemovendoCliente(ClienteRowGateWay clienteRowGateWay)
        {
            clienteRowGateWay.Delete();
        }

        private static void AtualizandoCliente(int Id)
        {
            var clienteRowGateWay = new ClienteFinderRowGate().FindById(Id);
            clienteRowGateWay.Nome = "José Alterado";
            clienteRowGateWay.Update();
        }

        private static void InserindoCliente(ClienteRowGateWay clienteRowGateWay)
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

            clienteRowGateWay.Idade = cliente.Idade;
            clienteRowGateWay.Nome = cliente.Nome;
            clienteRowGateWay.Sobrenome = cliente.Sobrenome;
            clienteRowGateWay.DataNascimento = cliente.DataNascimento;

            clienteRowGateWay.Insert();

            cliente.Id = clienteRowGateWay.Id;
        }
    }
}
