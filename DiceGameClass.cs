using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public class DiceGameClass
    {
        private List<Dice> DiceList;

        public DiceGameClass(List<Dice> diceList)
        {
            DiceList = diceList;
        }

        public void Play()
        {
            Console.WriteLine("Let's determine who makes the first move.");
            var fairFirst = new FairRandom(2);
            Console.WriteLine($"Computer selected a random value in the range 0 - 1 (HMAC={fairFirst.HmacHex}).");
            int userGuess = GetUserNumber(2);
            Console.WriteLine($"Computer selection: {fairFirst.ComputerNumber} (KEY={fairFirst.GetKeyHex()}).");

            bool userMovesFirst = (fairFirst.ComputerNumber == userGuess);

            int userDieIndex = userMovesFirst ? PromptUserSelectDice() : PromptComputerSelectDice();
            int computerDieIndex = userMovesFirst ? PromptComputerSelectDice(exclude: userDieIndex) : PromptUserSelectDice(exclude: userDieIndex);

            Dice userDie = DiceList[userDieIndex];
            Dice computerDie = DiceList[computerDieIndex];

            Console.WriteLine("It's Computer roll.");
            var compRoll = new FairRandom(computerDie.Faces.Length);
            Console.WriteLine($"Computer selected a random value in the range 0 - {computerDie.Faces.Length - 1} (HMAC={compRoll.HmacHex}).");
            int userInput = GetUserNumber(computerDie.Faces.Length);
            Console.WriteLine($"Computer number is {compRoll.ComputerNumber} (KEY={compRoll.GetKeyHex()}).");
            int compIndex = compRoll.FinalResult(userInput, computerDie.Faces.Length);
            int compValue = computerDie.Roll(compIndex);
            Console.WriteLine($"Computer roll result is {compValue}.");

            Console.WriteLine("It's your roll.");
            var userRoll = new FairRandom(userDie.Faces.Length);
            Console.WriteLine($"Computer selected a random value in the range 0 - {userDie.Faces.Length - 1} (HMAC={userRoll.HmacHex}).");
            int userChoice = GetUserNumber(userDie.Faces.Length);
            Console.WriteLine($"Computer number is {userRoll.ComputerNumber} (KEY={userRoll.GetKeyHex()}).");
            int userIndex = userRoll.FinalResult(userChoice, userDie.Faces.Length);
            int userValue = userDie.Roll(userIndex);
            Console.WriteLine($"Your roll result is {userValue}.");

            if (userValue > compValue) Console.WriteLine("You win!");
            else if (userValue < compValue) Console.WriteLine("Computer wins!");
            else Console.WriteLine("It's a tie!");
        }

        private int GetUserNumber(int max)
        {
            while (true)
            {
                Console.WriteLine(string.Join("\n", Enumerable.Range(0, max).Select(i => $"{i} - {i}")));
                Console.WriteLine("X - exit\n? - help");
                string input = Console.ReadLine().Trim().ToUpper();
                if (input == "X") Environment.Exit(0);
                if (input == "?") TablePrinter.Print(DiceList);
                if (int.TryParse(input, out int number) && number >= 0 && number < max)
                    return number;
                Console.WriteLine("Invalid input. Try again.");
            }
        }

        private int PromptUserSelectDice(int? exclude = null)
        {
            Console.WriteLine("Choose your dice:");
            for (int i = 0; i < DiceList.Count; i++)
            {
                if (i == exclude) continue;
                Console.WriteLine($"{i} - {DiceList[i]}");
            }
            return GetUserNumber(DiceList.Count);
        }

        private int PromptComputerSelectDice(int? exclude = null)
        {
            var rnd = new Random();
            List<int> choices = Enumerable.Range(0, DiceList.Count).Where(i => i != exclude).ToList();
            int choice = choices[rnd.Next(choices.Count)];
            Console.WriteLine($"Computer selected the {DiceList[choice]} dice.");
            return choice;
        }
    }
}
