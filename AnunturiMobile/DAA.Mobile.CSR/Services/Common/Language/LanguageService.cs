using Plugin.Multilingual;
using Anunturi.Mobile.Infrastructure.Constants;
using Anunturi.Mobile.Resources.Language;
using Anunturi.Mobile.Services.Common.Settings;
using System.Globalization;

namespace Anunturi.Mobile.Services.Common.Language
{
    public class LanguageService : ILanguageService
    {
        private readonly ISettingsService _settingsService;

        public LanguageService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public void LoadLanguageFromSettings()
        {
            var languageCode = _settingsService.Language;

            if (languageCode != null)
            {
                var culture = new CultureInfo(languageCode);
                LanguageResources.Culture = culture;
                CrossMultilingual.Current.CurrentCultureInfo = culture;
            }
        }

        public void SetApplicationLanguage(string languageCode)
        {
            _settingsService.Language = languageCode;
            languageCode = languageCode == null ? LanguagesConstants.EnglishLanguageCode : languageCode;

            var culture = new CultureInfo(languageCode);
            LanguageResources.Culture = culture;
            CrossMultilingual.Current.CurrentCultureInfo = culture;
        }
    }
}
