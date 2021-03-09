using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ConsoleTables;

namespace LeituraEscritaDisco
{
    class Program
    {
        private static Stopwatch _stopwatch = Stopwatch.StartNew();

        static void Main(string[] args)
        {
            Console.WriteLine("Lendo arquivo do disco");

            FileInfo fileInfo = new FileInfo("./charcters_stats.csv");
            
            Console.WriteLine($"Caminho completo: {fileInfo.FullName}");
            Console.WriteLine($"Tamnho em Bytes: { fileInfo.Length }");
            Console.WriteLine($"extensão Arquivo { fileInfo.Extension }");

            if (fileInfo.Exists)
            {
                LeituraCarregandoArquivoInteiro();
                LeituraSemCarregarArquivoInteiro();
                LeituraComStreamReader();
            }
        }

        private static void LeituraComStreamReader()
        {
            ConsoleTable consoleTable = new ConsoleTable();

            using (StreamReader reader = new StreamReader("./charcters_stats.csv"))
            {
                consoleTable.AddColumn(reader.ReadLine().Split(','));
                while (!reader.EndOfStream)
                {
                    consoleTable.AddRow(reader.ReadLine().Split(','));
                }
                consoleTable.Write();
            }
        }

        private static void LeituraSemCarregarArquivoInteiro()
        {
            ConsoleTable consoleTable = new ConsoleTable();

            _stopwatch.Restart();

            int indexLine = 0;
            foreach (var line in File.ReadLines("./charcters_stats.csv"))
            {
                if (indexLine == 0)
                    consoleTable.AddColumn(line.Split(','));
                else
                    consoleTable.AddRow(line.Split(','));

                indexLine++;
            }
            consoleTable.Write();

            _stopwatch.Stop();
            Console.WriteLine($"Tempo de execução carregando arquivo parcialmente {_stopwatch.ElapsedMilliseconds}");
        }

        private static void LeituraCarregandoArquivoInteiro()
        {
            ConsoleTable consoleTable = new ConsoleTable();

            _stopwatch.Restart();

            List<string> linhas = File.ReadAllLines("./charcters_stats.csv").ToList();

            consoleTable.AddColumn(linhas[0].Split(','));
            for (int i = 1; i < linhas.Count; i++)
            {
                string[] colunas = linhas[i].Split(',');
                consoleTable.AddRow(colunas);
            }

            consoleTable.Write();

            _stopwatch.Stop();
            Console.WriteLine($"Tempo de execução carregando arquivo inteiro {_stopwatch.ElapsedMilliseconds}");
        }
    }
}
