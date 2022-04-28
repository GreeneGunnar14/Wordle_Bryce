using System;

namespace Wordle
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new BryceBot();

            var game = new WordleGame();

            game.MaxGuesses = 6;

            Console.WriteLine(game.Play(bot));
        }
    }
}

