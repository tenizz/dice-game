using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public class Dice
    {
        public int[] Faces { get; }

        public Dice(string csv)
        {
            Faces = csv.Split(',').Select(int.Parse).ToArray();
        }

        public int Roll(int index) => Faces[index % Faces.Length];

        public override string ToString() =>  string.Join(",", Faces);
    }
}
