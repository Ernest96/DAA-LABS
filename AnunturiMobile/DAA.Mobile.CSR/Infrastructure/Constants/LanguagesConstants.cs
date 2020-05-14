using Anunturi.Mobile.Models;
using System.Collections.Generic;

namespace Anunturi.Mobile.Infrastructure.Constants
{
    public class LanguagesConstants
    {
        public static IList<Language> LanguageList { get; }
        public const string EnglishLanguageCode = "en";
        public const string SpanishLanguageCode = "es";

        static LanguagesConstants()
        {
            LanguageList = new List<Language>()
            {
                new Language()
                {
                    Name = "English",
                    Code = EnglishLanguageCode
                },
                new Language()
                {
                    Name = "Español",
                    Code = SpanishLanguageCode
                }
            };
        }

    }
}
