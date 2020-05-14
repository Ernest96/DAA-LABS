namespace Anunturi.Mobile.Infrastructure.Constants
{
    public class AppSettingsConstants
    {
        public const bool DebugMode =
#if DEBUG
            true;
#else
            false;
#endif
    }
}
