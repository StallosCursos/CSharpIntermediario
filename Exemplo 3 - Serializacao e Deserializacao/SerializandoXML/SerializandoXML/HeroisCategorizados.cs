using SerializandoXML.Characteres;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace UtilizandoLinq
{
    [Serializable]
    [XmlRoot("Heroes")]
    public class HeroisCategorizados
    {
        [XmlArray("Chars")]
        [XmlArrayItem("Good", typeof(GoodChar))]
        [XmlArrayItem("Bad", typeof(BadChar))]
        public List<Character> Characters { get; set; }
    }
}
