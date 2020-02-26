using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Libs.Utils.Generators
{
    public class UniqueStringGenerator
    {
        public GeneratorSettings Settings { get; set; } = new GeneratorSettings();

        public IEnumerable<string> Generate()
        {
            if (!CanGenerate())
            {
                throw new Exception("The requested number of strings cannot be generated.");
            }

            var strings = new HashSet<string>();
            for (var i = 0; i < Settings.Count;)
            {
                if (strings.Add(Randomizer.RandomString(Settings.Pattern, Settings.Wildcard, Settings.Charset)))
                {
                    i++;
                }
            }

            return strings;
        }

        public bool CanGenerate()
        {
            var randomCharacterCount = Regex.Matches(Settings.Pattern, $"\\{Settings.Wildcard}").Count;
            return randomCharacterCount > 0 && Math.Pow(Settings.Charset.Distinct().Count(), randomCharacterCount) >= Settings.Count;
        }
    }

    public class GeneratorSettings
    {
        private string _pattern;

        public char Wildcard { get; set; } = DefaultWildcard;

        public int Count { get; set; } = 1;

        public int Length { get; set; } = DefaultLength;

        public string Charset { get; set; } = Charsets.SafeAlphaNumeric;

        public string Pattern
        {
            get => _pattern ?? new string(Wildcard, Length);
            set => _pattern = value;
        }

        public const char DefaultWildcard = '?';

        public const int DefaultLength = 8;
    }
}
