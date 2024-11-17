using System.Threading.Tasks;

namespace Shevchenko.AnthroponymDeclension
{
    using Shevchenko.Language;
    
    /// <summary>
    /// Provides functionality for inflecting anthroponyms (names, patronymics, and family names).
    /// </summary>
    public class AnthroponymInflector
    {
        private readonly GivenNameInflector _givenNameInflector;
        private readonly PatronymicNameInflector _patronymicNameInflector;
        private readonly FamilyNameInflector _familyNameInflector;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnthroponymInflector"/> class.
        /// </summary>
        /// <param name="givenNameInflector">The inflector for given names.</param>
        /// <param name="patronymicNameInflector">The inflector for patronymic names.</param>
        /// <param name="familyNameInflector">The inflector for family names.</param>
        public AnthroponymInflector(
            GivenNameInflector givenNameInflector,
            PatronymicNameInflector patronymicNameInflector,
            FamilyNameInflector familyNameInflector)
        {
            _givenNameInflector = givenNameInflector;
            _patronymicNameInflector = patronymicNameInflector;
            _familyNameInflector = familyNameInflector;
        }

        /// <summary>
        /// Inflects the anthroponym in the specified grammatical case.
        /// </summary>
        /// <param name="anthroponym">The anthroponym to inflect.</param>
        /// <param name="gender">The grammatical gender of the anthroponym.</param>
        /// <param name="grammaticalCase">The grammatical case to inflect into.</param>
        /// <returns>An inflected <see cref="Anthroponym"/>.</returns>
        public async Task<Anthroponym> InflectAsync(
            Anthroponym anthroponym,
            GrammaticalGender gender,
            GrammaticalCase grammaticalCase)
        {
            var inflectedAnthroponym = new Anthroponym();

            if (!string.IsNullOrEmpty(anthroponym.GivenName))
            {
                inflectedAnthroponym.GivenName = await _givenNameInflector.InflectAsync(
                    anthroponym.GivenName,
                    gender,
                    grammaticalCase
                );
            }

            if (!string.IsNullOrEmpty(anthroponym.PatronymicName))
            {
                inflectedAnthroponym.PatronymicName = await _patronymicNameInflector.InflectAsync(
                    anthroponym.PatronymicName,
                    gender,
                    grammaticalCase
                );
            }

            if (!string.IsNullOrEmpty(anthroponym.FamilyName))
            {
                inflectedAnthroponym.FamilyName = await _familyNameInflector.InflectAsync(
                    anthroponym.FamilyName,
                    gender,
                    grammaticalCase
                );
            }

            return inflectedAnthroponym;
        }
    }
}