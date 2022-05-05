using System;
using System.Collections.Generic;
namespace Wordle
{
    public class WordleGame
    {
        public string SecretWord { get; set; }
        public int MaxGuesses { get; set; } = 100;

        public WordleGame(string secretWord = "arise")
        {
            SecretWord = secretWord;
        }

        public int Play(IWordleBot bot)
        {
            int guessNumber;
            for (guessNumber = 0; guessNumber < MaxGuesses; guessNumber++)
            {
                string guess = bot.GenerateGuess();
                Console.WriteLine($"guess {guessNumber + 1}: {guess}");

                GuessResult guessResult = CheckGuess(guess);
                bot.Guesses.Add(guessResult);
                Console.WriteLine(guessResult);

                if (IsCorrect(guessResult))
                {
                    return guessNumber + 1;
                }
            }

            return guessNumber;
        }

        public GuessResult CheckGuess(string guess)
        {
            var guessResult = new GuessResult(guess);

            var foundArray = new int[guess.Length];

            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == SecretWord[i] && foundArray[i] != 1)
                {
                    guessResult.Guess[i].LetterResult = LetterResult.Correct;

                    foundArray[i] = 1;
                }
            }

            for (int i = 0; i < guess.Length; i++)
            {
                if (foundArray[i] != 1)
                {
                    for (int j = 0; j < guess.Length; j++)
                    {
                        if (guess[i] == SecretWord[j] && foundArray[j] != 1)
                        {
                            guessResult.Guess[i].LetterResult = LetterResult.Misplaced;

                            foundArray[j] = 1;

                            break;
                        }
                    }
                }
            }

            return guessResult;
        }

        private bool IsCorrect(GuessResult guessResult)
        {
            foreach (var letterGuess in guessResult.Guess)
            {
                if (letterGuess.LetterResult != LetterResult.Correct)
                    return false;
            }

            return true;
        }
    }
}

