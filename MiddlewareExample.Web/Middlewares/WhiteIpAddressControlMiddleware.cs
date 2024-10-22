using System.Net;

namespace MiddlewareExample.Web.Middlewares
{
    public class WhiteIpAddressControlMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private const string WhiteIpAddress = "192.01.01.02";
        public WhiteIpAddressControlMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }



        public async Task InvokeAsync(HttpContext context)
        {

            var reqIpAddress = context.Connection.RemoteIpAddress;


            bool AnyWhiteIpAddress = IPAddress.Parse(WhiteIpAddress).Equals(reqIpAddress);


            if (AnyWhiteIpAddress == true)
            {

                await _requestDelegate(context);
            }

            else
            {

                context.Response.StatusCode = HttpStatusCode.Forbidden.GetHashCode();
                await context.Response.WriteAsync("Forbidden");
            }




        }
    }
}