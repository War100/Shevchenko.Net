using Shevchenko.Language;
using Shevchenko.WordDeclension;

namespace ShevchenkoTest.WordDeclension;

public class DeclensionRuleInflectorTest
{
    [Fact]
    public void Inflect_ShouldReturnOriginalWord_WhenNoMatchingRuleFound()
    {
        // Arrange
        var rule = new DeclensionRule
        {
            Pattern = new DeclensionPattern { Modify = "(о)$" },
            GrammaticalCases = new GrammaticalCases()
        };
        var inflector = new DeclensionRuleInflector(rule);

        // Act
        var result = inflector.Inflect("мова", GrammaticalCase.Genitive);

        // Assert
        Assert.Equal("мова", result);  // Should return original as no rule matches
    }
    
    [Fact]
    public void Inflect_ShouldReturnInflectedWord_WhenRuleReplaceApplies()
    {
        // Arrange
        var rule = new DeclensionRule
        {
            Pattern = new DeclensionPattern { Modify = "(о)$" }, // Find pattern ending with "о"
            GrammaticalCases = new GrammaticalCases
            {
                { GrammaticalCase.Genitive, new List<InflectionCommands>
                    {
                        new InflectionCommands
                        {
                            { 0, new InflectionCommand { Action = InflectionCommandAction.Replace, Value = "і" } }
                        }
                    }
                }
            }
        };
        var inflector = new DeclensionRuleInflector(rule);
        
        // Act
        var result = inflector.Inflect("Місто", GrammaticalCase.Genitive);

        // Assert
        Assert.Equal("Місті", result);
    }
    
    [Fact]
    public void Inflect_ShouldReturnInflectedWord_WhenRuleAppendApplies()
    {
        // Arrange
        var rule = new DeclensionRule
        {
            Pattern = new DeclensionPattern { Modify = "(єм)$" }, // Find pattern ending with "о"
            GrammaticalCases = new GrammaticalCases
            {
                { GrammaticalCase.Genitive, new List<InflectionCommands>
                    {
                        new InflectionCommands
                        {
                            { 0, new InflectionCommand { Action = InflectionCommandAction.Append, Value = "у" } }
                        }
                    }
                }
            }
        };
        var inflector = new DeclensionRuleInflector(rule);
        
        // Act
        var result = inflector.Inflect("Віфлієм", GrammaticalCase.Genitive);

        // Assert
        Assert.Equal("Віфлієму", result);
    }

}