// Put this in a new file, e.g. HttpResponseMessageExtensions.cs
using System;
using System.Net.Http;

namespace test_api.Extensions  // Use your project's namespace
{
    public static class HttpResponseMessageExtensions
    {
        public static void WriteRequestToConsole(this HttpResponseMessage response, ILogger logger)
        {
            if (response is null)
            {
                return;
            }

            var request = response.RequestMessage;
            logger.LogInformation(
                "{Method} {Uri} HTTP/{Version}",
                request?.Method,
                request?.RequestUri,
                request?.Version
            );
        }
    }
}