using System;
using System.Collections.Generic;
namespace Wordle
{
	public class WordleGame
	{
        public string SecretWord { get; set; }
        public int MaxGuesses { get; set; }

		public WordleGame(string secretWord = "arise")
		{
			SecretWord = secretWord;
		}

		public int Play(IWordleBot bot)
        {
			int guessNumber;
			for(guessNumber = 0; guessNumber < MaxGuesses; guessNumber++)
            {
				string guess = bot.GenerateGuess();
                Console.WriteLine($"guess {guessNumber + 1}: {guess}");

				GuessResult guessResult = CheckGuess(guess);
				bot.Guesses.Add(guessResult);
                Console.WriteLine(guessResult);

				if(IsCorrect(guessResult))
                {
					return guessNumber;
                }
            }

			return guessNumber;
        }

		public GuessResult CheckGuess( string guess )
        {
			var letterGuesses = new List<LetterGuess>();

			var foundArray = new int[guess.Length];

			for (int i = 0; i < guess.Length; i++)
            {
				var result = new LetterGuess(guess[i]);

				if (guess[i] == SecretWord[i] && foundArray[i] != 1)
                {
					result.LetterResult = LetterResult.Correct;

					foundArray[i] = 1;
                }
				else
                {
					for (int j = 0; j < guess.Length; j++)
                    {
						if (guess[i] == guess[j] && foundArray[j] != 1)
                        {
							result.LetterResult = LetterResult.Misplaced;

							foundArray[j] = 1;
                        }
						else
                        {
							result.LetterResult = LetterResult.Incorrect;
                        }
                    }
                }

				letterGuesses.Add(result);
            }

			var guessResult = new GuessResult(letterGuesses);

			return guessResult;
        }

		private bool IsCorrect(GuessResult guessResult)
        {
			foreach(var letterGuess in guessResult.Guess)
			{
				if (letterGuess.LetterResult != LetterResult.Correct)
					return false;

			}

			return true;
        }
	}
}

