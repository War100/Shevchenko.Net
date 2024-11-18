namespace Shevchenko.Language
{
    using System.ComponentModel;
    
    public enum WordClass
    {
        [Description("noun")]
        Noun,
        
        [Description("adjective")]
        Adjective,
        
        [Description("unknown")]
        Unknown,
    }
}