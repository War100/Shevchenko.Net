namespace Shevchenko.AnthroponymDeclension
{
    using System;
    
    /// <summary>
    /// Anthroponym class.
    /// </summary>
    public class Anthroponym
    {
        public string GivenName { get; set; }
        public string PatronymicName { get; set; }
        public string FamilyName { get; set; }
        
        public static explicit operator DeclensionOutput(Anthroponym anthroponym)
        {
            if (anthroponym == null)
                throw new ArgumentNullException(nameof(anthroponym));

            return new DeclensionOutput
            {
                GivenName = anthroponym.GivenName,
                PatronymicName = anthroponym.PatronymicName,
                FamilyName = anthroponym.FamilyName
            };
        }
    }
}