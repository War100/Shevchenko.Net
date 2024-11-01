using System.Text;

namespace Shevchenko.Language
{
    public class LetterCase
    {
        /// <summary>
        /// Copies a letter case pattern from a template word to a target word.
        /// Returns a modified target word in the letter case of the template word.
        /// </summary>
        public static string CopyLetterCase(string templateWord, string targetWord)
        {
            var result = new StringBuilder();

            for (int index = 0; index < targetWord.Length; index++)
            {
                char templateLetter = index < templateWord.Length
                    ? templateWord[index]
                    : templateWord[templateWord.Length - 1];
            
                char targetLetter = targetWord[index];
                if (InLowerCase(templateLetter))
                {
                    result.Append(char.ToLower(targetLetter));
                }
                else if (InUpperCase(templateLetter))
                {
                    result.Append(char.ToUpper(targetLetter));
                }
                else
                {
                    result.Append(targetLetter);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Detects if a letter is in upper case.
        /// </summary>
        private static bool InUpperCase(char letter)
        {
            return char.IsUpper(letter);
        }

        /// <summary>
        /// Detects if a letter is in lower case.
        /// </summary>
        private static bool InLowerCase(char letter)
        {
            return char.IsLower(letter);
        }
    }
}