using ConsoleTables;
using PersistenciaCompleta.Dados;
using PersistenciaCompleta.Entidades;
using System;

namespace PersistenciaCompleta
{
    class Program
    {
        static void Main(string[] args)
        {
            ClienteDados clienteDados = new ClienteDados();

            var clienteInserido = InserindoNovoCliente(clienteDados);

            AtualizandoCliente(clienteDados, clienteInserido);

            ExibindoTodosClientes(clienteDados);
            
            BuscandoERemovendoCliente(clienteDados, clienteInserido);

            ExibindoTodosClientes(clienteDados);
        }

        private static void BuscandoERemovendoCliente(ClienteDados clienteDados, Cliente clienteInserido)
        {
            EnderecoDados enderecoDados = new EnderecoDados();

            var cliente = clienteDados.Find(clienteInserido.Id);

            cliente.Enderecos.ForEach(e => enderecoDados.Delete(e));

            clienteDados.Delete(cliente);
        }

        private static void ExibindoTodosClientes(ClienteDados clienteDados)
        {
            var Clientes = clienteDados.Select();

            ConsoleTable consoletable = new ConsoleTable();
            consoletable.AddColumn(new[] { "Id", "Nome", "Sobrenome", "Idade", "DataNascimento" });

            Clientes.ForEach(cliente => consoletable.AddRow(
                cliente.Id, cliente.Nome, cliente.Sobrenome, cliente.Idade, cliente.DataNascimento
            ));

            consoletable.Write();
        }

        private static void AtualizandoCliente(ClienteDados clienteDados, Cliente clienteInserido)
        {
            clienteInserido.Nome = "Cliente Alterado";
            clienteDados.Update(clienteInserido);
        }

        private static Cliente InserindoNovoCliente(ClienteDados clienteDados)
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

            EnderecoDados enderecoDados = new EnderecoDados();

            cliente.Id = clienteDados.Insert(cliente);
            cliente.Enderecos.ForEach(endereco => {
                endereco.IdCliente = cliente.Id;
                endereco.Id = enderecoDados.Insert(endereco);
            });

            return cliente;
        }
    }
}
