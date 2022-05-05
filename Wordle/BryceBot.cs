using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Wordle
{
    public class BryceBot : IWordleBot
    {
        public BryceBot()
        {
            Guesses = new List<GuessResult>();

            string[] unFilteredData = File.ReadAllLines(Directory.GetCurrentDirectory() + "/../../../data/unigram_freq.csv");

            WordFrequencies = new Dictionary<string, int>();

            PositionalFrequencies = new Dictionary<int, MaxHeap<char>>();

            for (int i = 0; i < WordLength; i++)
            {
                PositionalFrequencies[i] = new MaxHeap<char>();
            }

            int count = 0;
            int line = 0;
            while (count < 10000)
            {
                string[] splitLine = unFilteredData[line].Split(',');

                string word = splitLine[0];

                //Find all words with the correct length and add their frequencies to the Word Frequency dictionary
                if (word.Length == WordLength)
                {
                    if (int.TryParse(splitLine[1], out int freq))
                    {
                        WordFrequencies[word] = freq;
                        count++;
                    }
                }

                line++;
            }
        }

        private int WordLength = 5;

        public List<GuessResult> Guesses { get; set; }

        public List<string> GuessStrings { get; set; }

        public Dictionary<string, int> WordFrequencies { get; set; }

        public Dictionary<int, MaxHeap<char>> PositionalFrequencies { get; set; }

        public Dictionary<char, int> LetterCount { get; set; }

        public string RetrieveMax()
        {
            int max = 0;

            string value = "";

            foreach (var word in WordFrequencies.Keys)
            {
                if (WordFrequencies[word] > max)
                {
                    value = word;

                    max = WordFrequencies[word];
                }
            }

            return value;
        }

        public string GenerateGuess()
        {
            LetterCount = new Dictionary<char, int>();

            string output = "";

            if (Guesses.Count != 0)
            {
                List<LetterGuess> lastGuess = Guesses[Guesses.Count - 1].Guess;

                foreach (var letter in lastGuess)
                {
                    if (letter.LetterResult == LetterResult.Correct || letter.LetterResult == LetterResult.Misplaced)
                    {
                        try
                        {
                            LetterCount[letter.Letter] += 1;
                        }
                        catch (KeyNotFoundException)
                        {
                            LetterCount[letter.Letter] = 1;
                        }
                    }
                }

                foreach (var word in WordFrequencies.Keys)
                {
                    bool removeWord = true;

                    foreach (var guess in Guesses)
                    {
                        if (guess.GuessString == word)
                        {
                            break;
                        }
                        removeWord = false;
                    }
                    if (!removeWord)
                    {
                        GuessResult previousGuess = Guesses[Guesses.Count - 1];

                        for (int i = 0; i < WordLength; i++)
                        {
                            bool condition1 = previousGuess.GuessString[i] == word[i] && previousGuess.Guess[i].LetterResult != LetterResult.Correct;
                            bool condition1Inverse = previousGuess.GuessString[i] != word[i] && previousGuess.Guess[i].LetterResult == LetterResult.Correct;

                            if (condition1 || condition1Inverse)
                            {
                                removeWord = true;
                                break;
                            }
                        }
                    }
                    if (!removeWord)
                    {
                        Dictionary<char, int> guessLetterCount = new Dictionary<char, int>();

                        foreach (var letter in word)
                        {
                            try
                            {
                                guessLetterCount[letter] += 1;
                            }
                            catch (KeyNotFoundException)
                            {
                                guessLetterCount[letter] = 1;
                            }
                        }

                        foreach (var letter in LetterCount.Keys)
                        {
                            try
                            {
                                if (guessLetterCount[letter] < LetterCount[letter])
                                {
                                    removeWord = true;
                                    break;
                                }

                            }
                            catch (KeyNotFoundException)
                            {
                                removeWord = true;
                                break;
                            }
                        }
                    }
                    if (removeWord)
                    {
                        WordFrequencies.Remove(word);
                    }
                }
            }

            output = RetrieveMax();

            return output;
        }
    }
}
