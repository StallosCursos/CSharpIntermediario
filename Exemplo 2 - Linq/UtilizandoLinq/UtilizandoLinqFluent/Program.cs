using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using UtilizandoLinq;

namespace UtilizandoLinqFluent
{
    class Program: ProgramBase
    {
        static void Main(string[] args)
        {
            IEnumerable<string> Arquivo = LerArquivoCsv("./charcters_stats.csv");

            IEnumerable<Character> characters = Arquivo.Select(t => new Character(t));

            UtilizandoFiltros(characters);
            Console.Clear();

            UtilizandoAgrupamento(characters);
            Console.Clear();

            SomandoValores(characters);
            Console.Clear();

            UtilizandoJuncoes(characters);
        }

        private static void UtilizandoJuncoes(IEnumerable<Character> characters)
        {
            var resultado = characters.GroupBy(t => t.Alignment)
                                      .ToDictionary(k => k.Key, v => v.ToList());

            var bons = resultado["good"];
            var mals = resultado["bad"];

            var juncao = bons.Join(mals, b => b.Total, m => m.Total, (bons, mals) =>
            {
                return new
                {
                    charbom = bons,
                    charmal = mals
                };
            });

            ConsoleTable consoleTable = new ConsoleTable();
            consoleTable.AddColumn(new[] { "Bom", "Mal", "Total" });

            foreach (var item in juncao)
            {
                consoleTable.AddRow(item.charbom.Name, item.charmal.Name, item.charbom.Total);
            }
            consoleTable.Write();
        }

        private static void UtilizandoAgrupamento(IEnumerable<Character> characters)
        {
            var dicionarioGrupos = characters.GroupBy(t => t.Alignment)
                                             .OrderBy(t => t.Key)
                                             .Select(t => new { Aligment = t.Key, Chars = t.ToList() })
                                             .ToDictionary(k => k.Aligment, v => v.Chars);

            ImprimirTabela("Apenas Chars bons", dicionarioGrupos["good"]);
            ImprimirTabela("Apenas Chars ruins", dicionarioGrupos["bad"]);
        }

        private static void SomandoValores(IEnumerable<Character> characters)
        {
            var resultado = characters.Where(t => t.Intelligence >= 50)
                                      .GroupBy(t => t.Alignment)
                                      .Select(t => new
                                      {
                                          Chave = t.Key,
                                          Quantidade = t.Count(),
                                          Chars = t.ToList(),
                                          SomaInteligencia = t.Sum(s => s.Intelligence),
                                          MinimoInteligencia = t.Min(s => s.Intelligence),
                                          MaximoInteligencia = t.Max(s => s.Intelligence)
                                      });

            resultado.ToList().ForEach(t =>
            {

                ConsoleTable consoleTable = new ConsoleTable();
                consoleTable.AddColumn(new[] { "Chave", "Quantidade", "Soma", "Minimo", "Maximo" });
                consoleTable.AddRow(t.Chave, t.Quantidade, t.SomaInteligencia, t.MinimoInteligencia, t.MaximoInteligencia);
                consoleTable.Write();

                ImprimirTabela($"Chars do grupo {t.Chave}", t.Chars);
            });
        }

        private static void UtilizandoFiltros(IEnumerable<Character> characters)
        {
            var charsBons = characters.Where(t => t.Alignment == "good");
            ImprimirTabela("Apenas chars bons", charsBons.ToList());
        }
    }
}
