using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace UtilizandoLinq
{
    [Serializable]
    [XmlRoot("Heroes")]
    public class Herois
    {
        public List<Character> Characters{ get; set; }
    }
}
