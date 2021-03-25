using ConsoleTables;
using DataGateway.DataGateWay;
using DataGateway.Entitites;
using System;

namespace DataGateway
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataGateWay<Cliente> clienteGateWay = new ClienteDataGateWay();
            IDataGateWay<Endereco> enderecoGateway = new EnderecoDataGateWay();

            var cliente = InserindoCliente(clienteGateWay, enderecoGateway);
            AtualizandoCliente(clienteGateWay, cliente);

            ExibindoTodosClientes(clienteGateWay);

            BuscandoERemovendoCliente(clienteGateWay, enderecoGateway, cliente);

            ExibindoTodosClientes(clienteGateWay);
        }

        private static void ExibindoTodosClientes(IDataGateWay<Cliente> clienteGateWay)
        {
            var Clientes = clienteGateWay.SelectAll();

            ConsoleTable consoletable = new ConsoleTable();
            consoletable.AddColumn(new[] { "Id", "Nome", "Sobrenome", "Idade", "DataNascimento" });

            Clientes.ForEach(cliente => consoletable.AddRow(
                cliente.Id, cliente.Nome, cliente.Sobrenome, cliente.Idade, cliente.DataNascimento
            ));

            consoletable.Write();
        }

        private static void BuscandoERemovendoCliente(IDataGateWay<Cliente> clienteGateway, IDataGateWay<Endereco> enderecoGateway, Cliente clienteInserido)
        {
            var cliente = clienteGateway.Find(clienteInserido.Id);
            cliente.Enderecos = enderecoGateway.Select(new Endereco() { IdCliente = cliente.Id });

            cliente.Enderecos.ForEach(e => enderecoGateway.Delete(e));

            clienteGateway.Delete(cliente);
        }

        private static void AtualizandoCliente(IDataGateWay<Cliente> clienteGateway, Cliente clienteInserido)
        {
            clienteInserido.Nome = "Cliente Alterado";
            clienteGateway.Update(clienteInserido);
        }

        private static Cliente InserindoCliente(IDataGateWay<Cliente> clienteGateWay, IDataGateWay<Endereco> enderecoGateway)
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

            clienteGateWay.Insert(cliente);
            cliente.Enderecos.ForEach(endereco =>
            {
                endereco.IdCliente = cliente.Id;
                enderecoGateway.Insert(endereco);
            });

            return cliente;
        }
    }
}
