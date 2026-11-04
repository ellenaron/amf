using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Beolvasas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string exeDir = Path.GetDirectoryName(exePath);
            string csvFilePath = Path.Combine(exeDir, "Preferences", "naprendszer.csv");

            if (!File.Exists(csvFilePath))
            {
                Console.WriteLine($"A fájl nem található: {csvFilePath}");
                return;
            }

            string[] datas = File.ReadAllLines(csvFilePath).Skip(1).ToArray();
            // verzió 1
            foreach (string data in datas)
            {
                string[] fields = data.Split(';');

            }
            // verzió 2
            //for (int i = 1; i < datas.Length; i++)
            //{
            //    string data = datas[i];
            //}
        }
    }
}
