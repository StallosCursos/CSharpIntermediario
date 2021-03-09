using System;
using System.IO;
using System.Linq;
using System.Text;

namespace EscritaDisco
{
    class Program
    {
        static void Main(string[] args)
        {
            File.WriteAllText("saidasimples.txt", "Olá");

            string[] linhas = File.ReadAllLines("./charcters_stats.csv").ToArray();
            File.WriteAllLines("saidasimples.txt", linhas);
            
            File.AppendAllLines("saidasimples.txt", linhas);
            File.AppendAllText("saidasimples.txt", "Ultima linha");

            byte[] arquivo = File.ReadAllBytes("./charcters_stats.csv");
            File.WriteAllBytes("saidasimples.txt", arquivo);

            using (FileStream stream = File.Open("saidasimples.txt", FileMode.OpenOrCreate))
            {
                var bytes = Encoding.UTF8.GetBytes($"saida por stream {DateTime.Now.ToString()}");
                stream.Write(bytes);
                stream.Close();
            }
        }
    }
}
