using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace DiceGame
{
    public class TablePrinter
    {
        public static void Print(List<Dice> diceList)
        {
            Console.WriteLine("Probability of the win for the user:");

            var header = new List<string> { "User dice v" };
            header.AddRange(diceList.Select(d => d.ToString()));

            var table = new ConsoleTable(header.ToArray());

            for (int i = 0; i < diceList.Count; i++)
            {
                var row = new List<string> { diceList[i].ToString() };
                for (int j = 0; j < diceList.Count; j++)
                {
                    if (i == j)
                    {
                        row.Add("—");
                        continue;
                    }
                    double p = CalculateWinProbability(diceList[i], diceList[j]);
                    row.Add($"{p:F4}");
                }
                table.AddRow(row.ToArray());
            }
            table.Write();
        }

        private static double CalculateWinProbability(Dice a, Dice b)
        {
            int winCount = a.Faces.SelectMany(x => b.Faces, (x, y) => x > y ? 1 : 0).Sum();
            return winCount / (double)(a.Faces.Length * b.Faces.Length);
        }
    }
}
