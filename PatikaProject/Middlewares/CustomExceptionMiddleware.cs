namespace PatikaProject.Middlewares
{
    public class CustomExceptionMiddleware
    {

        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next) 
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

        }
    }
}
