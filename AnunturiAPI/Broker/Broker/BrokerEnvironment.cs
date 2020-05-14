
namespace Broker
{
    public class BrokerEnvironment
    {
        public static string[] IpWhitelist = new string[]
        {
            "127.0.0.1",
            "::1",
            "192.168.1.3"
        };

        public static string ApiKey = "5590f6ce-a156-4d4c-bd81-894c870016cf";

        public static string ApiBaseUrl = "https://localhost:44358";

        public static string BrokerBaseUrl = "https://192.168.1.3:443/";
    }
}