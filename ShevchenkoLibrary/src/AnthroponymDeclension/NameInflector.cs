namespace Shevchenko.AnthroponymDeclension
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shevchenko.Language;
    
    public abstract class NameInflector
    {
        /// <summary>
        /// Inflects the name in the given grammatical case.
        /// </summary>
        /// <param name="name">The name to inflect.</param>
        /// <param name="gender">The grammatical gender of the name.</param>
        /// <param name="grammaticalCase">The grammatical case to inflect the name into.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the inflected name.</returns>
        public async Task<string> InflectAsync(
            string name,
            GrammaticalGender? gender,
            GrammaticalCase grammaticalCase)
        {
            var inflectedNameParts = new List<string>();

            var nameParts = name.Split(separator: '-');
            for (var index = 0; index < nameParts.Length; index++)
            {
                var inflectedNamePart = await InflectNamePartAsync(
                    word: nameParts[index],
                    gender: gender,
                    grammaticalCase: grammaticalCase,
                    isLastWord: index == nameParts.Length - 1);

                inflectedNameParts.Add(item: inflectedNamePart);
            }

            return string.Join(separator: "-", values: inflectedNameParts);
        }

        /// <summary>
        /// Inflects a single part of a compound name in the given grammatical case.
        /// </summary>
        /// <param name="word">The part of the name to inflect.</param>
        /// <param name="gender">The grammatical gender of the name part.</param>
        /// <param name="grammaticalCase">The grammatical case to inflect the name part into.</param>
        /// <param name="isLastWord">Indicates if this is the last part of the name.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the inflected name part.</returns>
        protected abstract Task<string> InflectNamePartAsync(
            string word,
            GrammaticalGender? gender,
            GrammaticalCase grammaticalCase,
            bool isLastWord);
    }
}