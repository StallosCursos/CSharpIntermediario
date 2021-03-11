using System;
using System.Collections.Generic;
using System.Text;

namespace UtilizandoLinq
{
    public class Character
    {
        public Character(string linha)
        {
            string[] colunas = linha.Split(',');

            this.Name = colunas[0];
            this.Alignment = colunas[1];
            this.Intelligence = Int32.Parse(colunas[2]);
            this.Strength = Int32.Parse(colunas[3]);
            this.Speed = Int32.Parse(colunas[4]);
            this.Durability = Int32.Parse(colunas[5]);
            this.Power = Int32.Parse(colunas[6]);
            this.Combat = Int32.Parse(colunas[7]);
            this.Total = Int32.Parse(colunas[8]);
        }

        public string Name { get; private set; }
        public string Alignment { get; private set; }
        public int Intelligence { get; private set; }
        public int Strength { get; private set; }
        public int Speed { get; private set; }
        public int Durability { get; private set; }
        public int Power { get; private set; }
        public int Combat { get; private set; }
        public int Total { get; private set; }
    }
}
