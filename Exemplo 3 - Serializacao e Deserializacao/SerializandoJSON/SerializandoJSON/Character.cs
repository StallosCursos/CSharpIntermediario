using System;
using System.Collections.Generic;
using System.Text;

namespace UtilizandoLinq
{
    [Serializable]
    public class Character
    {
        public Character() { }

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

        public string Name { get; set; }
        public virtual string Alignment { get; set; }
        public int Intelligence { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public int Durability { get; set; }
        public int Power { get; set; }
        public int Combat { get; set; }
        public int Total { get; set; }
    }
}
