namespace Shevchenko.AnthroponymDeclension
{
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Shevchenko.Language;
    using Shevchenko.WordDeclension;
    using Shevchenko.AnthroponymDeclension.FamilyNameClassifier;
    
    /// <summary>
    /// Handles inflection of family names based on grammatical rules and classification.
    /// </summary>
    public class FamilyNameInflector : NameInflector
    {
        private static readonly Regex UncertainFemininePattern = new Regex("[ая]$", RegexOptions.IgnoreCase);
        private static readonly Regex UncertainMasculinePattern = new Regex("(ой|ий|ій|их)$", RegexOptions.IgnoreCase);

        private readonly WordInflector _wordInflector;
        private readonly FamilyNameClassifier.FamilyNameClassifier _familyNameClassifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="FamilyNameInflector"/> class.
        /// </summary>
        /// <param name="wordInflector">The inflector used for word declension.</param>
        /// <param name="familyNameClassifier">The classifier for determining family name classes.</param>
        public FamilyNameInflector(WordInflector wordInflector, FamilyNameClassifier.FamilyNameClassifier familyNameClassifier)
        {
            _wordInflector = wordInflector;
            _familyNameClassifier = familyNameClassifier;
        }

        /// <inheritdoc />
        protected override async Task<string> InflectNamePartAsync(
            string familyName,
            GrammaticalGender gender,
            GrammaticalCase grammaticalCase,
            bool isLastWord)
        {
            // Skip inflection for monosyllabic names unless they are the last part.
            if (!isLastWord && Linguistics.IsMonosyllable(familyName))
            {
                return familyName;
            }

            FamilyNameClass familyNameClass = null;

            // Check if the family name's class is uncertain and requires classification.
            if (IsUncertainFamilyNameClass(familyName, gender))
            {
                familyNameClass = _familyNameClassifier.Classify(familyName);
            }

            // Perform inflection using the word inflector.
            return await _wordInflector.InflectAsync(familyName, new DeclensionParams
            {
                GrammaticalCase = grammaticalCase,
                Gender = gender,
                WordClass = familyNameClass?.WordClass,
                ApplicationType = ApplicationType.FamilyName
            });
        }

        /// <summary>
        /// Determines whether a given family name could be ambiguous between noun and adjective classes.
        /// </summary>
        /// <param name="familyName">The family name to check.</param>
        /// <param name="gender">The grammatical gender of the name.</param>
        /// <returns>True if the family name is uncertain; otherwise, false.</returns>
        private static bool IsUncertainFamilyNameClass(string familyName, GrammaticalGender gender)
        {
            return (gender == GrammaticalGender.Feminine && UncertainFemininePattern.IsMatch(familyName)) ||
                   (gender == GrammaticalGender.Masculine && UncertainMasculinePattern.IsMatch(familyName));
        }
    }
}
