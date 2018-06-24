using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparator {
    class Program {
        static void Main(string[] args) {
            string pathMaster = "m.txt";
            string pathTest = "t.txt";
            char delimiter = '\t';

            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<string> fileMaster = File.ReadAllLines(pathMaster).ToList();
            List<string> fileTest = File.ReadAllLines(pathTest).ToList();
            int countColums = fileMaster[0].Split(delimiter).Length;
            int countRows=0;
            Console.WriteLine(fileMaster.Count());
            Console.WriteLine(fileTest.Count);
            fileMaster = fileMaster.Except(fileTest).ToList();
            fileTest = fileTest.Except(fileMaster).ToList();
            Console.WriteLine(fileMaster.Count());
            Console.WriteLine(fileTest.Count);
            List<string> singleColumnMaster = new List<string>();
            List<string> singleColumnTest = new List<string>();

            List<double> percColumnDeviations = new List<double>();

            for (int i = 0; i < countColums; i++) {
                singleColumnMaster.Clear();
                singleColumnTest.Clear();

                foreach (var row in fileMaster) {
                    singleColumnMaster.Add(row.Split(delimiter)[i]);
                }
                
                foreach (var row in fileTest) {
                    singleColumnTest.Add(row.Split(delimiter)[i]);
                }

                countRows = singleColumnMaster.Distinct().Count();

                List<string> diff = singleColumnMaster.Select(d => d.ToString()).Intersect(singleColumnTest).ToList();
                percColumnDeviations.Add((double)diff.Count / countRows);
            }

            foreach (var perc in percColumnDeviations) {
                Console.Write(perc + " ");
            }
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------");
            sw.Stop();
            Console.WriteLine("Total Elapsed = {0}", sw.Elapsed);
        }
    }
}
