namespace Shevchenko.WordDeclension
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Shevchenko.Language;
    
    public delegate bool CustomRuleFilter(DeclensionRule declensionRule, int index, List<DeclensionRule> declensionRules);

    public class DeclensionParams
    {
        public GrammaticalCase GrammaticalCase { get; set; }
        public GrammaticalGender Gender { get; set; }
        public WordClass? WordClass { get; set; }
        public ApplicationType? ApplicationType { get; set; }
        public CustomRuleFilter CustomRuleFilter { get; set; }
    }
    
    public class WordInflector
    {
        private readonly List<DeclensionRule> _declensionRules;

        public WordInflector(IEnumerable<DeclensionRule> declensionRules)
        {
            _declensionRules = declensionRules
                .OrderByDescending(rule => rule.Priority)
                .ToList();
        }

        /// <summary>
        /// Inflects a given word according to the specified parameters.
        /// </summary>
        public async Task<string> InflectAsync(string word, DeclensionParams parameters)
        {
            var matchingRules = await FindMatchingRulesAsync(word, parameters);
            var matchingRule = matchingRules.FirstOrDefault();

            return matchingRule != null
                ? new DeclensionRuleInflector(matchingRule).Inflect(word, parameters.GrammaticalCase)
                : word;
        }

        /// <summary>
        /// Finds matching declension rules for the given word.
        /// </summary>
        private async Task<List<DeclensionRule>> FindMatchingRulesAsync(string word, DeclensionParams parameters)
        {
            return await Task.Run(() =>
                _declensionRules
                    .Where(rule => rule.Gender.Contains(parameters.Gender))
                    .Where(rule =>
                        parameters.ApplicationType == null ||
                        rule.ApplicationType.Count == 0 ||
                        rule.ApplicationType.Contains(parameters.ApplicationType.Value))
                    .Where(rule => Regex.IsMatch(word, rule.Pattern.Find, RegexOptions.IgnoreCase))
                    .Where(rule => parameters.WordClass == null || rule.WordClass == parameters.WordClass)
                    .Where((rule, index) =>
                        parameters.CustomRuleFilter == null ||
                        parameters.CustomRuleFilter(rule, index, _declensionRules))
                    .ToList()
            );
        }
    }
}