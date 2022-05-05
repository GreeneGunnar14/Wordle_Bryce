using System;
using System.IO;

namespace Wordle
{
    class Program
    {
        static void Main(string[] args)
        {
            WordleGame game = new WordleGame("where");

            game.MaxGuesses = 6;

            BryceBot bot = new BryceBot();

            Console.WriteLine(game.Play(bot));
        }
    }
}

