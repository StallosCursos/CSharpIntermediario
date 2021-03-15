using SerializandoXML.Characteres;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UtilizandoLinq;

namespace SerializandoXML
{
    class Program: ProgramBase
    {
        static void Main(string[] args)
        {
            IEnumerable<string> Arquivo = LerArquivoCsv("./charcters_stats.csv");
            IEnumerable<Character> characters = Arquivo.Select(t => new Character(t));

            SerializarClasseCharacters(characters);
            SerializandoClasseHeroes(characters);
            SerializandoComHerança(Arquivo);
        }

        private static void SerializandoComHerança(IEnumerable<string> Arquivo)
        {
            var good = Arquivo.Where(t => t.Split(',')[1] == "good").Select(t => new GoodChar(t));
            var bad = Arquivo.Where(t => t.Split(',')[1] == "bad").Select(t => new BadChar(t));

            List<Character> characteresCategorizados = (good as IEnumerable<Character>).Concat(bad).ToList();

            HeroisCategorizados heroisCategorizados = new HeroisCategorizados();

            heroisCategorizados.Characters = characteresCategorizados;

            var xmlSerializer = new XmlSerializer(typeof(HeroisCategorizados));

            using (TextWriter textWriter = new StreamWriter("./herois-categorizados.xml"))
            {
                xmlSerializer.Serialize(textWriter, heroisCategorizados);
                textWriter.Close();
            }

            using (TextReader textReader = new StreamReader("./herois-categorizados.xml"))
            {
                var herois = xmlSerializer.Deserialize(textReader);
                ImprimirTabela("Chars do disco deserializados ", (herois as HeroisCategorizados).Characters);
            }
        }

        private static void SerializandoClasseHeroes(IEnumerable<Character> characters)
        {
            var xmlSerializer = new XmlSerializer(typeof(Herois));

            using (TextWriter textWriter = new StreamWriter("./herois.xml"))
            {
                var Herois = new Herois { Characters = characters.ToList() };
                xmlSerializer.Serialize(textWriter, Herois);
                textWriter.Close();
            }

            using (TextReader textReader = new StreamReader("./herois.xml"))
            {
                var herois = xmlSerializer.Deserialize(textReader);
                ImprimirTabela("Chars do disco deserializados ", (herois as Herois).Characters);
            }
        }

        private static void SerializarClasseCharacters(IEnumerable<Character> characters)
        {
            var Resultado = characters.Where(t => t.Alignment == "good");
            ImprimirTabela("Chars filtrados ", Resultado.ToList());

            var xmlSerializer = new XmlSerializer(typeof(List<Character>));

            // Serializando objeto para XML
            using (TextWriter textWriter = new StreamWriter("./chars.xml"))
            {
                xmlSerializer.Serialize(textWriter, Resultado.ToList());
                textWriter.Close();
            }

            // Deserializando XML para objeto
            using (TextReader textReader = new StreamReader("./chars.xml"))
            {
                var goodChars = xmlSerializer.Deserialize(textReader);
                ImprimirTabela("Chars do disco deserializados ", (List<Character>)goodChars);
            }
        }
    }
}
