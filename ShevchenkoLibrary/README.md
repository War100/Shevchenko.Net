# Shevchenko.net
Library for declension of Ukrainian anthroponyms.

This library is a C#/.NET translation of the original JavaScript library [shevchenko-js](https://github.com/tooleks/shevchenko-js).
Translation of the library is done for .Net Standard 2.0 Framework.

This project is created as a part of the university practice. It's still incomplete so use it with caution.

The unfinished part is the family-name-classifier which is responsible for classifying family name class. Instead of ML model this project uses dictionary and heuristic method.

## Usage

### Personal name declension 
``` C#
using Shevchenko;
using Shevchenko.AnthroponymDeclension;

namespace ShevchenkoTestConsole;


class Program
{
    static async Task Main(string[] args)
    {
        var input = new Anthroponym
        {
            GivenName = "Тарас",
            PatronymicName = "Григорович",
            FamilyName = "Шевченко",
            Gender = Shevchenko.Language.GrammaticalGender.Masculine
        };
        
        var output = await AnthroponymInflection.InAblativeAsync(input);
        Console.WriteLine($"{output.GivenName} {output.PatronymicName} {output.FamilyName}");
    }
}
```
#### Output
``` C#
Тарасом Григоровичем Шевченком
```

### Personal name declension without provided gender
``` C#
using Shevchenko;
using Shevchenko.AnthroponymDeclension;

namespace ShevchenkoTestConsole;


class Program
{
    static async Task Main(string[] args)
    {
        Anthroponym input = new Anthroponym
        {
            GivenName = "Тарас",
            PatronymicName = "Григорович",
            FamilyName = "Шевченко",
        };
        
        var output = await AnthroponymInflection.InAblativeAsync(input);
        Console.WriteLine($"{output.GivenName} {output.PatronymicName} {output.FamilyName}");
    }
}
```

#### Output
``` C#
Тарасом Григоровичем Шевченком
```

### Automatic grammatical gender detection
```C#
using Shevchenko;
using Shevchenko.AnthroponymDeclension;

namespace ShevchenkoTestConsole;


class Program
{
    static async Task Main(string[] args)
    {
        Anthroponym input = new Anthroponym
        {
            GivenName = "Лариса",
            PatronymicName = "Петрівна",
            FamilyName = "Косач",
        };

        var gender = await AnthroponymInflection.DetectGenderAsync(input);

        input.Gender = gender;
        
        Console.WriteLine($"{gender}");
    }
}
```

#### Output
```
Feminine
```

## License
This project is licensed under the MIT License. See the LICENSE file for details.
