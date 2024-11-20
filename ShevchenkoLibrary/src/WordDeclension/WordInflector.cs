namespace Shevchenko.WordDeclension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Shevchenko.Language;
    
    public delegate bool CustomRuleFilter(DeclensionRule declensionRule, int index, List<DeclensionRule> declensionRules);

    public class DeclensionParams
    {
        public GrammaticalCase GrammaticalCase { get; set; }
        public GrammaticalGender? Gender { get; set; }
        public WordClass? WordClass { get; set; }
        public ApplicationType? ApplicationType { get; set; }
        public CustomRuleFilter CustomRuleFilter { get; set; }
    }
    
    public class WordInflector
    {
        //private readonly List<DeclensionRule> _declensionRules;
        public List<DeclensionRule> _declensionRules { get; set; }

        public WordInflector(IEnumerable<DeclensionRule> declensionRules)
        {
            ValidateRules(declensionRules);
            _declensionRules = declensionRules
                .OrderByDescending(rule => rule.Priority)
                .ToList();
        }

        /// <summary>
        /// Inflects a given word according to the specified parameters.
        /// </summary>
        public async Task<string> InflectAsync(string word, DeclensionParams parameters)
        {
            if (word == null)
                throw new InvalidOperationException("Word parameter is null.");
            if (parameters == null)
                throw new InvalidOperationException("Parameters are null.");
            
            var matchingRules = await FindMatchingRulesAsync(word, parameters);
            ValidateRules(matchingRules);
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
            if (string.IsNullOrEmpty(word))
                throw new ArgumentException("The input word cannot be null or empty.", nameof(word));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters), "Declension parameters cannot be null.");

            return await Task.Run(() =>
            {
                ValidateRules(_declensionRules);
                var matchingRules = _declensionRules
                    .Where(rule =>
                    {
                        if (rule == null)
                            throw new InvalidOperationException("One of the declension rules is null.");

                        if (rule.Gender == null)
                            return false;
                            //throw new InvalidOperationException($"Rule with pattern '{rule?.Pattern?.Find}' has null Gender.");

                        return rule.Gender.Contains(parameters.Gender);
                    })
                    .Where(rule =>
                    {
                        if (rule != null && (rule.ApplicationType == null || rule.ApplicationType.Count == 0))
                            return true;

                        if (parameters.ApplicationType == null)
                            return true;

                        return rule != null && rule.ApplicationType.Contains(parameters.ApplicationType.Value);
                    })
                    .Where(rule =>
                    {
                        if (rule.Pattern == null)
                            throw new InvalidOperationException("A declension rule has a null Pattern.");

                        if (string.IsNullOrEmpty(rule.Pattern.Find))
                            throw new InvalidOperationException("A declension rule has an empty 'Find' pattern.");

                        return Regex.IsMatch(word, rule.Pattern.Find, RegexOptions.IgnoreCase);
                    })
                    .Where(rule => parameters.WordClass == null || rule.WordClass == parameters.WordClass)
                    .Where((rule, index) =>
                    {
                        if (parameters.CustomRuleFilter == null)
                            return true;

                        return parameters.CustomRuleFilter(rule, index, _declensionRules);
                    })
                    .ToList();

                ValidateRules(matchingRules);
                return matchingRules;
            });
        }
        
        private void ValidateRules(IEnumerable<DeclensionRule> rules)
        {
            foreach (var rule in rules)
            {
                if (rule == null)
                    throw new InvalidOperationException("A declension rule is null.");
                if (rule.Gender == null)
                    throw new InvalidOperationException($"Rule with pattern '{rule?.Pattern?.Find}' has null Gender.");
                if (rule.Pattern == null || string.IsNullOrEmpty(rule.Pattern.Find))
                    throw new InvalidOperationException("A declension rule has an invalid or missing pattern.");
            }
        }

    }
}