using DAO.Entitites;
using System.Collections.Generic;

namespace DAO.DataAcessObject
{
    public interface IEnderecoDao
    {
        void Delete(Endereco objeto);
        Endereco Find(int Id);
        Endereco Insert(Endereco objeto);
        List<Endereco> Select(Endereco Object);
        List<Endereco> SelectAll();
        void Update(Endereco objeto);
    }
}