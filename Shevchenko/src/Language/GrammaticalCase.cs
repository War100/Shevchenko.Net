namespace Shevchenko.Language
{
    using System.ComponentModel;

    public enum GrammaticalCase
    {
        [Description("nominative")]
        Nominative,
    
        [Description("genitive")]
        Genitive,
    
        [Description("dative")]
        Dative,
    
        [Description("accusative")]
        Accusative,
    
        [Description("ablative")]
        Ablative,
    
        [Description("locative")]
        Locative,
    
        [Description("vocative")]
        Vocative
    }
}