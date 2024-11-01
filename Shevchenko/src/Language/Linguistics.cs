using System;
using System.Text.RegularExpressions;

namespace Shevchenko.Language
{
    public static class Linguistics
    {
        private static readonly Regex PATTERN_VOWELS = new Regex("[аоуеиіяюєї]", RegexOptions.IgnoreCase);

        /// <summary>
        /// Returns the number of syllables in a given word.
        /// </summary>
        public static int CountSyllables(string word)
        {
            var matches = PATTERN_VOWELS.Matches(word);
            return matches.Count;
        }

        /// <summary>
        /// Returns true if a given word is a monosyllable.
        /// Returns false otherwise.
        /// </summary>
        public static bool IsMonosyllable(string word)
        {
            return CountSyllables(word) == 1;
        }
    }

}