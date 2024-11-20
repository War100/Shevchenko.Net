namespace Shevchenko.WordDeclension
{
    using System.ComponentModel;
    using System.Collections.Generic;
    using Shevchenko.Language;
    
    public enum ApplicationType
    {
        [Description("givenName")]
        GivenName,
        [Description("patronymicName")]
        PatronymicName,
        [Description("familyName")]
        FamilyName
    }

    public class DeclensionPattern
    {
        public string Find { get; set; }
        public string Modify { get; set; }
    }

    public enum InflectionCommandAction
    {
        [Description("replace")]
        Replace,
        [Description("append")]
        Append
    }

    public class InflectionCommand
    {
        public InflectionCommandAction Action { get; set; }
        public string Value { get; set; }
    }

    public class GrammaticalCases : Dictionary<GrammaticalCase, List<InflectionCommands>> {}

    public class InflectionCommands : Dictionary<int, InflectionCommand> {}
    
    public class DeclensionRule
    {
        public string Description { get; set; }
        
        public List<string> Examples { get; set; }
        
        public WordClass WordClass { get; set; }
        
        public List<GrammaticalGender?> Gender { get; set; }
        
        public int Priority { get; set; }
        
        public List<ApplicationType> ApplicationType { get; set; }
        
        public DeclensionPattern Pattern { get; set; }
        
        public GrammaticalCases GrammaticalCases { get; set; }
    }
}