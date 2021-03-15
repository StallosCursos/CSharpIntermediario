using System;
using UtilizandoLinq;

namespace SerializandoXML.Characteres
{
    public class BadChar: Character
    {
        public BadChar()
        {
        }

        public BadChar(string linha) : base(linha)
        {
        }

        public override string Alignment 
        {
            get => base.Alignment; 
            set 
            {
                if (value == "bad")
                    base.Alignment = value;
                else
                    throw new Exception("Este tipo pode ser apenas do tipo bad");
            } 
        }
    }
}
