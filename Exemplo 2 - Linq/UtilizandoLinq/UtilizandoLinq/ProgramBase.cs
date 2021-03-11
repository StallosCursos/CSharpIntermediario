using ConsoleTables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UtilizandoLinq
{
    internal class ProgramBase
    {
        private static List<string> _colunas = new List<string>();

        internal static List<Character> CarregarLista(IEnumerable<string> Arquivo)
        {
            var chars = ConvertendoEnumerableParaLista(Arquivo);
            return chars;
        }

        internal static List<Character> ConvertendoEnumerableParaLista(IEnumerable<string> Arquivo)
        {
            List<Character> characters = Arquivo.Select(linha => new Character(linha)).ToList();
            return characters;
        }

        internal static void ImprimirTabela(string titulo, List<Character> characters)
        {
            ConsoleTable consoleTable = new ConsoleTable();
            consoleTable.AddColumn(_colunas);

            characters.ForEach(
                line => consoleTable.AddRow(
                    line.Name, line.Alignment, line.Intelligence, line.Strength, line.Speed,
                    line.Durability, line.Power, line.Combat, line.Total
                )
            );

            Console.WriteLine(titulo);
            consoleTable.Write();
        }

        internal static IEnumerable<string> LerArquivoCsv(string fileName)
        {
            string line;

            StreamReader file = new StreamReader(fileName);

            _colunas.Clear();
            _colunas.AddRange(file.ReadLine().Split(','));

            while ((line = file.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
}