namespace Shevchenko.WordDeclension
{
    using System;
    using System.Text.RegularExpressions;
    using Shevchenko.Language;
    
    public static class PatternUtils
    {
        /// <summary>
        /// Counts the number of groups in a given regular expression.
        /// </summary>
        public static int CountGroups(string pattern)
        {
            var regex = new Regex($"{pattern}|");
            var matches = regex.Match(string.Empty);
            return matches.Groups.Count - 1;
        }
    }
    
    public class DeclensionRuleInflector
    {
        private readonly DeclensionRule _rule;
        private readonly CommandRunnerFactory _commandRunnerFactory;

        public DeclensionRuleInflector(DeclensionRule rule)
        {
            _rule = rule;
            _commandRunnerFactory = new CommandRunnerFactory();
        }

        /// <summary>
        /// Inflects the given word in the specified grammatical case using the rule.
        /// </summary>
        public string Inflect(string word, GrammaticalCase grammaticalCase)
        {
            if (_rule.GrammaticalCases.TryGetValue(grammaticalCase, out var commandsList) && commandsList.Count > 0)
            {
                var commands = commandsList[0];
                var searchPattern = new Regex(_rule.Pattern.Modify, RegexOptions.IgnoreCase);
                var inflectedWord = searchPattern.Replace(word, match =>
                {
                    var replacer = string.Empty;
                    var groupCount = PatternUtils.CountGroups(_rule.Pattern.Modify);
                
                    for (int groupIndex = 0; groupIndex < groupCount; groupIndex++)
                    {
                        var value = match.Groups[groupIndex + 1].Value; 
                        if (commands.TryGetValue(Convert.ToInt32(groupIndex.ToString()), out var command) && command != null)
                        {
                            value = _commandRunnerFactory.Make(command).Exec(value);
                        }
                        replacer += value;
                    }
                    return replacer;
                });

                return LetterCase.CopyLetterCase(word, inflectedWord);
            }
            return word;
        }
    }
}