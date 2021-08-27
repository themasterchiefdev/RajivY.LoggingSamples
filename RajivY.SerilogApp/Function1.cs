using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace RajivY.SerilogApp
{
    public static class Function1
    {
        [Function("Function1")]
        public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req, FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Function1");
            logger.LogInformation("C# HTTP trigger function processed a request.");
            var requestId = Guid.NewGuid()
                                .ToString();
            #region Demo Logging Code

            var position = new { Latitude = 25, Longitude = 134,RequestId=requestId };
            const int elapsedMs = 34;
            logger.LogInformation("Processed {@Position} in {Elapsed:000} ms.", position, elapsedMs);

            #endregion

            logger.LogInformation("Received Http request: {@req}.", req.Url);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");
            logger.LogInformation("Response: {@response}", response.Headers);
            return response;
        }
    }
}