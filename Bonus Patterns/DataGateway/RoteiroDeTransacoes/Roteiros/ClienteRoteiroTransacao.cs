using DAO.DataAcessObject;
using DAO.Entitites;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RoteiroDeTransacoes.Roteiros
{
    public class ClienteRoteiroTransacao
    {
        private readonly IClienteDao _clienteDao;
        private readonly IEnderecoDao _enderecoDao;

        public ClienteRoteiroTransacao()
        {
            _clienteDao = new ClienteDao();
            _enderecoDao = new EnderecoDao(_clienteDao.AcessoDados);
        }

        public Cliente InserirNovoCliente(Cliente cliente)
        {
            _clienteDao.AcessoDados.BeginTransaction();
            try
            {
                _clienteDao.Insert(cliente);
                cliente.Enderecos.ForEach(endereco =>
                {
                    endereco.IdCliente = cliente.Id;
                    _enderecoDao.Insert(endereco);
                });

                _clienteDao.AcessoDados.Commit();
                return cliente;
            }
            catch (Exception error)
            {
                _clienteDao.AcessoDados.RollBack();
                throw error;
            }
        }

        public Cliente RemoverCliente(Cliente cliente)
        {
            _clienteDao.AcessoDados.BeginTransaction();
            try
            {
                cliente.Enderecos.ForEach(endereco =>
                {
                    endereco.IdCliente = cliente.Id;
                    _enderecoDao.Delete(endereco);
                });
                _clienteDao.Delete(cliente);

                _clienteDao.AcessoDados.Commit();
                return cliente;
            }
            catch (Exception error)
            {
                _clienteDao.AcessoDados.RollBack();
                throw error;
            }
        }
    }
}
