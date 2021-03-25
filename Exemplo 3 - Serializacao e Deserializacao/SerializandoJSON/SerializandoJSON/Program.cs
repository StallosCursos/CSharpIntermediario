
using Newtonsoft.Json;
using SerializandoXML.Characteres;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using UtilizandoLinq;

namespace SerializandoJSON
{
    public class CharsJson
    {
        public List<Character> Characters { get; set; }
    }

    class Program: ProgramBase
    {
        static void Main(string[] args)
        {
            IEnumerable<string> Arquivo = LerArquivoCsv("./charcters_stats.csv");
            IEnumerable<Character> characters = Arquivo.Select(t => new Character(t));

            SerializacaoDotNet(characters);

            SerializacaoNewtonSoft(characters);

            SerializandoComHerança(Arquivo);
        }

        private static void SerializandoComHerança(IEnumerable<string> Arquivo)
        {
            var good = Arquivo.Where(t => t.Split(',')[1] == "good").Select(t => new GoodChar(t));
            var bad = Arquivo.Where(t => t.Split(',')[1] == "bad").Select(t => new BadChar(t));

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            List<Character> characteresCategorizados = (good as IEnumerable<Character>).Concat(bad).ToList();

            var objetoSerializado = JsonConvert.SerializeObject(characteresCategorizados, settings);
            File.WriteAllText("chars-newtonsoft-heranca.json", objetoSerializado);

            string arquivoDisco = File.ReadAllText("chars-newtonsoft-heranca.json");
            var objetoDeserializado = JsonConvert.DeserializeObject<List<Character>>(arquivoDisco, settings);
        }

        private static void SerializacaoNewtonSoft(IEnumerable<Character> characters)
        {
            var objetoSerializado = JsonConvert.SerializeObject(characters);
            File.WriteAllText("chars-newtonsoft.json", objetoSerializado);

            string arquivoDisco = File.ReadAllText("chars-newtonsoft.json");
            var objetoDeserializado = JsonConvert.DeserializeObject<Character[]>(arquivoDisco);

            ImprimirTabela("Json .net", objetoDeserializado.ToList());
        }

        private static void SerializacaoDotNet(IEnumerable<Character> characters)
        {
            CharsJson charsJson = new CharsJson() { Characters = characters.ToList() };

            var arquivoSerializado = System.Text.Json.JsonSerializer.Serialize(charsJson);

            File.WriteAllText("chars.json", arquivoSerializado);

            string arquivoDisco = File.ReadAllText("chars.json");

            var chars = System.Text.Json.JsonSerializer.Deserialize<CharsJson>(arquivoDisco);

            ImprimirTabela("Json .net", chars.Characters);
        }
    }
}
