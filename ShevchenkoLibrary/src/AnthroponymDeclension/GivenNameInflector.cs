namespace Shevchenko.AnthroponymDeclension
{
    using System.Threading.Tasks;
    using Shevchenko.Language;
    using Shevchenko.WordDeclension;
    
    /// <summary>
    /// Handles inflection of given names based on grammatical rules.
    /// </summary>
    public class GivenNameInflector : NameInflector
    {
        private readonly WordInflector _wordInflector;

        /// <summary>
        /// Initializes a new instance of the <see cref="GivenNameInflector"/> class.
        /// </summary>
        /// <param name="wordInflector">The <see cref="WordInflector"/> instance to use for inflecting words.</param>
        public GivenNameInflector(WordInflector wordInflector)
        {
            _wordInflector = wordInflector;
        }

        /// <inheritdoc />
        protected override async Task<string> InflectNamePartAsync(
            string givenName,
            GrammaticalGender gender,
            GrammaticalCase grammaticalCase,
            bool isLastWord)
        {
            var parameters = new DeclensionParams
            {
                GrammaticalCase = grammaticalCase,
                Gender = gender,
                ApplicationType = ApplicationType.GivenName
            };

            return await _wordInflector.InflectAsync(givenName, parameters);
        }
    }
}