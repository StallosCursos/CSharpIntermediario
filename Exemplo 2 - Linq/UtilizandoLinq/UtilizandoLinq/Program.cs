using ConsoleTables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UtilizandoLinq
{
    class Program : ProgramBase
    {
        static void Main(string[] args)
        {
            IEnumerable<string> Arquivo = LerArquivoCsv("./charcters_stats.csv");

            List<Character> chars = CarregarLista(Arquivo);

            UtilizandoFiltros(chars);

            Console.Clear();

            UtilizandoAgrupamento(chars);

            Console.Clear();

            SomandoValores(chars);

            Console.Clear();

            UtilizandoJuncoes(chars);
        }

        private static void UtilizandoJuncoes(List<Character> chars)
        {
            var resultado = (
                               from consulta in chars
                               group consulta by consulta.Alignment into Group
                               select new
                               {
                                   Chave = Group.Key,
                                   chars = Group.ToList()
                               }
                            ).ToDictionary(t => t.Chave, t => t.chars);

            var bons = resultado["good"];
            var mals = resultado["bad"];

            var juncao = from charsbons in bons
                         join charsmals in mals on charsbons.Total equals charsmals.Total
                         select new { charbom = charsbons, charmal = charsmals };

            ConsoleTable consoleTable = new ConsoleTable();
            consoleTable.AddColumn(new[] { "Bom", "Mal", "Total" });

            foreach (var item in juncao)
            {      
                consoleTable.AddRow(item.charbom.Name, item.charmal.Name, item.charbom.Total);
            }
            consoleTable.Write();
        }

        private static void SomandoValores(List<Character> chars)
        {
            var resultado = from consulta in chars
                            where consulta.Intelligence >= 50
                            group consulta by consulta.Alignment into Group
                            select new
                            {
                                Chave = Group.Key,
                                Quantidade = Group.Count(),
                                Chars = Group.ToList(),
                                SomaInteligencia = Group.Sum(t => t.Intelligence),
                                MinimoInteligencia = Group.Min(t => t.Intelligence),
                                MaximoInteligencia = Group.Max(t => t.Intelligence)
                             };

            resultado.ToList().ForEach( t => {
 
                ConsoleTable consoleTable = new ConsoleTable();
                consoleTable.AddColumn(new[] { "Chave", "Quantidade", "Soma", "Minimo", "Maximo" });
                consoleTable.AddRow(t.Chave, t.Quantidade, t.SomaInteligencia, t.MinimoInteligencia, t.MaximoInteligencia);
                consoleTable.Write();

                ImprimirTabela($"Chars do grupo {t.Chave}", t.Chars);
            });
        }

        private static void UtilizandoAgrupamento(List<Character> chars)
        {
            var GruposChars = from consulta in chars
                              group consulta by consulta.Alignment into AligmentGroup
                              orderby AligmentGroup.Key
                              select new { Aligment = AligmentGroup.Key, Chars = AligmentGroup.ToList() };

            IDictionary<string, List<Character>> dicionarioGrupos = GruposChars.ToDictionary(k => k.Aligment, v => v.Chars);

            ImprimirTabela("Apenas Chars bons", dicionarioGrupos["good"]);
            ImprimirTabela("Apenas Chars ruins", dicionarioGrupos["bad"]);
        }

        private static void UtilizandoFiltros(List<Character> chars)
        {
            var charsBons = from consulta in chars
                            where consulta.Alignment == "good"
                            select consulta;

            ImprimirTabela("Apenas chars bons", charsBons.ToList());
        }
    }
}
