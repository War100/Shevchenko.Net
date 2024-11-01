using System;

namespace Shevchenko.Language
{
    /// <summary>
    /// Ukrainian alphabet encoding where the key is the letter of the alphabet
    /// and the value is the order number of the corresponding letter starting from 1.
    /// </summary>
    public enum AlphabetEncoding
    {
        А = 1,
        Б,
        В,
        Г,
        Ґ,
        Д,
        Е,
        Є,
        Ж,
        З,
        И,
        І,
        Ї,
        Й,
        К,
        Л,
        М,
        Н,
        О,
        П,
        Р,
        С,
        Т,
        У,
        Ф,
        Х,
        Ц,
        Ч,
        Ш,
        Щ,
        Ь,
        Ю,
        Я
    }

    /// <summary>
    /// Size of Ukrainian alphabet.
    /// </summary>
    public static class AlphabetConstants
    {
        public const int ALPHABET_SIZE = 33;
    }

    /// <summary>
    /// A letter of Ukrainian alphabet.
    /// </summary>
    public struct Letter
    {
        private readonly AlphabetEncoding _value;

        public Letter(char character)
        {
            if (Enum.TryParse(character.ToString(), true, out AlphabetEncoding parsedValue))
            {
                _value = parsedValue;
            }
            else
            {
                throw new ArgumentException("Invalid Ukrainian letter.", nameof(character));
            }
        }

        public AlphabetEncoding Value => _value;

        public override string ToString() => _value.ToString();
    }

}