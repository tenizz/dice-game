using DiceGame;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("❌ Error: You must specify at least 3 dice.");
            Console.WriteLine("Example: dotnet run 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3");
            return;
        }

        List<Dice> diceList = new List<Dice>();

        try
        {
            foreach (var arg in args)
            {
                var dice = new Dice(arg);
                if (dice.Faces.Length < 1)
                    throw new Exception("Each dice must have at least one face.");
                diceList.Add(dice);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("❌ Error: Invalid dice format. Use comma-separated integers.");
            Console.WriteLine("Example: dotnet run 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3");
            return;
        }

        // Start the game
        var game = new DiceGameClass(diceList);
        game.Play();
    }
}