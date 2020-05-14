using Newtonsoft.Json;
using Refit;
using System;

namespace Anunturi.Mobile.Infrastructure.Helpers
{
    public static class ExceptionHelpers
    {
        public static bool IsApiBadRequestException(this Exception exception)
        {
            var apiException = exception as ApiException;
            if (apiException != null && apiException.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return true;
            }

            return false;
        }

        public static bool IsJsonException(this Exception exception)
        {
            return exception is JsonException;
        }
    }
}
