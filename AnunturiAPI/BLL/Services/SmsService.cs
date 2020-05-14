using Infrastructure;
using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BLL.Services
{
    public class SmsService
    {
        public IList<string> SendSms(string text, Dictionary<string, SmsAuthority> receivers)
        {
            var errors = new List<string>();

            foreach (var receiver in receivers)
            {
                try
                {
                    TwilioClient.Init(receiver.Value.AccountSid, receiver.Value.Authtoken);
                    var message = MessageResource.Create(
                    body: text,
                    from: new Twilio.Types.PhoneNumber(receiver.Value.MainPhone),
                    to: new Twilio.Types.PhoneNumber(receiver.Key));

                    TwilioClient.Invalidate();
                }
                catch (Exception e)
                {
                    errors.Add($"Can't send sms to {receiver.Key}. {e.Message}");
                }
            }

            return errors;
        }
    }
}
