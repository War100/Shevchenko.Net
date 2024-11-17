using System.Reflection;
using Shevchenko;
using Xunit.Abstractions;

namespace ShevchenkoTest.AnthroponymDeclension;

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Shevchenko.Language;
using Shevchenko.WordDeclension;
using Shevchenko.AnthroponymDeclension;


public class TestAnthroponymInflector
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly AnthroponymInflector _anthroponymInflector;
     
        public TestAnthroponymInflector(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            var wordInflector = new WordInflector(TestRules());
            var familyNameClassifier = new Shevchenko.AnthroponymDeclension.FamilyNameClassifier.FamilyNameClassifier();
            var givenNameInflector = new GivenNameInflector(wordInflector);
            var patronymicNameInflector = new PatronymicNameInflector(wordInflector);
            var familyNameInflector = new FamilyNameInflector(wordInflector, familyNameClassifier);

            _anthroponymInflector = new AnthroponymInflector(
                givenNameInflector,
                patronymicNameInflector,
                familyNameInflector
            );
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public async Task AnthroponymInflector_Should_Inflect_Correctly(
            AnthroponymTestCase dataItem)
        {
            var anthroponym = dataItem.GrammaticalCases["nominative"];
            var gender = dataItem.Gender;

            foreach (var grammaticalCaseKey in dataItem.GrammaticalCases.Keys)
            {
                if (Enum.TryParse<GrammaticalCase>(grammaticalCaseKey, true, out var grammaticalCase))
                {
                    var expected = dataItem.GrammaticalCases[grammaticalCaseKey];
                    var result = await _anthroponymInflector.InflectAsync(anthroponym, gender, grammaticalCase);
                    
                    _testOutputHelper.WriteLine($"Testing: {grammaticalCase}, Gender: {gender}");
                    _testOutputHelper.WriteLine($"Expected: {expected.GivenName} {expected.PatronymicName} {expected.FamilyName}");
                    _testOutputHelper.WriteLine($"Actual: {result.GivenName} {result.PatronymicName} {result.FamilyName}");
    
                    Assert.Equal(expected.GivenName, result.GivenName);
                    Assert.Equal(expected.PatronymicName, result.PatronymicName);
                    Assert.Equal(expected.FamilyName, result.FamilyName);
                }
                else
                {
                    throw new ArgumentException($"Invalid grammatical case: {grammaticalCaseKey}");
                }
            }

        }
    
        public static IEnumerable<object[]> TestData()
        {
            var testDataPath = "Resources/anthroponym-inflector.test-data.json";
            var jsonData = File.ReadAllText(testDataPath);
            var testCases = JsonConvert.DeserializeObject<List<AnthroponymTestCase>>(jsonData);

            foreach (var testCase in testCases)
            {
                yield return new object[] { testCase };
            }
        }
        
        public static IEnumerable<DeclensionRule> TestRules()
        {
            
            var rulesDataPath = "Resources/declension-rules.json";
            var jsonData = File.ReadAllText(rulesDataPath);
            var rules = JsonConvert.DeserializeObject<List<DeclensionRule>>(jsonData);

            return rules;
        }
    }

    public class AnthroponymTestCase
    {
        public GrammaticalGender Gender { get; set; }
        public Dictionary<string, Anthroponym> GrammaticalCases { get; set; }
    }