namespace Shevchenko
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Shevchenko.Language;
    using Shevchenko.GenderDetection;
    using Shevchenko.WordDeclension;
    using Shevchenko.AnthroponymDeclension;
    
    public class AnthroponymInflection
    {
        private static IEnumerable<DeclensionRule> Rules()
        {
            
            var rulesDataPath = "Resources/declension-rules.json";
            var jsonData = File.ReadAllText(rulesDataPath);
            var rules = JsonConvert.DeserializeObject<List<DeclensionRule>>(jsonData);

            return rules;
        }
        
        private static AnthroponymInflector CreateInflector()
        {
            var wordInflector = new WordInflector(Rules());
            var familyNameClassifier = new Shevchenko.AnthroponymDeclension.FamilyNameClassifier.FamilyNameClassifier();
            var givenNameInflector = new GivenNameInflector(wordInflector);
            var patronymicNameInflector = new PatronymicNameInflector(wordInflector);
            var familyNameInflector = new FamilyNameInflector(wordInflector, familyNameClassifier);
            AnthroponymInflector inflector = new AnthroponymInflector(
                givenNameInflector,
                patronymicNameInflector,
                familyNameInflector
            );
            return inflector;
        }
        
        private static AnthroponymInflector _anthroponymInflector = CreateInflector();
        
        /// <summary>
        /// Inflects an anthroponym in nominative grammatical case.
        /// </summary>
        /// <example>
        /// var anthroponym = await AnthroponymInflector.InNominativeAsync(new DeclensionInput
        /// {
        ///     Gender = GrammaticalGender.Masculine,
        ///     GivenName = "Тарас",
        ///     PatronymicName = "Григорович",
        ///     FamilyName = "Шевченко"
        /// });
        /// </example>
        public static async Task<DeclensionOutput> InNominativeAsync(DeclensionInput input)
        {
            InputValidation.ValidateDeclensionInput(input);
            Anthroponym anthroponym = (Anthroponym)input;
            return (DeclensionOutput)await _anthroponymInflector.InflectAsync(anthroponym, input.Gender, GrammaticalCase.Nominative);
        }

        /// <summary>
        /// Inflects an anthroponym in genitive grammatical case.
        /// </summary>
        public static async Task<DeclensionOutput> InGenitiveAsync(DeclensionInput input)
        {
            InputValidation.ValidateDeclensionInput(input);
            Anthroponym anthroponym = (Anthroponym)input;
            return (DeclensionOutput)await _anthroponymInflector.InflectAsync(anthroponym, input.Gender, GrammaticalCase.Genitive);
        }

        /// <summary>
        /// Inflects an anthroponym in dative grammatical case.
        /// </summary>
        public static async Task<DeclensionOutput> InDativeAsync(DeclensionInput input)
        {
            InputValidation.ValidateDeclensionInput(input);
            Anthroponym anthroponym = (Anthroponym)input;
            return (DeclensionOutput)await _anthroponymInflector.InflectAsync(anthroponym, input.Gender, GrammaticalCase.Dative);
        }

        /// <summary>
        /// Inflects an anthroponym in accusative grammatical case.
        /// </summary>
        public static async Task<DeclensionOutput> InAccusativeAsync(DeclensionInput input)
        {
            InputValidation.ValidateDeclensionInput(input);
            Anthroponym anthroponym = (Anthroponym)input;
            return (DeclensionOutput)await _anthroponymInflector.InflectAsync(anthroponym, input.Gender, GrammaticalCase.Accusative);
        }

        /// <summary>
        /// Inflects an anthroponym in ablative grammatical case.
        /// </summary>
        public static async Task<DeclensionOutput> InAblativeAsync(DeclensionInput input)
        {
            InputValidation.ValidateDeclensionInput(input);
            Anthroponym anthroponym = (Anthroponym)input;
            return (DeclensionOutput)await _anthroponymInflector.InflectAsync(anthroponym, input.Gender, GrammaticalCase.Ablative);
        }

        /// <summary>
        /// Inflects an anthroponym in locative grammatical case.
        /// </summary>
        public static async Task<DeclensionOutput> InLocativeAsync(DeclensionInput input)
        {
            InputValidation.ValidateDeclensionInput(input);
            Anthroponym anthroponym = (Anthroponym)input;
            return (DeclensionOutput)await _anthroponymInflector.InflectAsync(anthroponym, input.Gender, GrammaticalCase.Locative);
        }

        /// <summary>
        /// Inflects an anthroponym in vocative grammatical case.
        /// </summary>
        public static async Task<DeclensionOutput> InVocativeAsync(DeclensionInput input)
        {
            InputValidation.ValidateDeclensionInput(input);
            Anthroponym anthroponym = (Anthroponym)input;
            return (DeclensionOutput)await _anthroponymInflector.InflectAsync(anthroponym, input.Gender, GrammaticalCase.Vocative);
        }

        /// <summary>
        /// Detects the grammatical gender of an anthroponym.
        /// </summary>
        public static async Task<GrammaticalGender?> DetectGenderAsync(GenderDetectionInput input)
        {
            InputValidation.ValidateGenderDetectionInput(input);
            return await GenderDetector.DetectGender((Anthroponym)input);
        }
    }
}