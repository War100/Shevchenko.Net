using Shevchenko.Language;
using Shevchenko.WordDeclension;

namespace ShevchenkoTest.WordDeclension;

public class GrammaticalCasesTests
    {
        /// <summary>
        /// This test checks that the GrammaticalCases object is initialized with empty lists of InflectionCommands
        /// for each case.
        /// </summary>
        [Fact]
        public void ShouldInitializeGrammaticalCasesWithEmptyLists()
        {
            var grammaticalCases = new GrammaticalCases
            {
                { GrammaticalCase.Nominative, new List<InflectionCommands>() },
                { GrammaticalCase.Genitive, new List<InflectionCommands>() },
                { GrammaticalCase.Dative, new List<InflectionCommands>() },
                { GrammaticalCase.Accusative, new List<InflectionCommands>() },
                { GrammaticalCase.Ablative, new List<InflectionCommands>() },
                { GrammaticalCase.Locative, new List<InflectionCommands>() },
                { GrammaticalCase.Vocative, new List<InflectionCommands>() }
            };
            
            Assert.NotNull(grammaticalCases);
            foreach (var key in grammaticalCases.Keys)
            {
                Assert.Empty(grammaticalCases[key]);
            }
        }

        /// <summary>
        /// Test that checks whether it is possible to add command for selected case.
        /// Also check if the values "Value" and "Action" are properly saved in created command.
        /// </summary>
        [Fact]
        public void ShouldAddInflectionCommandToNominativeCase()
        {
            var grammaticalCases = new GrammaticalCases
            {
                { GrammaticalCase.Nominative, new List<InflectionCommands>() }
            };

            var inflectionCommand = new InflectionCommand
            {
                Action = InflectionCommandAction.Append,
                Value = "и"
            };

            var inflectionCommands = new InflectionCommands
            {
                { 1, inflectionCommand }
            };
            
            grammaticalCases[GrammaticalCase.Nominative].Add(inflectionCommands);
            
            Assert.Single(grammaticalCases[GrammaticalCase.Nominative]);
            Assert.Equal("и", grammaticalCases[GrammaticalCase.Nominative][0][1].Value);
            Assert.Equal(InflectionCommandAction.Append, grammaticalCases[GrammaticalCase.Nominative][0][1].Action);
        }
    }
