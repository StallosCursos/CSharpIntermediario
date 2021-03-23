using System;
using System.Collections.Generic;

namespace PersistenciaCompleta.Entidades
{
    public class Cliente
    {
        private int _idade;
        private DateTime _dataNascimento;

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int Idade { get => _idade; }
        public DateTime DataNascimento
        {
            get => _dataNascimento;
            set
            {
                _dataNascimento = value;
                CalcularIdade(value);
            }
        }

        private void CalcularIdade(DateTime value)
        {
            _idade = DateTime.Now.Year - _dataNascimento.Year;
            if (DateTime.Now.DayOfYear < _dataNascimento.DayOfYear)
                _idade -= 1;
        }

        public List<Endereco> Enderecos { get; set; }
    }
}