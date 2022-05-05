using System;
namespace Wordle
{
	public enum LetterResult
	{
		Correct,
		Misplaced,
		Incorrect
	}

	public class LetterGuess
	{
        public char Letter { get; set; }

		public LetterResult LetterResult { get; set; }

        public LetterGuess(char letter)
		{
			Letter = letter;
			LetterResult = LetterResult.Incorrect;
		}
	}
}

