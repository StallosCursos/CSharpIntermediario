using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UtilizandoLinq;

namespace SerializandoBinario
{
    class Program: ProgramBase
    {
        static void Main(string[] args)
        {
            IEnumerable<string> Arquivo = LerArquivoCsv("./charcters_stats.csv");
            IEnumerable<Character> characters = Arquivo.Select(t => new Character(t));

            var Resultado = characters.Where(t => t.Alignment == "good");

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            SerializandoObjeto(Resultado, binaryFormatter);

            List<Character> ArquivoDeserializado;

            using (FileStream ArquivoSerializado = new FileStream("./chars_bons.ser", FileMode.Open))
            {
                ArquivoDeserializado = (List<Character>)binaryFormatter.Deserialize(ArquivoSerializado);
                ArquivoSerializado.Close();
            }
        }

        private static void SerializandoObjeto(IEnumerable<Character> Resultado, BinaryFormatter binaryFormatter)
        {
            using (FileStream ArquivoSerializado = new FileStream("./chars_bons.ser", FileMode.Create))
            {
                binaryFormatter.Serialize(ArquivoSerializado, Resultado.ToList());
                ArquivoSerializado.Close();
            }
        }
    }
}
