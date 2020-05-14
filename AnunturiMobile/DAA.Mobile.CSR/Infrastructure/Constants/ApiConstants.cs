namespace Anunturi.Mobile.Infrastructure.Constants
{
    class ApiConstants
    {
        public static string BaseApiUrl
        {
            get
            {
                return "https://192.168.1.3:45457";
            }
        }

        public static string AnuntHubUrl
        {
            get
            {
                return "https://192.168.1.3:45457/signalAnunt";
            }
        }

        public static string ApiKey
        {
            get
            {
                return "5590f6ce-a156-4d4c-bd81-894c870016cf";
            }
        }

        public static string TokenEndpointPath { get; } = $"{BaseApiUrl}/Token";
    }
}
