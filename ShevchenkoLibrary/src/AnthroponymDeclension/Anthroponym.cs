using Shevchenko.Language;

namespace Shevchenko.AnthroponymDeclension
{
    /// <summary>
    /// Anthroponym class.
    /// <example>                                
    /// <code>                                   
    /// new Anthroponym                          
    /// {                                        
    ///     Gender = GrammaticalGender.Masculine,
    ///     GivenName = "Тарас",                 
    ///     PatronymicName = "Григорович",       
    ///     FamilyName = "Шевченко"              
    /// };                                       
    /// </code>                                  
    /// </example>                               
    /// </summary>
    public class Anthroponym
    {
        public GrammaticalGender? Gender { get; set; }
        public string GivenName { get; set; }
        public string PatronymicName { get; set; }
        public string FamilyName { get; set; }
    }
}