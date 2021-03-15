using System;
using System.Collections.Generic;
using System.Text;
using UtilizandoLinq;

namespace SerializandoXML.Characteres
{
    public class GoodChar : Character
    {
        public GoodChar()
        {
        }

        public GoodChar(string linha) : base(linha)
        {
        }

        public override string Alignment 
        { 
            get => base.Alignment;
            set
            {
                if (value == "good")
                    base.Alignment = value;
                else
                    throw new Exception("este tipo pode ser apenas do tipo good");
            }
        }
    }
}
