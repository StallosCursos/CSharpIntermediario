using System;
using System.Collections.Generic;
using System.Text;

namespace DAO.Entitites
{
    public class Endereco
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }

        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public TipoEndereco Tipo { get; set; }
    }
}
