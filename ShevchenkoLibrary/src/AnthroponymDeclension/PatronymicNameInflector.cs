namespace Shevchenko.AnthroponymDeclension
{
    using System.Threading.Tasks;
    using Shevchenko.Language;
    using Shevchenko.WordDeclension;

    /// <summary>
    /// Handles inflection of patronymic names based on grammatical rules.
    /// </summary>
    public class PatronymicNameInflector : NameInflector
    {
        private readonly WordInflector _wordInflector;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatronymicNameInflector"/> class.
        /// </summary>
        /// <param name="wordInflector">The <see cref="WordInflector"/> instance to use for inflecting words.</param>
        public PatronymicNameInflector(WordInflector wordInflector)
        {
            _wordInflector = wordInflector;
        }

        /// <inheritdoc />
        protected override async Task<string> InflectNamePartAsync(
            string patronymicName,
            GrammaticalGender? gender,
            GrammaticalCase grammaticalCase,
            bool isLastWord)
        {
            var parameters = new DeclensionParams
            {
                GrammaticalCase = grammaticalCase,
                Gender = gender,
                ApplicationType = ApplicationType.PatronymicName,
                CustomRuleFilter = (declensionRule, index, rules) =>
                    declensionRule.ApplicationType.Contains(ApplicationType.PatronymicName)
            };

            return await _wordInflector.InflectAsync(patronymicName, parameters);
        }
    }
}