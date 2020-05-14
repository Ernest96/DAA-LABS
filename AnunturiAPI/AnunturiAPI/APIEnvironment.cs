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
                    AccountSid = "id",
                    Authtoken = "token",
                    MainPhone = "+12017787530"
                }
            },
            {
                "+37360113415",
                new SmsAuthority()
                {
                    AccountSid = "id",
                    Authtoken = "token",
                    MainPhone = "+13343261561"
                }
            }
        };
    }
}