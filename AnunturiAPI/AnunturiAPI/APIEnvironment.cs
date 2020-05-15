using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnunturiAPI
{
    public class APIEnvironment
    {
        public static string[] IpWhitelist = new string[]
        {
            "127.0.0.1",
            "::1",
            "192.168.1.3"
        };

        public static string ApiKey = "5590f6ce-a156-4d4c-bd81-894c870016cf";

        public static readonly Dictionary<string, SmsAuthority> SMS_RECEIVERS = new Dictionary<string, SmsAuthority>
        {
            {
                "+37378079134",
                new SmsAuthority()
                {
                    AccountSid = "AC1d1a4276cb9ab42e8c13045d02ccdc15",
                    Authtoken = "3ab4ee5407f045aa9ab274bb6d86196e",
                    MainPhone = "+12017787530"
                }
            },
            {
                "+37360113415",
                new SmsAuthority()
                {
                    AccountSid = "ACbbbb7724d520f12272a36d44bb8fa9a5",
                    Authtoken = "7977deae702cafeb375e476b75b230f1",
                    MainPhone = "+13343261561"
                }
            }
        };
    }
}