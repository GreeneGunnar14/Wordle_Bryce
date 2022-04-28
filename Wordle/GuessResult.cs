using System;
using System.Collections.Generic;

namespace Wordle
{

	public class GuessResult
	{
        public List<LetterGuess> Guess { get; set; }

        public GuessResult(List<LetterGuess> guess)
		{
            Guess = guess;
		}

        public override string ToString()
        {
            string result = "";

            foreach(var item in Guess)
            {
                result += item.Letter;
            }

            result += "\n";

            foreach (var item in Guess)
            {
                switch(item.LetterResult)
                {
                    case LetterResult.Correct:
                        result += "C";
                        break;

                    case LetterResult.Misplaced:
                        result += "M";
                        break;

                    case LetterResult.Incorrect:
                        result += "I";
                        break;
                }
            }

            result += "\n";

            return result;
        }
    }
}

