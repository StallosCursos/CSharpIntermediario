using DAO.Entitites;
using System.Collections.Generic;

namespace DAO.DataAcessObject
{
    public interface IClienteDao: IDao
    {
        void Delete(Cliente objeto);
        Cliente Find(Cliente objeto);
        Cliente Find(int Id);
        Cliente Insert(Cliente objeto);
        List<Cliente> Select(Cliente Object);
        List<Cliente> SelectAll();
        void Update(Cliente objeto);
    }
}