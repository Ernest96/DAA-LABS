namespace Anunturi.Mobile.Services.Common.Language
{
    public interface ILanguageService
    {
        void SetApplicationLanguage(string languageCode);
        void LoadLanguageFromSettings();
    }
}
