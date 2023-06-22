//using Microsoft.Extensions.Configuration;

//namespace Shop.UsersApi.Controllers
//{
//    public class ApiKeyAuthentication : IEndpointFilter
//    {
//        //abc12c04-83cd-4778-8715-72f3ec104006 <- actualApiKey
//        private const string ApiKeyHeaderName = "X-Api-Key";
//        private readonly IConfiguration _configuration;

//        public ApiKeyAuthentication(IConfiguration configuration) 
//        {
//            _configuration = configuration;
//        }

//        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
//        {
//            string? apiKey = context.HttpContext.Request.Headers[ApiKeyHeaderName];

//            if (!IsApiKeyValid(apiKey))
//            {
//                return Results.Unauthorized();
//            }

//            return await next(context);
//        }
//        private bool IsApiKeyValid(string? apiKey)
//        {
//            if(string.IsNullOrWhiteSpace(apiKey))
//            {
//                return false;
//            }
//            string actualApiKey = _configuration.GetValue<string>(apiKey)!;
//            return apiKey == actualApiKey;
//        }
//    }
//}
