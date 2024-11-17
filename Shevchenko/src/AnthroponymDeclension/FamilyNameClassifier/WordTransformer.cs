using System;
using System.Collections.Generic;
using System.Linq;
using Tensorflow.Common.Extensions;

namespace Shevchenko.AnthroponymDeclension.FamilyNameClassifier
{
    using Language;
    
    public class WordTransformer
    {
        private readonly int _vectorSize;
        private readonly byte _unknownCharcode;

        public WordTransformer(int vectorSize, byte unknownCharcode = 0)
        {
            _vectorSize = vectorSize;
            _unknownCharcode = unknownCharcode;
        }

        /// <summary>
        /// Vectorizes a given word for ML processing.
        /// </summary>
        public byte[] Encode(string word)
        {
            var values = new byte[_vectorSize];
            var letters = word
                .Reverse()
                .Take(_vectorSize)
                .Reverse()
                .Select(char.ToLowerInvariant)
                .PadLeft(_vectorSize, '-');

            int index = 0;
            foreach (char letter in letters)
            {
                try
                {
                    var letterEncoding = new Letter(letter).Value;
                    values[index++] = (byte)letterEncoding; // Перетворюємо `AlphabetEncoding` у `byte`
                }
                catch (ArgumentException)
                {
                    values[index++] = _unknownCharcode;
                }
            }

            return values;
        }
    }

    public static class EnumerableExtensions
    {
        /// <summary>
        /// Pads a sequence to the left with the specified character until it reaches the desired length.
        /// </summary>
        public static IEnumerable<char> PadLeft(this IEnumerable<char> sequence, int totalWidth, char paddingChar)
        {
            var padCount = Math.Max(0, totalWidth - sequence.Count());
            return Enumerable.Repeat(paddingChar, padCount).Concat(sequence);
        }
    }
}